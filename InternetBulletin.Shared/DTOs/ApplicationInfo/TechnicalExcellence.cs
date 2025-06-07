// *********************************************************************************
//	<copyright file="TechnicalExcellence.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Technical Excellence.</summary>
// *********************************************************************************

namespace InternetBulletin.Shared.DTOs.ApplicationInfo
{
	using MongoDB.Bson.Serialization.Attributes;

	/// <summary>
	/// The Technical Excellence.
	/// </summary>
	public class TechnicalExcellence
	{
		/// <summary>
		/// The technical excellence title.
		/// </summary>
		[BsonElement("title")]
		public string Title { get; set; } = string.Empty;

		/// <summary>
		/// The technical excellence sections.
		/// </summary>
		[BsonElement("sections")]
		public List<FeatureSection> Sections { get; set; } = [];
	}
}
