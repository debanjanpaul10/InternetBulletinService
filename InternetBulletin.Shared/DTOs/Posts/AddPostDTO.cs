// *********************************************************************************
//	<copyright file="AddPostDTO.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>Add post dto.</summary>
// *********************************************************************************

namespace InternetBulletin.Shared.DTOs.Posts
{
	/// <summary>
	/// Add post dto.
	/// </summary>
	public class AddPostDTO
	{
		/// <summary>
		/// Gets or sets the post title.
		/// </summary>
		/// <value>
		/// The post title.
		/// </value>
		public string PostTitle { get; set; } = string.Empty;

		/// <summary>
		/// Gets or sets the content of the post.
		/// </summary>
		/// <value>
		/// The content of the post.
		/// </value>
		public string PostContent { get; set; } = string.Empty;

		/// <summary>
		/// Gets or sets the post created by.
		/// </summary>
		/// <value>
		/// The post created by.
		/// </value>
		public string PostCreatedBy { get; set; } = string.Empty;

	}
}