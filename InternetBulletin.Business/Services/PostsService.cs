// *********************************************************************************
//	<copyright file="PostsService.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Posts BusinessManager Class.</summary>
// *********************************************************************************

namespace InternetBulletin.Business.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using InternetBulletin.Business.Contracts;
    using InternetBulletin.Data.Contracts;
    using InternetBulletin.Data.Entities;
    using InternetBulletin.Shared.Constants;
    using InternetBulletin.Shared.DTOs;
    using InternetBulletin.Shared.DTOs.Posts;
    using InternetBulletin.Shared.Helpers;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// The Posts BusinessManager Class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    /// <param name="postsDataService">The Posts Data Service.</param>
    /// <param name="postRatingsDataService">The post ratings data service.</param>
    /// <seealso cref="IPostsService"/>
    public class PostsService(
        ILogger<PostsService> logger, IPostsDataService postsDataService, IPostRatingsDataService postRatingsDataService) : IPostsService
    {
        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<PostsService> _logger = logger;

        /// <summary>
        /// The posts data service
        /// </summary>
        private readonly IPostsDataService _postsDataService = postsDataService;

        /// <summary>
        /// The post ratings service.
        /// </summary>
        private readonly IPostRatingsDataService _postRatingsDataService = postRatingsDataService;

        /// <summary>
        /// Gets the post asynchronous.
        /// </summary>
        /// <param name="postId">The post identifier.</param>
        /// <param name="userName">The user name.</param>
        /// <returns>
        /// The specific post.
        /// </returns>
        public async Task<Post> GetPostAsync(string postId, string userName)
        {
            var postGuid = CommonUtilities.ValidateAndParsePostId(postId, this._logger);
            var result = await this._postsDataService.GetPostAsync(postGuid, userName, true);
            return CommonUtilities.ThrowIfNull(result, ExceptionConstants.PostNotFoundMessageConstant, this._logger);
        }

        /// <summary>
        /// Adds the new post asynchronous.
        /// </summary>
        /// <param name="newPost">The new post.</param>
        /// <returns>
        /// The boolean for success or failure.
        /// </returns>
        public async Task<bool> AddNewPostAsync(AddPostDTO newPost, string userName)
        {
            CommonUtilities.ThrowIfNull(newPost, ExceptionConstants.NullPostMessageConstant, this._logger);
            var result = await this._postsDataService.AddNewPostAsync(newPost, userName);
            return result;
        }

        /// <summary>
        /// Updates the post asynchronous.
        /// </summary>
        /// <param name="updatedPost">The updated post.</param>
        /// <returns>The updated post data.</returns>
        public async Task<Post> UpdatePostAsync(UpdatePostDTO updatedPost, string userName)
        {
            CommonUtilities.ThrowIfNull(updatedPost, ExceptionConstants.NullPostMessageConstant, this._logger);
            var result = await this._postsDataService.UpdatePostAsync(updatedPost, userName, false);
            return CommonUtilities.ThrowIfNull(result, ExceptionConstants.PostNotFoundMessageConstant, this._logger);
        }

        /// <summary>
        /// Deletes the post asynchronous.
        /// </summary>
        /// <param name="postId">The post identifier.</param>
        /// <param name="userName">The user name.</param>
        /// <returns>The boolean for success / failure</returns>
        public async Task<bool> DeletePostAsync(string postId, string userName)
        {
            var postGuid = CommonUtilities.ValidateAndParsePostId(postId, this._logger);
            var response = await this._postsDataService.DeletePostAsync(postGuid, userName);
            return response;
        }

        /// <summary>
        /// Gets all posts asynchronous.
        /// </summary>
        /// <param name="userName">The user name</param>
        /// <returns>The list of <see cref="PostWithRatingsDTO"/></returns>
        public async Task<List<PostWithRatingsDTO>> GetAllPostsAsync(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName)) // Consider IsNullOrWhiteSpace for robustness
            {
                var result = await this._postsDataService.GetAllPostsAsync();
                var postsData = result.Select(post => new PostWithRatingsDTO
                {
                    PostId = post.PostId,
                    PostTitle = post.PostTitle,
                    PostContent = post.PostContent,
                    PostCreatedDate = post.PostCreatedDate,
                    PostOwnerUserName = post.PostOwnerUserName,
                    Ratings = post.Ratings,
                    IsActive = post.IsActive,
                }).ToList();

                return postsData;
            }
            else
            {
                var postsData = await this._postRatingsDataService.GetAllPostsWithRatingsAsync(userName);
                return postsData;
            }
        }

    }
}
