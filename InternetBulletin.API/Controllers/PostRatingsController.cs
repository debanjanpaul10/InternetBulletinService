// *********************************************************************************
//	<copyright file="PostsController.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Posts Controller Class.</summary>
// *********************************************************************************

namespace InternetBulletin.API.Controllers
{
    using InternetBulletin.Business.Contracts;
    using InternetBulletin.Shared.Constants;
    using InternetBulletin.Shared.DTOs.Posts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// The Post Ratings Controller Class.
    /// </summary>
    /// <seealso cref="InternetBulletin.API.Controllers.BaseController" />
    /// <param name="httpContextAccessor">The http context accessor.</param>
    /// <param name="logger">The Logger.</param>
    /// <param name="postRatingsService">The posts ratings service.</param>
    [ApiController]
    [Route(RouteConstants.PostRatingsBase_RoutePrefix)]
    public class PostRatingsController(ILogger<PostRatingsController> logger, IHttpContextAccessor httpContextAccessor, IPostRatingsService postRatingsService) : BaseController(httpContextAccessor)
    {
        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<PostRatingsController> _logger = logger;

        /// <summary>
        /// The post ratings service.
        /// </summary>
        private readonly IPostRatingsService _postRatingsService = postRatingsService;

        /// <summary>
        /// Gets all the user ratings async
        /// </summary>
        /// <returns>The action result of the ratings data</returns>
        [HttpGet]
        [Route(RouteConstants.GetAllUserRatings_Route)]
        public async Task<IActionResult> GetAllUserRatingsAsync()
        {
            try
            {
                this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodStart, nameof(UpdateRatingAsync), DateTime.UtcNow, this.UserFullName ?? string.Empty));
                if (this.IsAuthorized())
                {
                    var result = await this._postRatingsService.GetAllUserPostRatingsAsync(this.UserName);
                    if (result is not null)
                    {
                        return this.HandleSuccessResult(result);
                    }
                    else
                    {
                        return this.HandleBadRequest(ExceptionConstants.UnableToGetUserPostRatingsMessageConstant);
                    }
                }

                return this.HandleUnAuthorizedRequest();
            }
            catch (Exception ex)
            {
                this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodFailed, nameof(GetAllUserRatingsAsync), DateTime.UtcNow, ex.Message));
                throw;
            }
            finally
            {
                this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodEnded, nameof(GetAllUserRatingsAsync), DateTime.UtcNow, this.UserFullName ?? string.Empty));
            }
        }

        /// <summary>
        /// Updates the rating of the post asynchronously.
        /// </summary>
        /// <param name="postRating">The post rating.</param>
        /// <returns>The action result.</returns>
        [HttpPost]
        [Route(RouteConstants.UpdateRating_Route)]
        public async Task<IActionResult> UpdateRatingAsync(PostRatingDTO postRating)
        {
            try
            {
                this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodStart, nameof(UpdateRatingAsync), DateTime.UtcNow, postRating.PostId));
                if (this.IsAuthorized())
                {
                    var result = await this._postRatingsService.UpdateRatingAsync(postId: postRating.PostId, isIncrement: postRating.IsIncrement, userName: this.UserName);
                    if (result is not null)
                    {
                        return this.HandleSuccessResult(result);
                    }
                    else
                    {
                        return this.HandleBadRequest(ExceptionConstants.PostGuidNotValidMessageConstant);
                    }
                }

                return this.HandleUnAuthorizedRequest();
            }
            catch (Exception ex)
            {
                this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodFailed, nameof(UpdateRatingAsync), DateTime.UtcNow, ex.Message));
                throw;
            }
            finally
            {
                this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodEnded, nameof(UpdateRatingAsync), DateTime.UtcNow, postRating.PostId));
            }
        }
    }
}


