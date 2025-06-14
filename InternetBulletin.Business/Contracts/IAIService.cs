// *********************************************************************************
//	<copyright file="IAIService.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>AI Services interface.</summary>
// *********************************************************************************

namespace InternetBulletin.Business.Contracts
{
	using InternetBulletin.Shared.DTOs.AI;

	/// <summary>
	/// AI Services interface.
	/// </summary>
	public interface IAIService
	{
		/// <summary>
		/// Rewrites with AI asynchronously.
		/// </summary>
		/// <param name="userName">The current user name.</param>
		/// <param name="requestDTO">The story.</param>
		/// <returns>The AI response data</returns>
		Task<string> RewriteWithAIAsync(string userName, UserStoryRequestDTO requestDTO);

		/// <summary>
		/// Generates the tag for story asynchronous.
		/// </summary>
		/// <param name="userName">The current user name.</param>
		/// <param name="requestDTO">The story.</param>
		/// <returns>The genre tag response.</returns>
		Task<string> GenerateTagForStoryAsync(string userName, UserStoryRequestDTO requestDTO);

		/// <summary>
		/// Moderates the content data asynchronous.
		/// </summary>
		/// <param name="userName">The current user name.</param>
		/// <param name="requestDTO">The story.</param>
		/// <returns>The moderation content response.</returns>
		Task<string> ModerateContentDataAsync(string userName, UserStoryRequestDTO requestDTO);
	}
}
