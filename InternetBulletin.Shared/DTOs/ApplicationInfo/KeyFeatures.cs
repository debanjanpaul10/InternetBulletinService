// *********************************************************************************
//	<copyright file="KeyFeatures.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Key features.</summary>
// *********************************************************************************

namespace InternetBulletin.Shared.DTOs.ApplicationInfo
{
	using MongoDB.Bson.Serialization.Attributes;

	/// <summary>
	/// The Key features.
	/// </summary>
	public class KeyFeatures
	{
		/// <summary>
		/// The key features title.
		/// </summary>
		[BsonElement("title")]
		public string Title { get; set; } = string.Empty;

		/// <summary>
		/// The key features section.
		/// </summary>
		[BsonElement("sections")]
		public List<FeatureSection> Sections { get; set; } = [];
	}
}
