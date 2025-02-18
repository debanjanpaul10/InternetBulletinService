﻿// *********************************************************************************
//	<copyright file="ExceptionConstants.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Exception Constants Class.</summary>
// *********************************************************************************
namespace InternetBulletin.Shared.Constants
{
	/// <summary>
	/// The Exception Constants Class.
	/// </summary>
	public static class ExceptionConstants
	{
		/// <summary>
		/// The user unauthorized message constant
		/// </summary>
		public static readonly string UserUnauthorizedMessageConstant = "User Not Authorized";

		/// <summary>
		/// The post not found message constant
		/// </summary>
		public static readonly string PostNotFoundMessageConstant = "It seems the post you are looking for does not exists anymore!";

		/// <summary>
		/// Something went wrong message constant
		/// </summary>
		public static readonly string SomethingWentWrongMessageConstant = "Oops! Something went wrong. Please try again after sometime.";

		/// <summary>
		/// The post exists message constant
		/// </summary>
		public static readonly string PostExistsMessageConstant = "A post with the generated ID already exists.";

		/// <summary>
		/// The null post message constant
		/// </summary>
		public static readonly string NullPostMessageConstant = "Attempted to add a null post.";

		/// <summary>
		/// The post identifier not present message constant
		/// </summary>
		public static readonly string PostIdNotPresentMessageConstant = "The provided postId is null or empty.";

		/// <summary>
		/// The post unique identifier not exists message constant
		/// </summary>
		public static readonly string PostGuidNotValidMessageConstant = "The provided postId is not a valid GUID.";

		/// <summary>
		/// The posts not present message constant
		/// </summary>
		public static readonly string PostsNotPresentMessageConstant = "There are no posts to be shown at the moment!";

		/// <summary>
		/// The user already exists message constant
		/// </summary>
		public static readonly string UserAlreadyExistsMessageConstant = "The given user alias and the user email is already being used!";

		/// <summary>
		/// The user does not exists message constant
		/// </summary>
		public static readonly string UserDoesNotExistsMessageConstant = "The user data does not exist with us anymore!";

		/// <summary>
		/// The user identifier not correct message constant
		/// </summary>
		public static readonly string UserIdNotCorrectMessageConstant = "The user id is not correct. Please enter a valid user id";

		/// <summary>
		/// The null user message constant
		/// </summary>
		public static readonly string NullUserMessageConstant = "Attempted to add null user data.";

		/// <summary>
		/// The users not present message constants
		/// </summary>
		public static readonly string UsersNotPresentMessageConstants = "There are no users registered.";
	}
}
