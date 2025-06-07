// *********************************************************************************
//	<copyright file="LoggingConstants.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Logging Constants Class.</summary>
// *********************************************************************************

namespace InternetBulletin.Shared.Constants
{
    /// <summary>
    /// The Logging Constants Class.
    /// </summary>
    public static class LoggingConstants
    {
        /// <summary>
        /// The log helper method start
        /// </summary>
        public const string LogHelperMethodStart = "{0} started at {1} for {2}";

        /// <summary>
        /// The log helper method ended
        /// </summary>
        public const string LogHelperMethodEnded = "{0} ended at {1} for {2}";

        /// <summary>
        /// The log helper method failed
        /// </summary>
        public const string LogHelperMethodFailed = "{0} failed at {1} with message {2}";

        /// <summary>
        /// The cache key found message constant.
        /// </summary>
        public const string CacheKeyFoundMessageConstant = "Cache service found the existing key: {0}";

        /// <summary>
        /// The cache key not found message constant.
        /// </summary>
        public const string CacheKeyNotFoundMessageConstant = "Cache service could not find the key: {0}";

    }
}
