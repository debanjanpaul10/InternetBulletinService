// *********************************************************************************
//	<copyright file="BaseController.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Base Controller Class.</summary>
// *********************************************************************************

namespace InternetBulletin.API.Controllers
{
    using InternetBulletin.Shared.Constants;
    using InternetBulletin.Shared.DTOs;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Net;
    using static InternetBulletin.Shared.Constants.ConfigurationConstants;

    /// <summary>
    /// The Base Controller Class.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    /// <param name="configuration">The Configuration.</param>
    [Authorize]
    public abstract class BaseController : ControllerBase
    {
        /// <summary>
        /// The user name.
        /// </summary>
        protected string UserName = string.Empty;

        /// <summary>
        /// The user full name.
        /// </summary>
        protected string UserFullName = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseController"/> class.
        /// </summary>
        /// <param name="httpContextAccessor">The http context accessor.</param>
        protected BaseController(IHttpContextAccessor httpContextAccessor)
        {
            if (httpContextAccessor.HttpContext is not null && httpContextAccessor.HttpContext?.User is not null)
            {
                var userName = httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(claim => claim.Type.Equals(UserNameClaimConstant))?.Value;
                if (!string.IsNullOrEmpty(userName))
                {
                    this.UserName = userName;
                }

                var userDisplayName = httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(claim => claim.Type.Equals(UserDisplayNameConstant))?.Value;
                if (!string.IsNullOrEmpty(userDisplayName))
                {
                    this.UserFullName = userDisplayName;
                }
            }
        }

        /// <summary>
        /// Is user authorized.
        /// </summary>
        protected bool IsAuthorized()
        {
            if (string.IsNullOrEmpty(this.UserName))
            {
                throw new UnauthorizedAccessException();
            }

            return true;
        }

        /// <summary>
        /// Handles the success result.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <returns>The ok object result</returns>
        protected OkObjectResult HandleSuccessResult(object response)
        {
            var responseData = new ResponseDTO()
            {
                Data = response,
                IsSuccess = true,
                StatusCode = (int)HttpStatusCode.OK
            };
            return this.Ok(responseData);
        }

        /// <summary>
        /// Handles the bad request.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>The bad request result.</returns>
        protected BadRequestObjectResult HandleBadRequest(string message)
        {
            var responseData = new ResponseDTO()
            {
                Data = message,
                IsSuccess = false,
                StatusCode = (int)HttpStatusCode.BadRequest
            };
            return this.BadRequest(responseData);
        }

        /// <summary>
        /// Handles the bad request.
        /// </summary>
        /// <returns>The unauthorized object result</returns>
        protected UnauthorizedObjectResult HandleUnAuthorizedRequest()
        {
            var responseData = new ResponseDTO()
            {
                Data = ExceptionConstants.UserUnauthorizedMessageConstant,
                StatusCode = (int)HttpStatusCode.Unauthorized,
                IsSuccess = false,
            };
            return this.Unauthorized(responseData);
        }
    }
}
