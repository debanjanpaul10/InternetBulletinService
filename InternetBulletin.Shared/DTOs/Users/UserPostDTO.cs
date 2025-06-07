// *********************************************************************************
//	<copyright file="UserPostDTO.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The User Post DTO.</summary>
// *********************************************************************************

namespace InternetBulletin.Shared.DTOs.Users
{
    /// <summary>
    /// The User Post DTO.
    /// </summary>
    public class UserPostDTO
    {
        /// <summary>
        /// Gets or sets the post identifier.
        /// </summary>
        /// <value>
        /// The post identifier.
        /// </value>
        public Guid PostId { get; set; }

        /// <summary>
        /// Gets or sets the post title.
        /// </summary>
        /// <value>
        /// The post title.
        /// </value>
        public string PostTitle { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the post created date.
        /// </summary>
        /// <value>
        /// The post created date.
        /// </value>
        public DateTime PostCreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the post owner user name.
        /// </summary>
        /// <value>
        /// The post owner user name.
        /// </value>
        public string PostOwnerUserName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the ratings.
        /// </summary>
        /// <value>
        /// The ratings.
        /// </value>
        public int Ratings { get; set; }
    }
} 