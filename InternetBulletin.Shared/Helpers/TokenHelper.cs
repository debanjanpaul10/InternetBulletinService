// *********************************************************************************
//	<copyright file="TokenHelper.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>Token helper class.</summary>
// *********************************************************************************

namespace InternetBulletin.Shared.Helpers
{
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using Azure.Core;
    using Azure.Identity;
    using InternetBulletin.Shared.Constants;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using static InternetBulletin.Shared.Constants.ConfigurationConstants;

    /// <summary>
    /// Token helper.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class TokenHelper
    {
        /// <summary>
        /// Gets ibbs ai token async.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="logger">The logger.</param>
        /// <exception cref="Exception">Exception error.</exception>
        public static async Task<string> GetIbbsAiTokenAsync(IConfiguration configuration, ILogger logger)
        {
            try
            {
                logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodEnded, nameof(GetIbbsAiTokenAsync), DateTime.UtcNow, IbbsConstants.IbbsAIConstant));


                var tenantId = configuration[TenantIdConstant];
                var clientId = configuration[IbbsAiAdClientId];
                var clientSecret = configuration[IbbsAiAdClientSecret];
                var scopes = new[] { string.Format(CultureInfo.CurrentCulture, TokenScopeFormat, clientId) };

                var credential = new ClientSecretCredential(tenantId, clientId, clientSecret);
                var accessToken = await credential.GetTokenAsync(new TokenRequestContext(scopes), CancellationToken.None).ConfigureAwait(false);

                if (string.IsNullOrEmpty(accessToken.Token))
                {
                    throw new Exception(ExceptionConstants.SomethingWentWrongMessageConstant);
                }

                return accessToken.Token;

            }
            catch (Exception ex)
            {
                logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodFailed, nameof(GetIbbsAiTokenAsync), DateTime.UtcNow, ex.Message));
                throw;
            }
            finally
            {
                logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodEnded, nameof(GetIbbsAiTokenAsync), DateTime.UtcNow, IbbsConstants.IbbsAIConstant));
            }
        }
    }

}

