// *********************************************************************************
//	<copyright file="UserPostRatingDTO.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>User post rating d t o.</summary>
// *********************************************************************************

namespace InternetBulletin.Shared.DTOs.Users
{
    /// <summary>
    /// User post rating d t o.
    /// </summary>
    public class UserPostRatingDTO
    {
        /// <summary>
        /// Gets or sets the post name.
        /// </summary>
        /// <value>
        /// The post name.
        /// </value>
        public string PostName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the rated on.
        /// </summary>
        /// <value>
        /// The rated on.
        /// </value>
        public DateTime RatedOn { get; set; }

        /// <summary>
        /// Gets or sets the current rating value.
        /// </summary>
        /// <value>
        /// The current rating value.
        /// </value>
        public int CurrentRatingValue { get; set; }
    }

}

