// *********************************************************************************
//	<copyright file="ResponseDTO.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Response DTO.</summary>
// *********************************************************************************

namespace InternetBulletin.Shared.DTOs
{
	/// <summary>
	/// The Response DTO.
	/// </summary>
	public class ResponseDTO
	{
		/// <summary>
		/// Gets or sets the status code.
		/// </summary>
		/// <value>
		/// The status code.
		/// </value>
		public int StatusCode { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is success.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is success; otherwise, <c>false</c>.
		/// </value>
		public bool IsSuccess { get; set; }

		/// <summary>
		/// Gets or sets the data.
		/// </summary>
		/// <value>
		/// The data.
		/// </value>
		public object Data { get; set; } = default!;
	}
}
