// *********************************************************************************
//	<copyright file="SyncUsersDataFromAD.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>Sync users data from azure AD to IBBS DB.</summary>
// *********************************************************************************

namespace InternetBulletin.Functions
{
    using InternetBulletin.Business.Contracts;
    using InternetBulletin.Shared.Constants;
    using Microsoft.Azure.Functions.Worker;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Sync users data from azure AD to IBBS DB.
    /// </summary>
    /// <param name="loggerFactory">The logger factory.</param>
    /// <param name="usersService">The users service.</param>
    public class SyncUsersDataFromAD(ILoggerFactory loggerFactory, IUsersService usersService)
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger _logger = loggerFactory.CreateLogger<SyncUsersDataFromAD>();

        /// <summary>
        /// The users service.
        /// </summary>
        private readonly IUsersService _usersService = usersService;

        /// <summary>
        /// Main Function
        /// </summary>
        /// <param name="timerInfo">The timer.</param>
        [Function("SyncUsersDataFromAD")]
        public async Task Run([TimerTrigger("12:00:00")] TimerInfo timerInfo)
        {
            try
            {
                this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodStart, nameof(SyncUsersDataFromAD), DateTime.UtcNow, string.Empty));
                await this._usersService.SaveUsersDataFromAzureAdAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodFailed, nameof(SyncUsersDataFromAD), DateTime.UtcNow, ex.Message));
                throw;
            }
            finally
            {
                this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodEnded, nameof(SyncUsersDataFromAD), DateTime.UtcNow, string.Empty));
            }
        }
    }
}

