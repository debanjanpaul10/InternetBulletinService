// *********************************************************************************
//	<copyright file="InternetBulletinBusinessException.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>Internet bulletin exception helper.</summary>
// *********************************************************************************

namespace InternetBulletin.Shared.ExceptionHelpers
{
	using System.Diagnostics.CodeAnalysis;
	using System;

	/// <summary>
	/// Internet bulletin exception helper.
	/// </summary>
	[ExcludeFromCodeCoverage]
	public class InternetBulletinBusinessException : Exception
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

		/// <summary>
		/// Initializes a new instance of the <see cref="InternetBulletinBusinessException"/> class.
		/// </summary>
		public InternetBulletinBusinessException() : base()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="InternetBulletinBusinessException"/> class.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		/// <param name="statusCode">The HTTP status code.</param>
		public InternetBulletinBusinessException(string message, int statusCode = 500) : base(message)
		{
			this.StatusCode = statusCode;
			this.ExceptionMessage = message;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="InternetBulletinBusinessException"/> class.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		/// <param name="innerException">The exception that is the cause of the current exception.</param>
		/// <param name="statusCode">The HTTP status code.</param>
		public InternetBulletinBusinessException(string message, Exception innerException, int statusCode = 500) 
			: base(message, innerException)
		{
			this.StatusCode = statusCode;
			this.ExceptionMessage = message;
			this.Details = innerException.StackTrace;
		}
	}
}


