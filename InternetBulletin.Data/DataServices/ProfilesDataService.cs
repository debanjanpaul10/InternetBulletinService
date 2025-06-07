// *********************************************************************************
//	<copyright file="ProfilesDataService.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Profiles Data Service Class.</summary>
// *********************************************************************************

namespace InternetBulletin.Data.DataServices
{
    using InternetBulletin.Data.Contracts;
    using InternetBulletin.Data.Entities;
    using InternetBulletin.Shared.Constants;
    using InternetBulletin.Shared.DTOs.Users;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// The Profiles Data Service Class.
    /// </summary>
    /// <param name="dbContext">The SQL DB Context</param>
    /// <param name="logger">The logger.</param>
    /// <seealso cref="IProfilesDataService"/>
    public class ProfilesDataService(SqlDbContext dbContext, ILogger<ProfilesDataService> logger) : IProfilesDataService
    {
        /// <summary>
        /// The sql db context.
        /// </summary>
        private readonly SqlDbContext _dbContext = dbContext;

        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger<ProfilesDataService> _logger = logger;

        /// <summary>
        /// Gets user posts async.
        /// </summary>
        /// <param name="userName">The user name.</param>
        /// <returns>The list of posts data dto</returns>
        public async Task<List<Post>> GetUserPostsAsync(string userName)
        {
            try
            {
                this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodStart, nameof(GetUserPostsAsync), DateTime.UtcNow, userName));
                var userPosts = await this._dbContext.Posts.Where(p => p.PostOwnerUserName == userName && p.IsActive).ToListAsync();

                return userPosts;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, string.Format(LoggingConstants.LogHelperMethodFailed, nameof(GetUserPostsAsync), DateTime.UtcNow, ex.Message));
                throw;
            }
            finally
            {
                this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodEnded, nameof(GetUserPostsAsync), DateTime.UtcNow, userName));
            }
        }

        /// <summary>
        /// Gets user ratings async.
        /// </summary>
        /// <param name="userName">The user name.</param>
        /// <returns>The list of user post ratings data</returns>
        public async Task<List<UserPostRatingDTO>> GetUserRatingsAsync(string userName)
        {
            try
            {
                this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodStart, nameof(GetUserRatingsAsync), DateTime.UtcNow, userName));
                var userRatings = await this._dbContext.PostRatings
                    .Where(pr => pr.UserName == userName && pr.IsActive && pr.RatingValue == 1)
                    .Join(
                        this._dbContext.Posts.Where(p => p.IsActive),
                        pr => pr.PostId,
                        p => p.PostId,
                        (pr, p) => new UserPostRatingDTO
                        {
                            PostName = p.PostTitle,
                            RatedOn = pr.RatedOn,
                            CurrentRatingValue = pr.RatingValue
                        }
                    ).ToListAsync();

                return userRatings;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, string.Format(LoggingConstants.LogHelperMethodFailed, nameof(GetUserRatingsAsync), DateTime.UtcNow, ex.Message));
                throw;
            }
            finally
            {
                this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodEnded, nameof(GetUserRatingsAsync), DateTime.UtcNow, userName));
            }
        }

    }
}