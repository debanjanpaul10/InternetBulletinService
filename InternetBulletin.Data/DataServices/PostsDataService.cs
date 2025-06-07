// *********************************************************************************
//	<copyright file="PostsDataService.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Posts Data Manager Class.</summary>
// *********************************************************************************

namespace InternetBulletin.Data.DataServices
{
	using InternetBulletin.Data.Contracts;
	using InternetBulletin.Data.Entities;
	using InternetBulletin.Shared.Constants;
	using InternetBulletin.Shared.DTOs;
	using InternetBulletin.Shared.DTOs.Posts;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.Logging;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	/// <summary>
	/// The Posts DataManager Class.
	/// </summary>
	/// <param name="cosmosDbContext">The Cosmos DB Context.</param>
	/// <param name="logger">The Logger.</param>
	public class PostsDataService(SqlDbContext dbContext, ILogger<PostsDataService> logger) : IPostsDataService
	{
		/// <summary>
		/// The Cosmos database context
		/// </summary>
		private readonly SqlDbContext _dbContext = dbContext;

		/// <summary>
		/// The logger
		/// </summary>
		private readonly ILogger<PostsDataService> _logger = logger;

		/// <summary>
		/// Gets the post asynchronous. 
		/// </summary>
		/// <param name="postId">The post identifier.</param>
		/// <param name="userName">The user name.</param>
		/// <param name="isForCurrentUser">
		///     If the flag is true, then the post must belong to the current user
		///     else if it is false, then the post must not belong to the current user
		/// </param>
		/// <returns>
		/// The specific post.
		/// </returns>
		public async Task<Post> GetPostAsync(Guid postId, string userName, bool isForCurrentUser)
		{
			try
			{
				this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodStart, nameof(GetPostAsync), DateTime.UtcNow, postId));

				var query = _dbContext.Posts.Where(p => p.PostId == postId && p.IsActive);
				query = isForCurrentUser
					? query.Where(p => p.PostOwnerUserName == userName)
					: query.Where(p => p.PostOwnerUserName != userName);

