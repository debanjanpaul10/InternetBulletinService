// *********************************************************************************
//	<copyright file="UserPostsDto.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The User Posts DTO.</summary>
// *********************************************************************************

namespace InternetBulletin.Shared.DTOs.Users
{
	/// <summary>
	/// The User Posts DTO.
	/// </summary>
	public class UserPostsDto
	{
		/// <summary>
		/// Gets or sets the post identifier.
		/// </summary>
		/// <value>
		/// The post identifier.
		/// </value>
		public Guid PostId { get; set; }

		/// <summary>
		/// Gets or sets the post title.
		/// </summary>
		/// <value>
		/// The post title.
		/// </value>
		public string PostTitle { get; set; } = string.Empty;

		/// <summary>
		/// Gets or sets the post created date.
		/// </summary>
		/// <value>
		/// The post created date.
		/// </value>
		public DateTime PostCreatedDate { get; set; }
	}
}
