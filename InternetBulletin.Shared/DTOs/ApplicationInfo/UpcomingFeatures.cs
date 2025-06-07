// *********************************************************************************
//	<copyright file="UpcomingFeatures.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The upcoming features.</summary>
// *********************************************************************************

namespace InternetBulletin.Shared.DTOs.ApplicationInfo
{
	using MongoDB.Bson.Serialization.Attributes;

	/// <summary>
	/// The upcoming features.
	/// </summary>
	public class UpcomingFeatures
	{
		/// <summary>
		/// The upcoming features title
		/// </summary>
		[BsonElement("title")]
		public string Title { get; set; } = string.Empty;

		/// <summary>
		/// The upocoming features sections.
		/// </summary>
		[BsonElement("sections")]
		public List<FeatureSection> Sections { get; set; } = [];
	}
}
