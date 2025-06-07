// *********************************************************************************
//	<copyright file="WhatIsIBBS.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary> What is IBBS data.</summary>
// *********************************************************************************

namespace InternetBulletin.Shared.DTOs.ApplicationInfo
{
	using MongoDB.Bson.Serialization.Attributes;

	/// <summary>
	/// What is IBBS data.
	/// </summary>
	public class WhatIsIBBS
	{
		/// <summary>
		/// The title for what is ibbs.
		/// </summary>
		[BsonElement("title")]
		public string Title { get; set; } = string.Empty;

		/// <summary>
		/// The content for what is ibbs.
		/// </summary>
		[BsonElement("content")]
		public string Content { get; set; } = string.Empty;
	}
}
