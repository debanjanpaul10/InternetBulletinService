// *********************************************************************************
//	<copyright file="GlobalExceptionHandler.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Global Exception Handler.</summary>
// *********************************************************************************

namespace InternetBulletin.API.Middleware
{
	using InternetBulletin.Shared.ExceptionHelpers;
	using Microsoft.AspNetCore.Http;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.Mvc.Filters;
	using Microsoft.Extensions.Logging;

	/// <summary>
	/// The Global Exception Handler.
	/// </summary>
	/// <param name="logger">The logger</param>
	public class GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger) : IExceptionFilter
	{
		/// <summary>
		/// The logger.
		/// </summary>
		private readonly ILogger<GlobalExceptionFilter> _logger = logger;

		/// <summary>
		/// Tries handle async.
		/// </summary>
		/// <param name="exceptionContext">The exception context.</param>
		public void OnException(ExceptionContext exceptionContext)
		{
			if (exceptionContext is not null && exceptionContext.HttpContext is not null && !exceptionContext.HttpContext.Response.HasStarted)
			{
				var path = exceptionContext.HttpContext?.Request?.Path;
				var user = exceptionContext.HttpContext?.User?.Identity?.Name;
				var tracedId = exceptionContext.HttpContext?.TraceIdentifier;
				var actionName = exceptionContext.ActionDescriptor?.DisplayName;

				var errorDictionary = new Dictionary<string, string>
				{
					{ "Action", actionName ?? string.Empty },
					{ "Path", path?? string.Empty  },
					{ "User", user ?? string.Empty},
					{ "TraceId", tracedId?? string.Empty }
				};
				this._logger.LogError(exceptionContext.Exception.ToString(), errorDictionary);


				exceptionContext.Result = new JsonResult(new InternetBulletinBusinessException
				{
					ExceptionMessage = exceptionContext.Exception.Message,
					StatusCode = StatusCodes.Status500InternalServerError,
					Details = exceptionContext.Exception.StackTrace
				});
			}

		}

	}

}