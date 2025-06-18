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
	using InternetBulletin.Shared.DTOs.AI;
	using InternetBulletin.Shared.Helpers;
	using Newtonsoft.Json;
	using System.Text.RegularExpressions;

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
		/// Rewrites the provided story using AI processing.
		/// </summary>
		/// <param name="userName">The username of the user requesting the rewrite.</param>
		/// <param name="requestDTO">The story content to be rewritten.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the AI-rewritten story.</returns>
		/// <exception cref="Exception">Thrown when AI services cannot be availed or when the response is invalid.</exception>
		public async Task<string> RewriteWithAIAsync(string userName, UserStoryRequestDTO requestDTO)
		{
			var rewriteAiUrl = RouteConstants.RewriteTextApi_Route;
			var aiStoryResponse = await this.ProcessAIRequestAsync<RewriteResponseDTO>(requestDTO, rewriteAiUrl, userName, AiUsages.RewriteStory);
			return aiStoryResponse.RewrittenStory;
		}

		/// <summary>
		/// Generates a genre tag for the provided story using AI processing.
		/// </summary>
		/// <param name="userName">The username of the user requesting the tag generation.</param>
		/// <param name="requestDTO">The story content for which to generate a tag.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the generated genre tag.</returns>
		/// <exception cref="Exception">Thrown when AI services cannot be availed or when the response is invalid.</exception>
		public async Task<string> GenerateTagForStoryAsync(string userName, UserStoryRequestDTO requestDTO)
		{
			var generateTagUrl = RouteConstants.GenerateTagApi_Route;
			var aiResponse = await this.ProcessAIRequestAsync<TagResponseDTO>(requestDTO, generateTagUrl, userName, AiUsages.GenreTag);
			return Regex.Replace(aiResponse.UserStoryTag, "<[^>]*>?", string.Empty).Trim();
		}

		/// <summary>
		/// Moderates the content of the provided story using AI processing.
		/// </summary>
		/// <param name="userName">The username of the user requesting content moderation.</param>
		/// <param name="requestDTO">The story content to be moderated.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the content rating.</returns>
		/// <exception cref="Exception">Thrown when AI services cannot be availed or when the response is invalid.</exception>
		public async Task<string> ModerateContentDataAsync(string userName, UserStoryRequestDTO requestDTO)
		{
			var moderateContentUrl = RouteConstants.ModerateContentApi_Route;
			var aiResponse = await this.ProcessAIRequestAsync<ModerationContentResponseDTO>(requestDTO, moderateContentUrl, userName, AiUsages.ModerateContent);
			return Regex.Replace(aiResponse.ContentRating, "<[^>]*>?", string.Empty).Trim();
		}

		#region PRIVATE Methods

		/// <summary>
		/// Processes an AI request and handles the response.
		/// </summary>
		/// <typeparam name="T">The type of response DTO expected from the AI service.</typeparam>
		/// <param name="requestDTO">The request data containing the content to be processed.</param>
		/// <param name="apiUrl">The API endpoint URL for the AI service.</param>
		/// <param name="userName">The username of the user making the request.</param>
		/// <param name="aiUsage">The type of AI usage being performed.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the processed AI response.</returns>
		/// <exception cref="Exception">Thrown when AI services cannot be availed or when the response is invalid.</exception>
		private async Task<T> ProcessAIRequestAsync<T>(UserStoryRequestDTO requestDTO, string apiUrl, string userName, AiUsages aiUsage) where T : class
		{
			var response = await this._httpClientHelper.GetIbbsAiResponseAsync(data: requestDTO, apiUrl: apiUrl);
			var aiResponse = JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync()) ?? throw new Exception(ExceptionConstants.AiServicesCannotBeAvailedExceptionConstant);
			await this.SaveAiUsageDataAsync(userName, GetTotalTokensConsumed(aiResponse), GetCandidatesTokenCount(aiResponse), GetPromptTokenCount(aiResponse), nameof(aiUsage));

			return aiResponse;
		}

		/// <summary>
		/// Gets the total tokens consumed from the AI response.
		/// </summary>
		/// <typeparam name="T">The type of response DTO.</typeparam>
		/// <param name="response">The AI response object.</param>
		/// <returns>The total number of tokens consumed in the AI operation.</returns>
		private static int GetTotalTokensConsumed<T>(T response) where T : class
		{
			return response switch
			{
				RewriteResponseDTO r => r.TotalTokensConsumed,
				TagResponseDTO t => t.TotalTokensConsumed,
				ModerationContentResponseDTO m => m.TotalTokensConsumed,
				_ => 0
			};
		}

		/// <summary>
		/// Gets the candidates token count from the AI response.
		/// </summary>
		/// <typeparam name="T">The type of response DTO.</typeparam>
		/// <param name="response">The AI response object.</param>
		/// <returns>The number of tokens used for generating candidates in the AI operation.</returns>
		private static int GetCandidatesTokenCount<T>(T response) where T : class
		{
			return response switch
			{
				RewriteResponseDTO r => r.CandidatesTokenCount,
				TagResponseDTO t => t.CandidatesTokenCount,
				ModerationContentResponseDTO m => m.CandidatesTokenCount,
				_ => 0
			};
		}

		/// <summary>
		/// Gets the prompt token count from the AI response.
		/// </summary>
		/// <typeparam name="T">The type of response DTO.</typeparam>
		/// <param name="response">The AI response object.</param>
		/// <returns>The number of tokens used in the prompt for the AI operation.</returns>
		private static int GetPromptTokenCount<T>(T response) where T : class
		{
			return response switch
			{
				RewriteResponseDTO r => r.PromptTokenCount,
				TagResponseDTO t => t.PromptTokenCount,
				ModerationContentResponseDTO m => m.PromptTokenCount,
				_ => 0
			};
		}

		/// <summary>
		/// Saves the AI usage data for the current user.
		/// </summary>
		/// <param name="userName">The username of the user.</param>
		/// <param name="totalTokensConsumed">The total number of tokens consumed in the AI operation.</param>
		/// <param name="candidatesTokenCount">The number of tokens used for generating candidates.</param>
		/// <param name="promptTokenCount">The number of tokens used in the prompt.</param>
		/// <param name="aiUsage">The type of AI usage being performed.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains a boolean indicating whether the save operation was successful.</returns>
		private async Task<bool> SaveAiUsageDataAsync(string userName, int totalTokensConsumed, int candidatesTokenCount, int promptTokenCount, string aiUsage)
		{
			var aiUsageData = new AiUsageDTO
			{
				TotalTokensConsumed = totalTokensConsumed,
				CandidatesTokenCount = candidatesTokenCount,
				PromptTokenCount = promptTokenCount,
				UserName = userName,
				Usage = aiUsage,
				UsageTime = DateTime.UtcNow
			};
			return await this._aiDataService.SaveAiUsageDataAsync(aiUsageData);
		}

		#endregion
	}
}
