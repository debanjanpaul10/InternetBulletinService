// *********************************************************************************
//	<copyright file="IPostRatingsService.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>Post ratings service interface.</summary>
// *********************************************************************************

namespace InternetBulletin.Business.Services
{
    using System;
    using System.Globalization;
    using System.Threading.Tasks;
    using InternetBulletin.Business.Contracts;
    using InternetBulletin.Data.Contracts;
    using InternetBulletin.Data.Entities;
    using InternetBulletin.Shared.Constants;
    using InternetBulletin.Shared.DTOs.Posts;
    using InternetBulletin.Shared.Helpers;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Post ratings service.
    /// </summary>
    /// <param name="logger">The logger.</param>
    /// <param name="postRatingsDataService">The post ratings data service.</param>
    /// <param name="postsDataService">The posts data service.</param>
    /// <seealso cref="IPostRatingsService"/>
    public class PostRatingsService(ILogger<PostRatingsService> logger, IPostRatingsDataService postRatingsDataService, IPostsDataService postsDataService) : IPostRatingsService
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger<PostRatingsService> _logger = logger;

        /// <summary>
        /// The post ratings data service.
        /// </summary>
        private readonly IPostRatingsDataService _postRatingsDataService = postRatingsDataService;

        /// <summary>
        /// The posts data service.
        /// </summary>
        private readonly IPostsDataService _postsDataService = postsDataService;

        /// <summary>
        /// Updates rating async.
        /// </summary>
        /// <param name="postId">The post id.</param>
        /// <param name="isIncrement">If the rating is increased.</param>
        /// <param name="userName">The current user name</param>
        /// <returns>The update rating data dto.</returns>
        public async Task<UpdateRatingDto> UpdateRatingAsync(string postId, bool isIncrement, string userName)
        {
            var postIdGuid = CommonUtilities.ValidateAndParsePostId(postId, this._logger);
            var (post, postRating) = await this.GetPostAndRatingAsync(postIdGuid, userName);
            CommonUtilities.ThrowIfNull(post, ExceptionConstants.PostNotFoundMessageConstant, this._logger);

            var updatedRating = await this.HandleRatingAsync(post, postIdGuid, userName, postRating);
            return updatedRating;
        }

        #region PRIVATE Methods

        /// <summary>
        /// Gets post and rating async.
        /// </summary>
        /// <param name="postIdGuid">The post id guid.</param>
        /// <param name="userName">The user name.</param>
        /// <returns>The tupple containing post and post rating.</returns>
        private async Task<(Post post, PostRating postRating)> GetPostAndRatingAsync(Guid postIdGuid, string userName)
        {
            var postTask = this._postsDataService.GetPostAsync(postIdGuid, userName, false);
            var postRatingTask = this._postRatingsDataService.GetPostRatingAsync(postIdGuid, userName);
            await Task.WhenAll(postTask, postRatingTask).ConfigureAwait(false);
            return (postTask.Result, postRatingTask.Result);
        }

        /// <summary>
        /// Handles both first time and already rated logic.
        /// If this is the first time, then add a new entry for the post ratings.
        /// If this is not the first time, then increment/decrement the rating value based on the previous value.
        /// If the postrating is null meaning the first time.
        /// </summary>
        /// <param name="post">The post data</param>
        /// <param name="postIdGuid">The post id guid.</param>
        /// <param name="postRating">The post rating.</param>
        /// <param name="userName">The user name.</param>
        private async Task<UpdateRatingDto> HandleRatingAsync(Post post, Guid postIdGuid, string userName, PostRating? postRating)
        {
            if (postRating is not null && !string.IsNullOrEmpty(Convert.ToString(postRating.PostId, CultureInfo.CurrentCulture)))
            {
                if (postRating.PreviousRatingValue == 0)
                {
                    post.Ratings += 1;
                    postRating.PreviousRatingValue = 1;
                }
                else
                {
                    post.Ratings = Math.Max(0, post.Ratings - 1);
                    postRating.PreviousRatingValue = 0;
                }

                await this._postsDataService.UpdatePostAsync(CreateUpdatePostDTO(post), userName);
                await this._postRatingsDataService.UpdatePostRatingAsync(postRating);
                return new UpdateRatingDto { HasAlreadyUpdated = true, IsUpdateSuccess = true };
            }
            else
            {
                post.Ratings += 1;
                var newRating = new PostRating
                {
                    PostId = postIdGuid,
                    UserName = userName,
                    RatedOn = DateTime.UtcNow,
                    IsActive = true,
                    PreviousRatingValue = 1
                };

                await this._postsDataService.UpdatePostAsync(CreateUpdatePostDTO(post), userName);
                await this._postRatingsDataService.AddPostRatingAsync(newRating);
                return new UpdateRatingDto { HasAlreadyUpdated = false, IsUpdateSuccess = true };
            }
        }

        /// <summary>
        /// Creates update post dto.
        /// </summary>
        /// <param name="post">The post.</param>
        private static UpdatePostDTO CreateUpdatePostDTO(Post post)
        {
            return new UpdatePostDTO
            {
                PostContent = post.PostContent,
                PostRating = post.Ratings,
                PostId = post.PostId,
                PostTitle = post.PostTitle
            };
        }

        #endregion
    }

}

