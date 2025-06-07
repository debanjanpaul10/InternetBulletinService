// *********************************************************************************
//	<copyright file="IAIDataService.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>AI Data Services interface.</summary>
// *********************************************************************************

namespace InternetBulletin.Data.Contracts
{
	using InternetBulletin.Shared.DTOs;

	/// <summary>
	/// AI Data Services interface.
	/// </summary>
	public interface IAIDataService
	{
		/// <summary>
		/// Saves the AI usage data for the current user.
		/// </summary>
		/// <param name="aiUsageData">The ai usage data.</param>
		/// <returns>A boolean for success/failure.</returns>
		Task<bool> SaveAiUsageDataAsync(AiUsageDTO aiUsageData);
	}
}
