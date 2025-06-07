// *********************************************************************************
//	<copyright file="ApplicationTechnologies.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The application technologies data dto.</summary>
// *********************************************************************************

namespace InternetBulletin.Shared.DTOs.ApplicationInfo
{
	using MongoDB.Bson;
	using MongoDB.Bson.Serialization.Attributes;

	/// <summary>
	/// The application technologies data dto.
	/// </summary>
	[BsonIgnoreExtraElements]
	public class ApplicationTechnologies
	{
		/// <summary>
		/// The Id.
		/// </summary>
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string Id { get; set; } = string.Empty;

		/// <summary>
		/// The heading.
		/// </summary>
		[BsonElement("Heading")]
		public string Heading { get; set; } = string.Empty;

		/// <summary>
		/// The description.
		/// </summary>
		[BsonElement("Description")]
		public string Description { get; set; } = string.Empty;

		/// <summary>
		/// The link.
		/// </summary>
		[BsonElement("Link")]
		public string Link { get; set; } = string.Empty;

		/// <summary>
		/// The image.
		/// </summary>
		[BsonElement("Image")]
		public string Image { get; set; } = string.Empty;
	}
}
