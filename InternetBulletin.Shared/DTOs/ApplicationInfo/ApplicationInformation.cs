// *********************************************************************************
//	<copyright file="ApplicationInformationData.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Application Information Data DTO.</summary>
// *********************************************************************************

namespace InternetBulletin.Shared.DTOs.ApplicationInfo
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    /// <summary>
    /// The Application Information Data DTO.
    /// </summary>
    public class ApplicationInformation
    {
        /// <summary>
        /// The Id.
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// The Title.
        /// </summary>
        [BsonElement("title")]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// The Introduction
        /// </summary>
        [BsonElement("introduction")]
        public Introduction Introduction { get; set; } = new();

        /// <summary>
        /// The data object for what is IBBS.
        /// </summary>
        [BsonElement("whatIsIBBS")]
        public WhatIsIBBS WhatIsIBBS { get; set; } = new();

        /// <summary>
        /// The key features object.
        /// </summary>
        [BsonElement("keyFeatures")]
        public KeyFeatures KeyFeatures { get; set; } = new();

        /// <summary>
        /// How to use object.
        /// </summary>
        [BsonElement("howToUse")]
        public HowToUse HowToUse { get; set; } = new();

        /// <summary>
        /// The technical excellence object.
        /// </summary>
        [BsonElement("technicalExcellence")]
        public TechnicalExcellence TechnicalExcellence { get; set; } = new();

        /// <summary>
        /// The upcoming features object.
        /// </summary>
        [BsonElement("upcomingFeatures")]
        public UpcomingFeatures UpcomingFeatures { get; set; } = new();
    }
    
}


