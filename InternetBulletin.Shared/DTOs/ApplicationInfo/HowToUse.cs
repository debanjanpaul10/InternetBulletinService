// *********************************************************************************
//	<copyright file="HowToUse.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>How to use data.</summary>
// *********************************************************************************

namespace InternetBulletin.Shared.DTOs.ApplicationInfo
{
	using MongoDB.Bson.Serialization.Attributes;

	/// <summary>
	/// How to use data.
	/// </summary>
	public class HowToUse
	{
		/// <summary>
		/// The how to use title.
		/// </summary>
		[BsonElement("title")]
		public string Title { get; set; } = string.Empty;

		/// <summary>
		/// The how to use sections.
		/// </summary>
		[BsonElement("sections")]
		public List<UsageSection> Sections { get; set; } = [];
	}
}
