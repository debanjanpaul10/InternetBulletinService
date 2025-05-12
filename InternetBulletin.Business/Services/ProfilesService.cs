// *********************************************************************************
//	<copyright file="ProfilesService.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Profiles Service Class.</summary>
// *********************************************************************************

namespace InternetBulletin.Business.Services
{
    using System.Threading.Tasks;
    using InternetBulletin.Business.Contracts;
    using InternetBulletin.Data.Contracts;
    using InternetBulletin.Shared.Constants;
    using InternetBulletin.Shared.DTOs;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// The Profiles Data Service Class.
    /// </summary>
    /// <param name="profilesDataService">The Cosmos DB Context</param>
    /// <param name="logger">The Logger</param>
    public class ProfilesService(IProfilesDataService profilesDataService, ILogger<ProfilesService> logger) : IProfilesService
    {
        /// <summary>
        /// The profiles data service.
        /// </summary>
        private readonly IProfilesDataService _profilesDataService = profilesDataService;

        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger<ProfilesService> _logger = logger;
        
        /// <summary>
        /// Gets user profile data async.
        /// </summary>
        /// <param name="userName">The user name.</param>
        public async Task<UserProfileDto> GetUserProfileDataAsync(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                var exception = new Exception(ExceptionConstants.UserIdCannotBeNullMessageConstant);
                this._logger.LogError(exception, exception.Message);
                throw exception;
            }

            var result = await this._profilesDataService.GetUserProfileDataAsync(userName).ConfigureAwait(false);
            if (result is not null && !string.IsNullOrEmpty(result.UserName))
            {
                return result;
            }
            else
            {
                var exception = new Exception(ExceptionConstants.UserDoesNotExistsMessageConstant);
                this._logger.LogError(exception, exception.Message);
                throw exception;
            }
        }
    }
}