// *********************************************************************************
//	<copyright file="CommonUtilities.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>Common utilities.</summary>
// *********************************************************************************

namespace InternetBulletin.Shared.Helpers
{
    using System.Diagnostics.CodeAnalysis;
    using Azure.Identity;
    using InternetBulletin.Shared.Constants;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using Microsoft.Graph;
    using Microsoft.Graph.Models;
    using static InternetBulletin.Shared.Constants.ConfigurationConstants;

    /// <summary>
    /// Common utilities.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class CommonUtilities
    {
        /// <summary>
        /// Throws if null.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="message">The message.</param>
        /// <param name="commonLogger">The common logger.</param>
        /// <typeparam name="T">The null variable type</typeparam>
        /// <typeparam name="L">The logger type.</typeparam>
        public static T ThrowIfNull<T, L>(T obj, string message, ILogger<L> commonLogger)
        {
            if (obj is null)
            {
                ThrowLoggedException(message, commonLogger);
            }
            return obj;
        }

        /// <summary>
        /// Throws logged exception.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="commonLogger">The common logger.</param>
        /// <typeparam name="T"></typeparam>
        public static void ThrowLoggedException<T>(string message, ILogger<T> commonLogger)
        {
            var exception = new Exception(message);
            commonLogger.LogError(exception, exception.Message);
            throw exception;
        }

        /// <summary>
        /// Validates and parse post id.
        /// </summary>
        /// <param name="postId">The post id.</param>
        /// <param name="logger">The logger.</param>
        /// <typeparam name="T">The logger type.</typeparam>
        public static Guid ValidateAndParsePostId<T>(string postId, ILogger<T> logger)
        {
            if (string.IsNullOrWhiteSpace(postId))
            {
                ThrowLoggedException(ExceptionConstants.PostIdNotPresentMessageConstant, logger);
            }

            if (!Guid.TryParse(postId, out var postGuid))
            {
                ThrowLoggedException(ExceptionConstants.PostGuidNotValidMessageConstant, logger);
            }

            return postGuid;
        }

        /// <summary>
        /// Gets graph api data async.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="logger">The logger.</param>
        /// <returns>The user collection response data.</returns>
        /// <exception cref="Exception">Exception error.</exception>
        public static async Task<UserCollectionResponse> GetGraphApiDataAsync(IConfiguration configuration, ILogger logger)
        {
            try
            {
                logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodEnded, nameof(GetGraphApiDataAsync), DateTime.UtcNow, string.Empty));

                var scopes = new[] { configuration[GraphAPIDefaultScopeConstant] };
                var tenantId = configuration[TenantIdConstant];
                var clientId = configuration[GraphAPIClientIdConstant];
                var clientSecret = configuration[GraphAPIClientSecretConstant];

                var clientSecretCredential = new ClientSecretCredential(tenantId, clientId, clientSecret);
                var graphClient = new GraphServiceClient(clientSecretCredential, scopes);

                // Get all users and filter by the custom username field
                var users = await graphClient.Users
                    .GetAsync(requestConfiguration =>
                    {
                        requestConfiguration.QueryParameters.Select = [
                            IbbsConstants.IdConstant,
                            IbbsConstants.DisplayNameConstant,
                            IbbsConstants.IdentitiesConstant,
                            IbbsConstants.UserNameExtensionConstant
                        ];
                    });

                if (users is not null)
                {
                    return users;
                }

                throw new Exception(ExceptionConstants.UserDoesNotExistsMessageConstant);

            }
            catch (Exception ex)
            {
                logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodFailed, nameof(GetGraphApiDataAsync), DateTime.UtcNow, ex.Message));
                throw;
            }
            finally
            {
                logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodEnded, nameof(GetGraphApiDataAsync), DateTime.UtcNow, string.Empty));
            }
        }

    }
}


