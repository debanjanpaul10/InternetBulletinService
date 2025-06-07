// *********************************************************************************
//	<copyright file="UserPostsAndRatingsDTO.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The User Posts and Ratings DTO.</summary>
// *********************************************************************************

namespace InternetBulletin.Shared.DTOs.Users
{
    /// <summary>
    /// The User Posts and Ratings DTO.
    /// </summary>
    public class UserPostsAndRatingsDTO
    {
        /// <summary>
        /// Gets or sets the user posts.
        /// </summary>
        /// <value>
        /// The user posts.
        /// </value>
        public List<UserPostDTO> UserPosts { get; set; } = [];

        /// <summary>
        /// Gets or sets the user post ratings.
        /// </summary>
        /// <value>
        /// The user post ratings.
        /// </value>
        public List<UserPostRatingDTO> UserPostRatings { get; set; } = [];
    }
} 