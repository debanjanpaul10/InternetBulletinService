// *********************************************************************************
//	<copyright file="BulletinServicesController.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Bulletin Services Controller Class.</summary>
// *********************************************************************************

namespace InternetBulletin.API.Controllers
{
	using InternetBulletin.Business.Contracts;
	using InternetBulletin.Shared.Constants;
	using InternetBulletin.Shared.DTOs.ApplicationInfo;
	using InternetBulletin.Shared.DTOs.Posts;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;

	/// <summary>
	/// Bulletin services class.
	/// </summary>
	/// <param name="logger">The logger.</param>
	/// <param name="httpContextAccessor">The http context accessor.</param>
	/// <param name="usersService">The user services.</param>
	/// <param name="aiService">The ai service</param>
	/// <param name="bulletinService">The bulletin service.</param>
	/// <seealso cref="BaseController"/>
	[ApiController]
	[Route(RouteConstants.BulletinServicesBase_RoutePrefix)]
	public class BulletinServicesController(
		ILogger<BulletinServicesController> logger, IHttpContextAccessor httpContextAccessor, 
		IUsersService usersService, IAIService aiService, IBulletinService bulletinService) : BaseController(httpContextAccessor)
	{
		/// <summary>
		/// The logger.
		/// </summary>
		private readonly ILogger<BulletinServicesController> _logger = logger;

		/// <summary>
		/// The user services.
		/// </summary>
		private readonly IUsersService _usersService = usersService;

		/// <summary>
		/// The AI Service.
		/// </summary>
		private readonly IAIService _aiService = aiService;

		/// <summary>
		/// The bulletin service.
		/// </summary>
		private readonly IBulletinService _bulletinService = bulletinService;

		/// <summary>
		/// Gets the about us data asynchronously.
		/// </summary>
		/// <returns>The about us page data <see cref="AboutUsAppInfoDataDTO"/></returns>
		[HttpGet]
		[Route(RouteConstants.GetAboutUsData_Route)]
		[AllowAnonymous]
		public async Task<AboutUsAppInfoDataDTO> GetAboutUsDataAsync()
		{
			try
			{
				this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodStart, nameof(RewriteWithAIAsync), DateTime.UtcNow, this.UserName ?? string.Empty));
				var result = await this._bulletinService.GetAboutUsDataAsync();
				return result;
			}
			catch (Exception ex)
			{
				this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodFailed, nameof(GetAboutUsDataAsync), DateTime.UtcNow, ex.Message));
				throw;
			}
			finally
			{
				this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodEnded, nameof(GetAboutUsDataAsync), DateTime.UtcNow, this.UserName ?? string.Empty));
			}
		}

		/// <summary>
		/// Gets graph user data async.
		/// </summary>
		/// <param name="userName">The user name.</param>
		/// <returns>The graph user data dto.</returns>
		[HttpGet]
		[Route(RouteConstants.GetUsersDataFromGraph_Route)]
		public async Task<IActionResult> GetUsersDataFromGraphApiAsync(string userName)
		{
			try
			{
				this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodStart, nameof(GetUsersDataFromGraphApiAsync), DateTime.UtcNow, this.UserName));
				if (this.IsAuthorized())
				{
					var result = await this._usersService.GetGraphUserDataAsync(userName);
					if (result is not null)
					{
						return this.HandleSuccessResult(result);
					}
					else
					{
						return this.HandleBadRequest(ExceptionConstants.UserIdNotPresentExceptionConstant);
					}
				}

				return this.HandleUnAuthorizedRequest();

			}
			catch (Exception ex)
			{
				this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodFailed, nameof(GetUsersDataFromGraphApiAsync), DateTime.UtcNow, ex.Message));
				throw;
			}
			finally
			{
				this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodEnded, nameof(GetUsersDataFromGraphApiAsync), DateTime.UtcNow, this.UserName));
			}
		}

		/// <summary>
		/// Rewrites the with ai asynchronous.
		/// </summary>
		/// <param name="story">The story.</param>
		/// <returns>The ai rewritten response.</returns>
		[HttpPost]
		[Route(RouteConstants.RewriteWithAI_Route)]
		public async Task<IActionResult> RewriteWithAIAsync(RewriteRequestDTO requestDto)
		{
			try
			{
				this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodStart, nameof(RewriteWithAIAsync), DateTime.UtcNow, this.UserName));
				if (this.IsAuthorized())
				{
					ArgumentNullException.ThrowIfNull(requestDto);
		  
					var rewrittenStory = await this._aiService.RewriteWithAIAsync(this.UserName, requestDto);
					if (!string.IsNullOrEmpty(rewrittenStory))
					{
						return this.HandleSuccessResult(rewrittenStory);
					}

					return this.HandleBadRequest(ExceptionConstants.SomethingWentWrongMessageConstant);
				}

				return this.HandleUnAuthorizedRequest();

			}
			catch (Exception ex)
			{
				this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodFailed, nameof(RewriteWithAIAsync), DateTime.UtcNow, ex.Message));
				throw;
			}
			finally
			{
				this._logger.LogInformation(string.Format(LoggingConstants.LogHelperMethodEnded, nameof(RewriteWithAIAsync), DateTime.UtcNow, this.UserName));
			}
		}
	}

}

