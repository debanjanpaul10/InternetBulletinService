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
	/// The Posts Controller Class.
	/// </summary>
	/// <seealso cref="InternetBulletin.API.Controllers.BaseController" />
	/// <param name="httpContextAccessor">The http context accessor.</param>
	/// <param name="logger">The Logger.</param>
	/// <param name="postsService">The Posts Service.</param>
	[ApiController]
	[Route(RouteConstants.PostsBase_RoutePrefix)]
	public class PostsController(
		ILogger<PostsController> logger, IHttpContextAccessor httpContextAccessor, IPostsService postsService) : BaseController(httpContextAccessor)
	{
		/// <summary>
		/// The posts service
		/// </summary>
		private readonly IPostsService _postsService = postsService;

		/// <summary>
		/// The logger
		/// </summary>
		private readonly ILogger<PostsController> _logger = logger;

		/// <summary>
		/// Gets all posts data asynchronous.
		/// </summary>
		/// <returns>The action result.</returns>
		[HttpGet]
		[AllowAnonymous]
		[Route(RouteConstants.GetAllPosts_Route)]
		public async Task<IActionResult> GetAllPostsDataAsync()
		{
			try
			{
				this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodStart, nameof(GetPostAsync), DateTime.UtcNow, this.UserName ?? string.Empty));
				var result = await this._postsService.GetAllPostsAsync(this.UserName ?? string.Empty);
				if (result is not null && result.Count > 0)
				{
					return this.HandleSuccessResult(result);
				}
				else
				{
					return this.HandleBadRequest(ExceptionConstants.PostsNotPresentMessageConstant);
				}
			}
			catch (Exception ex)
			{
				this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodFailed, nameof(GetAllPostsDataAsync), DateTime.UtcNow, ex.Message));
				throw;
			}
			finally
			{
				this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodEnded, nameof(GetAllPostsDataAsync), DateTime.UtcNow, this.UserName ?? string.Empty));
			}
		}


		/// <summary>
		/// Gets the post asynchronous.
		/// </summary>
		/// <param name="postId">The post identifier.</param>
		/// <returns>The action result.</returns>
		[HttpGet]
		[Route(RouteConstants.GetPost_Route)]
		public async Task<IActionResult> GetPostAsync(string postId)
		{
			try
			{
				this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodStart, nameof(GetPostAsync), DateTime.UtcNow, postId));
				if (this.IsAuthorized())
				{
					var result = await this._postsService.GetPostAsync(postId, this.UserName);
					if (result is not null && !Equals(result.PostId, Guid.Empty))
					{
						return this.HandleSuccessResult(result);
					}
					else
					{
						return this.HandleBadRequest(ExceptionConstants.PostNotFoundMessageConstant);
					}
				}

				return this.HandleUnAuthorizedRequest();

			}
			catch (Exception ex)
			{
				this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodFailed, nameof(GetPostAsync), DateTime.UtcNow, ex.Message));
				throw;
			}
			finally
			{
				this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodEnded, nameof(GetPostAsync), DateTime.UtcNow, postId));
			}
		}

		/// <summary>
		/// Adds the new post asynchronous.
		/// </summary>
		/// <param name="newPost">The new post.</param>
		/// <returns>The action result of JSON response.</returns>
		[HttpPost]
		[Route(RouteConstants.NewPost_Route)]
		public async Task<IActionResult> AddNewPostAsync(AddPostDTO newPost)
		{
			try
			{
				this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodStart, nameof(AddNewPostAsync), DateTime.UtcNow, newPost.PostTitle));
				if (this.IsAuthorized())
				{
					var result = await this._postsService.AddNewPostAsync(newPost, this.UserName);
					if (result)
					{
						return this.HandleSuccessResult(result);
					}
					else
					{
						return this.HandleBadRequest(ExceptionConstants.SomethingWentWrongMessageConstant);
					}
				}

				return this.HandleUnAuthorizedRequest();
			}
			catch (Exception ex)
			{
				this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodFailed, nameof(AddNewPostAsync), DateTime.UtcNow, ex.Message));
				throw;
			}
			finally
			{
				this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodEnded, nameof(AddNewPostAsync), DateTime.UtcNow, newPost.PostTitle));
			}
		}

		/// <summary>
		/// Updates the post asynchronous.
		/// </summary>
		/// <param name="updatePost">The update post.</param>
		/// <returns>The action result of the JSON response.</returns>
		[HttpPost]
		[Route(RouteConstants.UpdatePost_Route)]
		public async Task<IActionResult> UpdatePostAsync(UpdatePostDTO updatePost)
		{
			try
			{
				this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodStart, nameof(UpdatePostAsync), DateTime.UtcNow, updatePost.PostId));
				if (this.IsAuthorized())
				{
					var result = await this._postsService.UpdatePostAsync(updatePost, this.UserName);
					if (result is not null && result.PostId != Guid.Empty)
					{
						return this.HandleSuccessResult(result);
					}
					else
					{
						return this.HandleBadRequest(ExceptionConstants.SomethingWentWrongMessageConstant);
					}
				}

				return this.HandleUnAuthorizedRequest();
			}
			catch (Exception ex)
			{
				this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodFailed, nameof(UpdatePostAsync), DateTime.UtcNow, ex.Message));
				throw;
			}
			finally
			{
				this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodEnded, nameof(UpdatePostAsync), DateTime.UtcNow, updatePost.PostId));
			}
		}

		/// <summary>
		/// Deletes the post asynchronous.
		/// </summary>
		/// <param name="postId">The post identifier.</param>
		/// <returns>The action result of the JSON response.</returns>
		[HttpPost]
		[Route(RouteConstants.DeletePost_Route)]
		public async Task<IActionResult> DeletePostAsync(string postId)
		{
			try
			{
				this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodStart, nameof(DeletePostAsync), DateTime.UtcNow, postId));
				if (this.IsAuthorized())
				{
					var result = await this._postsService.DeletePostAsync(postId, this.UserName);
					if (result)
					{
						return this.HandleSuccessResult(result);
					}
					else
					{
						return this.HandleBadRequest(ExceptionConstants.SomethingWentWrongMessageConstant);
					}
				}

				return this.HandleUnAuthorizedRequest();
			}
			catch (Exception ex)
			{
				this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodFailed, nameof(DeletePostAsync), DateTime.UtcNow, ex.Message));
				throw;
			}
			finally
			{
				this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodEnded, nameof(DeletePostAsync), DateTime.UtcNow, postId));
			}
		}
	}
}
