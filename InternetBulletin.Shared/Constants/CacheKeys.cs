// *********************************************************************************
//	<copyright file="CacheKeys.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>Cache keys.</summary>
// *********************************************************************************

namespace InternetBulletin.Shared.Constants
{
    /// <summary>
    /// Cache keys.
    /// </summary>
    public static class CacheKeys
    {
        /// <summary>
        /// The default cache expiration timespan.
        /// </summary>
        public static readonly TimeSpan DefaultCacheExpiration = TimeSpan.FromMinutes(10);

        /// <summary>
        /// The graph users data cache key.
        /// </summary>
        public const string FilteredGraphUsersDataCacheKey = "FilteredGraphUsersData";

        /// <summary>
        /// The abut us data cache key.
        /// </summary>
        public const string AboutUsDataCacheKey = "AboutUsData";
    }
}