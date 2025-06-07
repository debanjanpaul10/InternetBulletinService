// *********************************************************************************
//	<copyright file="UsageSection.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The usage section.</summary>
// *********************************************************************************

namespace InternetBulletin.Shared.DTOs.ApplicationInfo
{
	using MongoDB.Bson.Serialization.Attributes;

	/// <summary>
	/// The usage section.
	/// </summary>
	public class UsageSection
	{
		/// <summary>
		/// The usage section title.
		/// </summary>
		[BsonElement("title")]
		public string Title { get; set; } = string.Empty;

		/// <summary>
		/// The usage content.
		/// </summary>
		[BsonElement("content")]
		public string Content { get; set; } = string.Empty;

		/// <summary>
		/// The usage section items.
		/// </summary>
		[BsonElement("items")]
		public List<string> Items { get; set; } = [];
	}
}
