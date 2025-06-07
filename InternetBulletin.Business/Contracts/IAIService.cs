// *********************************************************************************
//	<copyright file="IAIService.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>AI Services interface.</summary>
// *********************************************************************************

namespace InternetBulletin.Business.Contracts
{
	using InternetBulletin.Shared.DTOs.Posts;

	/// <summary>
	/// AI Services interface.
	/// </summary>
	public interface IAIService
	{
		/// <summary>
		/// Rewrites with a i async.
		/// </summary>
		/// <param name="userName">The current user name.</param>
		/// <param name="requestDTO">The story.</param>
		/// <returns>The AI response data</returns>
		Task<string> RewriteWithAIAsync(string userName, RewriteRequestDTO requestDTO);
	}
}
