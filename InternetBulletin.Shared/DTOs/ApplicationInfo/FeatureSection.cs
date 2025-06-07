// *********************************************************************************
//	<copyright file="FeatureSection.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The feature section.</summary>
// *********************************************************************************

namespace InternetBulletin.Shared.DTOs.ApplicationInfo
{
	using MongoDB.Bson.Serialization.Attributes;

	/// <summary>
	/// The feature section.
	/// </summary>
	public class FeatureSection
	{
		/// <summary>
		/// The feature section title.
		/// </summary>
		[BsonElement("title")]
		public string Title { get; set; } = string.Empty;

		/// <summary>
		/// The features section items.
		/// </summary>
		[BsonElement("items")]
		public List<string> Items { get; set; } = [];
	}
}
