// *********************************************************************************
//	<copyright file="AIService.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>AI Service class.</summary>
// *********************************************************************************

namespace InternetBulletin.Business.Services
{
	using InternetBulletin.Business.Contracts;
	using InternetBulletin.Data.Contracts;
	using InternetBulletin.Shared.Constants;
	using InternetBulletin.Shared.DTOs;
	using InternetBulletin.Shared.DTOs.Posts;
	using InternetBulletin.Shared.Helpers;
	using Newtonsoft.Json;

	/// <summary>
	/// The AI Service class.
	/// </summary>
	/// <param name="aiDataService">The AI Data service</param>
	/// <param name="httpClientHelper">The http client helper</param>
	/// <seealso cref="IAIService"/>
	public class AIService(IHttpClientHelper httpClientHelper, IAIDataService aiDataService) : IAIService
	{
		/// <summary>
		/// The http client helper.
		/// </summary>
		private readonly IHttpClientHelper _httpClientHelper = httpClientHelper;

		/// <summary>
		/// The AI data service.
		/// </summary>
		private readonly IAIDataService _aiDataService = aiDataService;

		/// <summary>
		/// Rewrites with ai async.
		/// </summary>
		/// <param name="story">The story.</param>
		/// <returns>The AI rewritten response.</returns>
		public async Task<string> RewriteWithAIAsync(string userName, RewriteRequestDTO requestDTO)
		{
			var rewriteAiUrl = RouteConstants.RewriteTextApi_Route;
			var aiResponse = await this._httpClientHelper.GetIbbsAiResponseAsync(data: requestDTO, apiUrl: rewriteAiUrl);

			var aiStoryResponse = JsonConvert.DeserializeObject<RewriteResponseDTO>(await aiResponse.Content.ReadAsStringAsync());
			if (aiStoryResponse is not null && !string.IsNullOrEmpty(aiStoryResponse?.RewrittenStory))
			{
				await this.SaveAiUsageDataAsync(userName, aiResponse: aiStoryResponse);
				return aiStoryResponse.RewrittenStory;
			}

			throw new Exception(ExceptionConstants.AiServicesCannotBeAvailedExceptionConstant);
		}

		#region PRIVATE Methods

		/// <summary>
		/// Saves the AI usage data for the current user.
		/// </summary>
		/// <param name="userName">The current user name.</param>
		/// <param name="aiResponse">The ai response data.</param>
		/// <returns>A boolean for success/failure.</returns>
		private async Task<bool> SaveAiUsageDataAsync(string userName, RewriteResponseDTO aiResponse)
		{
			var aiUsageData = new AiUsageDTO
			{
				TotalTokensConsumed = aiResponse.TotalTokensConsumed,
				CandidatesTokenCount = aiResponse.CandidatesTokenCount,
				PromptTokenCount = aiResponse.PromptTokenCount,
				UserName = userName,
				Usage = nameof(AiUsages.RewriteStory),
				UsageTime = DateTime.UtcNow
			};
			var result = await this._aiDataService.SaveAiUsageDataAsync(aiUsageData);
			return result;
		}

		#endregion
	}
}