				var result = await query.FirstOrDefaultAsync() ?? new Post();
				return result;
			}
			catch (Exception ex)
			{
				this._logger.LogError(ex, string.Format(LoggingConstants.LogHelperMethodFailed, nameof(GetPostAsync), DateTime.UtcNow, ex.Message));
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
		/// <returns>
		/// The boolean for success or failure.
		/// </returns>
		public async Task<bool> AddNewPostAsync(AddPostDTO newPost, string userName)
		{
			try
			{
				this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodStart, nameof(AddNewPostAsync), DateTime.UtcNow, newPost.PostTitle));

				var postId = Guid.NewGuid();
				var existingPost = await this._dbContext.Posts.AnyAsync(x => x.PostId == postId && x.IsActive);
				if (!existingPost)
				{
					var dbPostData = new Post()
					{
						PostId = postId,
						PostContent = newPost.PostContent,
						PostTitle = newPost.PostTitle,
						IsActive = true,
						PostCreatedDate = DateTime.UtcNow,
						PostOwnerUserName = userName,
						Ratings = 0
					};
					await this._dbContext.Posts.AddAsync(dbPostData);
					await this._dbContext.SaveChangesAsync();
					return true;
				}
				else
				{
					var exception = new Exception(ExceptionConstants.PostExistsMessageConstant);
					this._logger.LogError(exception, exception.Message);

					throw exception;
				}
			}
			catch (DbUpdateException dbEx)
			{
				this._logger.LogError(dbEx, string.Format(LoggingConstants.LogHelperMethodFailed, nameof(AddNewPostAsync), DateTime.UtcNow, dbEx.Message));
				return false;
			}
			catch (Exception ex)
			{
				this._logger.LogError(ex, string.Format(LoggingConstants.LogHelperMethodFailed, nameof(AddNewPostAsync), DateTime.UtcNow, ex.Message));
				return false;
			}
			finally
			{
				this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodEnded, nameof(AddNewPostAsync), DateTime.UtcNow, newPost.PostTitle));
			}
		}

		/// <summary>
		/// Updates the post asynchronous.
		/// </summary>
		/// <param name="updatedPost">The updated post.</param>
		/// <param name="userName">The user name</param>
		/// <param name="isRatingUpdate">The boolean flag to signify rating update.</param>
		/// <returns>The updated post data.</returns>
		public async Task<Post> UpdatePostAsync(UpdatePostDTO updatedPost, string userName, bool isRatingUpdate)
		{
			try
			{
				this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodStart, nameof(AddNewPostAsync), DateTime.UtcNow, updatedPost.PostId));

				if (isRatingUpdate)
				{
					return await this.HandleRatingUpdateForPostAsync(updatedPost);
				}
				else
				{
					var dbPostData = await this._dbContext.Posts.FirstOrDefaultAsync(x => x.PostId == updatedPost.PostId && x.IsActive && x.PostOwnerUserName == userName);
					if (dbPostData is not null)
					{
						dbPostData.PostTitle = updatedPost.PostTitle;
						dbPostData.PostContent = updatedPost.PostContent;

						await this._dbContext.SaveChangesAsync();
						return dbPostData;
					}
					else
					{
						var exception = new Exception(ExceptionConstants.PostNotFoundMessageConstant);
						this._logger.LogError(exception, exception.Message);
						throw exception;
					}
				}

			}
			catch (DbUpdateException dbEx)
			{
				this._logger.LogError(dbEx, string.Format(LoggingConstants.LogHelperMethodFailed, nameof(UpdatePostAsync), DateTime.UtcNow, dbEx.Message));
				return new Post();
			}
			catch (Exception ex)
			{
				this._logger.LogError(ex, string.Format(LoggingConstants.LogHelperMethodFailed, nameof(UpdatePostAsync), DateTime.UtcNow, ex.Message));
				return new Post();
			}
			finally
			{
				this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodEnded, nameof(UpdatePostAsync), DateTime.UtcNow, updatedPost.PostId));
			}
		}

		/// <summary>
		/// Deletes the post asynchronous.
		/// </summary>
		/// <param name="postId">The post identifier.</param>
		/// <param name="userName">The user name.</param>
		/// <returns>
		/// The boolean for success / failure
		/// </returns>
		public async Task<bool> DeletePostAsync(Guid postId, string userName)
		{
			try
			{
				this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodStart, nameof(DeletePostAsync), DateTime.UtcNow, postId));
				var dbPostData = await this._dbContext.Posts.FirstOrDefaultAsync(post => post.PostId == postId && post.IsActive && post.PostOwnerUserName == userName);
				if (dbPostData is not null)
				{
					dbPostData.IsActive = false;
					await this._dbContext.SaveChangesAsync();

					return true;
				}
				else
				{
					var exception = new Exception(ExceptionConstants.PostNotFoundMessageConstant);
					this._logger.LogError(exception, exception.Message);
					throw exception;
				}

			}
			catch (DbUpdateException dbEx)
			{
				this._logger.LogError(dbEx, string.Format(LoggingConstants.LogHelperMethodFailed, nameof(DeletePostAsync), DateTime.UtcNow, dbEx.Message));
				return false;
			}
			catch (Exception ex)
			{
				this._logger.LogError(ex, string.Format(LoggingConstants.LogHelperMethodFailed, nameof(DeletePostAsync), DateTime.UtcNow, ex.Message));
				return false;
			}
			finally
			{
				this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodEnded, nameof(DeletePostAsync), DateTime.UtcNow, postId));
			}
		}

		/// <summary>
		/// Gets all posts async.
		/// </summary>
		/// <returns>The list of posts</returns>
		public async Task<List<Post>> GetAllPostsAsync()
		{
			try
			{
				this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodStart, nameof(GetAllPostsAsync), DateTime.UtcNow, string.Empty));

				var result = await this._dbContext.Posts.Where(x => x.IsActive).ToListAsync();
				return result;
			}
			catch (Exception ex)
			{
				this._logger.LogError(ex, string.Format(LoggingConstants.LogHelperMethodFailed, nameof(GetAllPostsAsync), DateTime.UtcNow, ex.Message));
				throw;
			}
			finally
			{
				this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodEnded, nameof(GetAllPostsAsync), DateTime.UtcNow, string.Empty));
			}
		}

		#region PRIVATE Methods

		/// <summary>
		/// Handles rating update for post async.
		/// </summary>
		/// <param name="updatedPost">The updated post.</param>
		/// <returns>The updated post</returns>
		private async Task<Post> HandleRatingUpdateForPostAsync(UpdatePostDTO updatedPost)
		{
			var dbPostData = await this._dbContext.Posts.FirstOrDefaultAsync(x => x.PostId == updatedPost.PostId && x.IsActive);
			if (dbPostData is not null)
			{
				if (updatedPost.PostRating.HasValue)
				{
					dbPostData.Ratings = updatedPost.PostRating.Value;
				}

				await this._dbContext.SaveChangesAsync();
				return dbPostData;
			}
			else
			{
				var exception = new Exception(ExceptionConstants.PostNotFoundMessageConstant);
				this._logger.LogError(exception, exception.Message);
				throw exception;
			}
		}

		#endregion

	}
}
