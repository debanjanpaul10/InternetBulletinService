// *********************************************************************************
//	<copyright file="AIDataService.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>AI Data Services class.</summary>
// *********************************************************************************

namespace InternetBulletin.Data.DataServices
{
	using System.Threading.Tasks;
	using InternetBulletin.Data.Contracts;
	using InternetBulletin.Data.Entities;
	using InternetBulletin.Shared.Constants;
	using InternetBulletin.Shared.DTOs.AI;
	using Microsoft.Extensions.Logging;

	/// <summary>
	/// The AI Data Services Class.
	/// </summary>
	/// <param name="dbContext">The database context.</param>
	/// <param name="logger">The logger.</param>
	public class AIDataService(SqlDbContext dbContext, ILogger<AIDataService> logger) : IAIDataService
	{
		/// <summary>
		/// The db context.
		/// </summary>
		private readonly SqlDbContext _dbContext = dbContext;

		// <summary>
		/// The logger.
		/// </summary>
		private readonly ILogger<AIDataService> _logger = logger;

		/// <summary>
		/// Saves the AI usage data for the current user.
		/// </summary>
		/// <param name="aiUsageData">The ai usage data.</param>
		/// <returns>A boolean for success/failure.</returns>
		public async Task<bool> SaveAiUsageDataAsync(AiUsageDTO aiUsageData)
		{
			try
			{
				this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodStart, nameof(SaveAiUsageDataAsync), DateTime.UtcNow, aiUsageData.UserName));
				var aiDbData = new AiUsage
				{
					IsActive = true,
					TotalTokensConsumed = aiUsageData.TotalTokensConsumed,
					CandidatesTokenCount = aiUsageData.CandidatesTokenCount,
					PromptTokenCount = aiUsageData.PromptTokenCount,
					Usage = aiUsageData.Usage,
					UsageTime = aiUsageData.UsageTime,
					UserName = aiUsageData.UserName
				};

				await this._dbContext.AiUsages.AddAsync(aiDbData);
				await this._dbContext.SaveChangesAsync();

				return true;
			}
			catch (Exception ex)
			{
				this._logger.LogError(ex, string.Format(LoggingConstants.LogHelperMethodFailed, nameof(SaveAiUsageDataAsync), DateTime.UtcNow, ex.Message));
				throw;
			}
			finally
			{
				this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodEnded, nameof(SaveAiUsageDataAsync), DateTime.UtcNow, aiUsageData.UserName));
			}
		}
	}
}
