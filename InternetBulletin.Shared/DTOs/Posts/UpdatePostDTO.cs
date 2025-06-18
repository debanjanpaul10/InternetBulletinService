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

		/// <summary>
		/// Gets or sets a value indicating whether this instance is NSFW.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is NSFW; otherwise, <c>false</c>.
		/// </value>
		public bool IsNSFW { get; set; }

		/// <summary>
		/// Gets or sets the genre tag.
		/// </summary>
		/// <value>
		/// The genre tag.
		/// </value>
		public string GenreTag { get; set; } = string.Empty;
	}
}