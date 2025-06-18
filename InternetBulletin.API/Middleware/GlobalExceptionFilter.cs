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
		/// <param name="context">The exception context.</param>
		public void OnException(ExceptionContext context)
		{
			if (context is not null && context.HttpContext is not null && !context.HttpContext.Response.HasStarted)
			{
				var path = context.HttpContext?.Request?.Path;
				var user = context.HttpContext?.User?.Identity?.Name;
				var tracedId = context.HttpContext?.TraceIdentifier;
				var actionName = context.ActionDescriptor?.DisplayName;

				var errorDictionary = new Dictionary<string, string>
				{
					{ "Action", actionName ?? string.Empty },
					{ "Path", path?? string.Empty  },
					{ "User", user ?? string.Empty},
					{ "TraceId", tracedId?? string.Empty }
				};
				this._logger.LogError(context.Exception.ToString(), errorDictionary);


				context.Result = new JsonResult(new InternetBulletinBusinessException
				{
					ExceptionMessage = context.Exception.Message,
					StatusCode = StatusCodes.Status500InternalServerError,
					Details = context.Exception.StackTrace
				});
			}

		}

	}

}