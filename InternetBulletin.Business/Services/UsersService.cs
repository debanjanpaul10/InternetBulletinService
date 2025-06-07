// *********************************************************************************
//	<copyright file="UsersService.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Users Services Class.</summary>
// *********************************************************************************

namespace InternetBulletin.Business.Services
{
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using InternetBulletin.Business.Contracts;
    using InternetBulletin.Data.Contracts;
    using InternetBulletin.Shared.Constants;
    using InternetBulletin.Shared.DTOs.Users;
    using InternetBulletin.Shared.Helpers;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using Microsoft.Graph.Models;

    /// <summary>
    /// The users service class.
    /// </summary>
    /// <param name="usersDataService">The users data service</param>
    /// <param name="configuration">The configuration.</param>
    /// <param name="logger">The logger</param>
    /// <param name="cacheService">The cache service.</param>
    /// <seealso cref="IUsersService"/>
    [ExcludeFromCodeCoverage]
    public class UsersService(IUsersDataService usersDataService, IConfiguration configuration, ILogger<UsersService> logger, ICacheService cacheService) : IUsersService
    {
        /// <summary>
        /// The users data service.
        /// </summary>
        private readonly IUsersDataService _usersDataService = usersDataService;

        /// <summary>
        /// The configuration.
        /// </summary>
        private readonly IConfiguration _configuration = configuration;

        /// <summary>
        /// The _logger.
        /// </summary>
        private readonly ILogger<UsersService> _logger = logger;

        /// <summary>
        /// The cache service.
        /// </summary>
        private readonly ICacheService _cacheService = cacheService;

        /// <summary>
        /// Gets graph user data async.
        /// </summary>
        /// <param name="userName">The user name.</param>
        /// <returns>The graph user data dto.</returns>
        public async Task<GraphUserDTO> GetGraphUserDataAsync(string userName)
        {
            var responseDto = new GraphUserDTO();
            var usersData = new UserCollectionResponse();
            var cachedData = this._cacheService.GetCachedData<UserCollectionResponse?>(CacheKeys.FilteredGraphUsersDataCacheKey);
            if (cachedData is not null)
            {
                usersData = cachedData;
            }
            else
            {
                usersData = await CommonUtilities.GetGraphApiDataAsync(this._configuration, this._logger);
                this._cacheService.SetCacheData(CacheKeys.FilteredGraphUsersDataCacheKey, usersData, CacheKeys.DefaultCacheExpiration);
            }

            if (usersData?.Value is not null)
            {
                // Find the user with matching username
                var user = usersData.Value.FirstOrDefault(u =>
                    u.AdditionalData is not null &&
                    u.AdditionalData.ContainsKey(IbbsConstants.UserNameExtensionConstant) &&
                    Convert.ToString(u.AdditionalData[IbbsConstants.UserNameExtensionConstant], CultureInfo.CurrentCulture) == userName);

                if (user is not null)
                {
                    // Create a new GraphUserDTO object with only the required fields
                    var filteredUser = new GraphUserDTO()
                    {
                        Id = user.Id ?? string.Empty,
                        DisplayName = user.DisplayName ?? string.Empty,
                        UserName = Convert.ToString(user.AdditionalData[IbbsConstants.UserNameExtensionConstant], CultureInfo.CurrentCulture) ?? string.Empty,
                        EmailAddress = user.Identities?
                            .FirstOrDefault(i => i.SignInType == IbbsConstants.EmailAddressConstant)?
                            .IssuerAssignedId ?? string.Empty
                    };

                    responseDto = filteredUser;
                }
            }

            return responseDto;
        }

        /// <summary>
        /// Saves users data from azure ad async.
        /// </summary>
        /// <param name="graphUsersData">The users data.</param>
        /// <returns>The boolean for success / failure</returns>
        public async Task<bool> SaveUsersDataFromAzureAdAsync()
        {
            var graphUsersData = new UserCollectionResponse();
            var cachedData = this._cacheService.GetCachedData<UserCollectionResponse>(CacheKeys.FilteredGraphUsersDataCacheKey);
            if (cachedData is not null)
            {
                graphUsersData = cachedData;
            }
            else
            {
                graphUsersData = await CommonUtilities.GetGraphApiDataAsync(this._configuration, this._logger);
            }

            var userData = GetAllGraphUsersData(graphUsersData);
            if (userData is not null && userData.Count > 0)
            {
                return await this._usersDataService.SaveUsersDataAsync(userData);
            }

            return false;
        }

        #region PRIVATE Methods

        /// <summary>
        /// Gets all graph users data.
        /// </summary>
        /// <param name="graphUsersData">The graph users data.</param>
        /// <returns>The list of graph user dto.</returns>
        private static List<GraphUserDTO> GetAllGraphUsersData(UserCollectionResponse graphUsersData)
        {
            var responseDto = new List<GraphUserDTO>();
            if (graphUsersData?.Value is not null)
            {
                var usersData = graphUsersData.Value.Where(u =>
                    u.AdditionalData.ContainsKey(IbbsConstants.UserNameExtensionConstant)
                    && !string.IsNullOrEmpty(Convert.ToString(u.AdditionalData[IbbsConstants.UserNameExtensionConstant], CultureInfo.CurrentCulture))).ToList();
                if (usersData.Count > 0)
                {
                    responseDto = [..usersData.Select(user => new GraphUserDTO
                    {
                        Id = user.Id ?? string.Empty,
                        DisplayName = user.DisplayName ?? string.Empty,
                        UserName = Convert.ToString(user.AdditionalData[IbbsConstants.UserNameExtensionConstant], CultureInfo.CurrentCulture) ?? string.Empty,
                        EmailAddress = user.Identities?.FirstOrDefault(i => i.SignInType == IbbsConstants.EmailAddressConstant)?.IssuerAssignedId ?? string.Empty
                    })];
                }
            }

            return responseDto;
        }

        #endregion
    }
}


