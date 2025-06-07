// *********************************************************************************
//	<copyright file="InternetBulletinBusinessException.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>Internet bulletin exception helper.</summary>
// *********************************************************************************

namespace InternetBulletin.Shared.ExceptionHelpers
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Internet bulletin exception helper.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class InternetBulletinBusinessException
    {
        /// <summary>
        /// Gets or sets the status code.
        /// </summary>
        /// <value>
        /// The status code.
        /// </value>
        public int StatusCode { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string? ExceptionMessage { get; set; }

        /// <summary>
        /// Gets or sets the details.
        /// </summary>
        /// <value>
        /// The details.
        /// </value>
        public string? Details { get; set; }

    }
}


