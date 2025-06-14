﻿// *********************************************************************************
//	<copyright file="AiUsage.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The AI Usage Entity Class.</summary>
// *********************************************************************************

namespace InternetBulletin.Data.Entities
{
	/// <summary>
	/// The AI Usage Entity Class.
	/// </summary>
	public class AiUsage
	{
		/// <summary>
		/// The primary identifier.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// The identifier for the user of the AI service.
		/// </summary>
		public string UserName { get; set; } = string.Empty;

		/// <summary>
		/// The Usage type where it is used.
		/// </summary>
		public string Usage { get; set; } = string.Empty;

		/// <summary>
		/// The time of usage.
		/// </summary>
		public DateTime UsageTime { get; set; }

		/// <summary>
		/// The total tokens consumed.
		/// </summary>
		public int? TotalTokensConsumed { get; set; }

		/// <summary>
		/// The candidates token count.
		/// </summary>
		public int? CandidatesTokenCount { get; set; }

		/// <summary>
		/// The prompt token count.
		/// </summary>
		public int? PromptTokenCount { get; set; }

		/// <summary>
		/// The boolean flag for active status.
		/// </summary>
		public bool IsActive { get; set; }
	}
}
