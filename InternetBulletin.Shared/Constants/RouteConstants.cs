// *********************************************************************************
//	<copyright file="RouteConstants.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Route Constants Class.</summary>
// *********************************************************************************

namespace InternetBulletin.Shared.Constants
{
	/// <summary>
	/// The Route Constants Class.
	/// </summary>
	public static class RouteConstants
	{
		#region WEB

		/// <summary>
		/// The web controller URL route prefix
		/// </summary>
		public const string WebControllerUrl_RoutePrefix = "InternetBulletinWeb";

		/// <summary>
		/// The get resource URL route prefix
		/// </summary>
		public const string GetResourceUrl_RoutePrefix = "GetResourceUrl";

		/// <summary>
		/// The post resource URL route prefix
		/// </summary>
		public const string PostResourceUrl_RoutePrefix = "PostResourceUrl";

		/// <summary>
		/// The rewrite story with ai url route.
		/// </summary>
		public const string RewriteStoryWithAiUrl_Route = "RewriteStoryWithAi";

		#endregion

		#region API

		#region Posts

		/// <summary>
		/// The posts base route prefix
		/// </summary>
		public const string PostsBase_RoutePrefix = "Posts";

		/// <summary>
		/// The get post route
		/// </summary>
		public const string GetPost_Route = "GetPost";

		/// <summary>
		/// The get all posts route
		/// </summary>
		public const string GetAllPosts_Route = "GetAllPosts";

		/// <summary>
		/// Creates new post route.
		/// </summary>
		public const string NewPost_Route = "AddPost";

		/// <summary>
		/// The update post route
		/// </summary>
		public const string UpdatePost_Route = "UpdatePost";

		/// <summary>
		/// The delete post route
		/// </summary>
		public const string DeletePost_Route = "DeletePost";

		#endregion

		#region PostRatings

		/// <summary>
		/// The post ratings base_ route prefix.
		/// </summary>
		public const string PostRatingsBase_RoutePrefix = "PostRatings";

		/// <summary>
		/// The update vote route.
		/// </summary>
		public const string UpdateRating_Route = "UpdateRating";

		/// <summary>
		/// The get all user ratings route.
		/// </summary>
		public const string GetAllUserRatings_Route = "GetAllUserRatings";

		#endregion

		#region Users

		/// <summary>
		/// The users base route prefx
		/// </summary>
		public const string UsersBase_RoutePrefx = "Users";

		/// <summary>
		/// The get user route
		/// </summary>
		public const string GetUser_Route = "GetUser";

		/// <summary>
		/// The get all users route
		/// </summary>
		public const string GetAllUsers_Route = "GetAllUsers";

		/// <summary>
		/// Creates new user_route.
		/// </summary>
		public const string NewUser_Route = "NewUser";

		/// <summary>
		/// The update user route
		/// </summary>
		public const string UpdateUser_Route = "UpdateUser";

		/// <summary>
		/// The delete user route
		/// </summary>
		public const string DeleteUser_Route = "DeleteUser";

		#endregion

		#region Configuration

		/// <summary>
		/// The configuration base_ route prefix.
		/// </summary>
		public const string ConfigurationBase_RoutePrefix = "Configuration";

		/// <summary>
		/// The get configuration_ route.
		/// </summary>
		public const string GetConfiguration_Route = "GetConfiguration";

		#endregion

		#region Profiles

		/// <summary>
		/// The profiles base_ route prefix.
		/// </summary>
		public const string ProfilesBase_RoutePrefix = "Profiles";

		/// <summary>
		/// The get user profile data route.
		/// </summary>
		public const string GetUserProfileData_Route = "GetUserProfileData";

		#endregion

		#endregion

		#region External

		/// <summary>
		/// The rewrite text api route.
		/// </summary>
		public const string RewriteTextApi_Route = "BulletinAI/RewriteText";

		#endregion

		#region BulletinServices

		/// <summary>
		/// The bulletin services base_ route prefix.
		/// </summary>
		public const string BulletinServicesBase_RoutePrefix = "BulletinServices";

		/// <summary>
		/// The get users data from graph_ route.
		/// </summary>
		public const string GetUsersDataFromGraph_Route = "GetUsersDataGraph";

		/// <summary>
		/// The rewrite with ai route
		/// </summary>
		public const string RewriteWithAI_Route = "RewriteWithAI";

		/// <summary>
		/// The Application information data
		/// </summary>
		public const string GetAboutUsData_Route = "GetAboutUsData";

		#endregion
	}
}
