// *********************************************************************************
//	<copyright file="IProfilesDataService.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Profiles Data Service Interface.</summary>
// *********************************************************************************

namespace InternetBulletin.Data.Contracts
{
	using InternetBulletin.Data.Entities;
	using InternetBulletin.Shared.DTOs.Users;

	/// <summary>
	/// The Profiles Data Service Interface.
	/// </summary>
	public interface IProfilesDataService
	{
		/// <summary>
		/// Gets all posts and ratings for user async.
		/// </summary>
		/// <param name="userName">The user name.</param>
		/// <returns>The user posts and ratings data.</returns>
		Task<List<Post>> GetUserPostsAsync(string userName);

		/// <summary>
		/// Gets user ratings async.
		/// </summary>
		/// <param name="userName">The user name.</param>
		/// <returns>The list of user post ratings data</returns>
		Task<List<UserPostRatingDTO>> GetUserRatingsAsync(string userName);
	}
}
