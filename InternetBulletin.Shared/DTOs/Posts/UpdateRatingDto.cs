// *********************************************************************************
//	<copyright file="UpdateRatingDto.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>Update rating DTO..</summary>
// *********************************************************************************

namespace InternetBulletin.Shared.DTOs.Posts
{
    /// <summary>
    /// Update rating dto.
    /// </summary>
    public class UpdateRatingDto
    {
        /// <summary>
        /// Gets or sets the post id.
        /// </summary>
        /// <value>
        /// The post id.
        /// </value>
        public Guid PostId { get; set; }

        /// <summary>
        /// Gets or sets the is update success.
        /// </summary>
        /// <value>
        /// The is update success.
        /// </value>
        public bool IsUpdateSuccess { get; set; }

        /// <summary>
        /// Gets or sets the has already updated.
        /// </summary>
        /// <value>
        /// The has already updated.
        /// </value>
        public bool HasAlreadyUpdated { get; set; }
    }
}


