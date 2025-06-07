// *********************************************************************************
//	<copyright file="Introduction.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The introduction.</summary>
// *********************************************************************************

namespace InternetBulletin.Shared.DTOs.ApplicationInfo
{
	using MongoDB.Bson.Serialization.Attributes;

	/// <summary>
	/// The introduction.
	/// </summary>
	public class Introduction
	{
		/// <summary>
		/// The title of introduction.
		/// </summary>
		[BsonElement("title")]
		public string Title { get; set; } = string.Empty;

		/// <summary>
		/// The content of introduction.
		/// </summary>
		[BsonElement("content")]
		public string Content { get; set; } = string.Empty;
	}
}
