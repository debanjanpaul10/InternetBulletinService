// *********************************************************************************
//	<copyright file="IUsersDataService.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>Users data service interface.</summary>
// *********************************************************************************

namespace InternetBulletin.Data.DataServices
{
	using DnsClient.Internal;
	using InternetBulletin.Data.Contracts;
	using InternetBulletin.Shared.Constants;
	using InternetBulletin.Shared.DTOs.Users;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.Logging;
	using Newtonsoft.Json;

	/// <summary>
	/// Users data service interface.
	/// </summary>
	/// <param name="dbContext">The database context.</param>
	/// <param name="logger">The logger.</param>
	/// <seealso cref="IUsersDataService"/>
	public class UsersDataService(SqlDbContext dbContext, ILogger<UsersDataService> logger) : IUsersDataService
	{
		/// <summary>
		/// The db context.
		/// </summary>
		private readonly SqlDbContext _dbContext = dbContext;

		/// <summary>
		/// The logger
		/// </summary>
		private readonly ILogger<UsersDataService> _logger = logger; 

		/// <summary>
		/// Saves users data async.
		/// </summary>
		/// <param name="usersData">The users data.</param>
		/// <returns>The boolean for success / failure</returns>
		public async Task<bool> SaveUsersDataAsync(List<GraphUserDTO> usersData)
		{
			try
			{
				this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodStart, nameof(SaveUsersDataAsync), DateTime.UtcNow, JsonConvert.SerializeObject(usersData)));

				var existingUsersData = await this._dbContext.Users.Where(x => x.IsActive).ToListAsync();

				// Get the IDs of existing users
				var existingUserIds = existingUsersData.Select(x => x.Id).ToHashSet();

				// Filter out users that already exist
				var newUsers = usersData.Where(x => !existingUserIds.Contains(x.Id)).ToList();

				if (newUsers.Count == 0)
				{
					return true; // No new users to save
				}

				// Convert DTOs to User entities and add them to the context
				var usersToAdd = newUsers.Select(user => new Entities.User
				{
					Id = user.Id,
					DisplayName = user.DisplayName,
					EmailAddress = user.EmailAddress,
					UserName = user.UserName,
					IsActive = true,
					DateCreated = DateTime.UtcNow
				});

				await this._dbContext.Users.AddRangeAsync(usersToAdd);
				await this._dbContext.SaveChangesAsync();

				return true;
			}
			catch (Exception ex)
			{
				this._logger.LogError(ex, string.Format(LoggingConstants.LogHelperMethodFailed, nameof(SaveUsersDataAsync), DateTime.UtcNow, ex.Message));
				throw;
			}
			finally
			{
				this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodEnded, nameof(SaveUsersDataAsync), DateTime.UtcNow, JsonConvert.SerializeObject(usersData)));
			}

		}
	}

}

