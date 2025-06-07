// *********************************************************************************
//	<copyright file="DatabaseConstants.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Database Constants Class.</summary>
// *********************************************************************************

namespace InternetBulletin.Shared.Constants
{
	/// <summary>
	/// The Database Constants Class.
	/// </summary>
	public static class DatabaseConstants
	{
		/// <summary>
		/// The integer data type constant
		/// </summary>
		public const string IntegerDataTypeConstant = "int";

		/// <summary>
		/// The n variable character maximum data type constant
		/// </summary>
		public const string NVarCharMaxDataTypeConstant = "nvarchar(max)";

		/// <summary>
		/// The bit data type constant
		/// </summary>
		public const string BitDataTypeConstant = "bit";

		/// <summary>
		/// The unique identifier data type constant.
		/// </summary>
		public const string UniqueIdentifierDataTypeConstant = "uniqueidentifier";

		/// <summary>
		/// The date time data type constant.
		/// </summary>
		public const string DateTimeDataTypeConstant = "datetime";

		/// <summary>
		/// The users table constant
		/// </summary>
		public const string UsersTableConstant = "Users";

		/// <summary>
		/// The posts table constant.
		/// </summary>
		public const string PostsTableConstant = "Posts";

		/// <summary>
		/// The primary key posts constant.
		/// </summary>
		public const string PrimaryKeyPostsConstant = "PK_Posts";

		/// <summary>
		/// The post ratings table name constant.
		/// </summary>
		public const string PostRatingsTableNameConstant = "PostRatings";

		/// <summary>
		/// The AI Usages table name constant.
		/// </summary>
		public const string AiUsagesTableNameConstant = "AiUsages";
	}
}
