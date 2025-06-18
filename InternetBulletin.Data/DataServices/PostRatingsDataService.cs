// *********************************************************************************
//	<copyright file="PostRatingsDataService.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>Post ratings data service class.</summary>
// *********************************************************************************

namespace InternetBulletin.Data.DataServices
{
    using InternetBulletin.Data.Contracts;
    using InternetBulletin.Data.Entities;
    using InternetBulletin.Shared.Constants;
    using InternetBulletin.Shared.DTOs.Posts;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Post ratings data service class.
    /// </summary>
    /// <param name="logger">The application logger</param>
    /// <param name="sqlDbContext">The sql db context</param>
    /// <seealso cref="IPostRatingsDataService"/>
    public class PostRatingsDataService(SqlDbContext sqlDbContext, ILogger<PostRatingsDataService> logger) : IPostRatingsDataService
    {
        /// <summary>
        /// The db context.
        /// </summary>
        private readonly SqlDbContext _dbContext = sqlDbContext;

        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger<PostRatingsDataService> _logger = logger;

        /// <summary>
        /// Gets all user post ratings async.
        /// </summary>
        /// <param name="userName">The user name.</param>
        /// <returns>The list of post ratings</returns>
        public async Task<List<PostRating>> GetAllUserPostRatingsAsync(string userName)
        {
            try
            {
                this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodStart, nameof(GetAllUserPostRatingsAsync), DateTime.UtcNow, string.Empty));
                var result = await this._dbContext.PostRatings.Where(r => r.UserName == userName && r.IsActive).ToListAsync();
                if (result is not null)
                {
                    return result;
                }
                else
                {
                    var exception = new Exception(ExceptionConstants.UnableToGetUserPostRatingsMessageConstant);
                    this._logger.LogError(exception, exception.Message);
                    throw exception;
                }
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, string.Format(LoggingConstants.LogHelperMethodFailed, nameof(GetAllUserPostRatingsAsync), DateTime.UtcNow, ex.Message));
                throw;
            }
            finally
            {
                this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodEnded, nameof(GetAllUserPostRatingsAsync), DateTime.UtcNow, string.Empty));
            }
        }

        /// <summary>
        /// Gets post rating async.
        /// </summary>
        /// <param name="postId">The post id.</param>
        /// <param name="userName">The user name.</param>
        /// <returns>The post rating data.</returns>
        public async Task<PostRating> GetPostRatingAsync(Guid postId, string userName)
        {
            try
            {
                this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodStart, nameof(GetPostRatingAsync), DateTime.UtcNow, string.Empty));

                var result = await this._dbContext.PostRatings.FirstOrDefaultAsync(r => r.PostId == postId && r.UserName == userName && r.IsActive);
                return result ?? new PostRating();
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, string.Format(LoggingConstants.LogHelperMethodFailed, nameof(GetPostRatingAsync), DateTime.UtcNow, ex.Message));
                throw;
            }
            finally
            {
                this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodEnded, nameof(GetPostRatingAsync), DateTime.UtcNow, string.Empty));
            }
        }

        /// <summary>
        /// Adds a new rating async.
        /// </summary>
        /// <param name="postRating">The post rating.</param>
        public async Task AddPostRatingAsync(PostRating postRating)
        {
            try
            {
                this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodStart, nameof(AddPostRatingAsync), DateTime.UtcNow, postRating.PostId));
                if (postRating is not null)
                {
                    await this._dbContext.PostRatings.AddAsync(postRating);
                    await this._dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, string.Format(LoggingConstants.LogHelperMethodFailed, nameof(AddPostRatingAsync), DateTime.UtcNow, ex.Message));
                throw;
            }
            finally
            {
                this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodEnded, nameof(AddPostRatingAsync), DateTime.UtcNow, postRating.PostId));
            }
        }

        /// <summary>
        /// Updates an existing rating async.
        /// </summary>
        /// <param name="postRating">The post rating.</param>
        public async Task UpdatePostRatingAsync(PostRating postRating)
        {
            try
            {
                this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodStart, nameof(UpdatePostRatingAsync), DateTime.UtcNow, postRating.PostId));
                var existingPostRating = await this._dbContext.PostRatings.FirstOrDefaultAsync(r => r.PostId == postRating.PostId && r.UserName == postRating.UserName);
                if (existingPostRating is not null)
                {
                    existingPostRating.RatedOn = DateTime.UtcNow;
                    await this._dbContext.SaveChangesAsync();
                }
                else
                {
                    var exception = new Exception(ExceptionConstants.PostNotFoundMessageConstant);
                    this._logger.LogError(exception, exception.Message);
                    throw exception;
                }
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, string.Format(LoggingConstants.LogHelperMethodFailed, nameof(UpdatePostRatingAsync), DateTime.UtcNow, ex.Message));
                throw;
            }
            finally
            {
                this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodEnded, nameof(UpdatePostRatingAsync), DateTime.UtcNow, postRating.PostId));
            }
        }

        /// <summary>
        /// Gets all posts with ratings async.
        /// </summary>
        /// <param name="userName">The user name.</param>
        public async Task<List<PostWithRatingsDTO>> GetAllPostsWithRatingsAsync(string userName)
        {
            try
            {
                this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodStart, nameof(GetAllPostsWithRatingsAsync), DateTime.UtcNow, userName));

                var query = from post in this._dbContext.Posts
                            where post.IsActive
                            join rating in this._dbContext.PostRatings.Where(r => r.UserName == userName && r.IsActive)
                            on post.PostId equals rating.PostId into ratings
                            from rating in ratings.DefaultIfEmpty()
                            select new PostWithRatingsDTO
                            {
                                PostId = post.PostId,
                                PostTitle = post.PostTitle,
                                PostContent = post.PostContent,
                                PostCreatedDate = post.PostCreatedDate,
                                PostOwnerUserName = post.PostOwnerUserName,
                                Ratings = post.Ratings,
                                IsActive = post.IsActive,
                                RatingValue = rating != null ? rating.RatingValue : 0,
                                GenreTag = post.GenreTag,
                                IsNsfw = post.IsNSFW
                            };

                var result = await query.ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, string.Format(LoggingConstants.LogHelperMethodFailed, nameof(GetAllPostsWithRatingsAsync), DateTime.UtcNow, ex.Message));
                throw;
            }
            finally
            {
                this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodEnded, nameof(GetAllPostsWithRatingsAsync), DateTime.UtcNow, userName));
            }
        }
    }
}


