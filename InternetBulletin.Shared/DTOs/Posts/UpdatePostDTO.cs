// *********************************************************************************
//	<copyright file="UpdatePostDTO.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>Update post d t o.</summary>
// *********************************************************************************

namespace InternetBulletin.Shared.DTOs.Posts
{
	/// <summary>
	/// Update post d t o.
	/// </summary>
	public class UpdatePostDTO
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
		/// Gets or sets the content of the post.
		/// </summary>
		/// <value>
		/// The content of the post.
		/// </value>
		public string PostContent { get; set; } = string.Empty;

		/// <summary>
		/// Gets or sets the post rating.
		/// </summary>
		/// <value>
		/// The post rating.
		/// </value>
		public int? PostRating { get; set; }
	}
}