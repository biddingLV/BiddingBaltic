using Bidding.Shared.ErrorHandling.Errors;
using Bidding.Shared.Exceptions;
using FeatureAuthorize;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;

namespace Bidding.Services.Shared.Permissions
{
    public class PermissionService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PermissionService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        /// <summary>
        /// Pretty much only gets UserId.
        /// </summary>
        /// <returns></returns>
        public string GetUserId()
        {
            return _httpContextAccessor.HttpContext.User.FindFirst(PermissionConstants.UserIdClaimType)?.Value;
        }

        /// <summary>
        /// Gets user id and also validates if the user id exists in Claims.
        /// </summary>
        /// <returns></returns>
        public int GetAndValidateUserId()
        {
            string userIdValue = GetUserId();

            if (!int.TryParse(userIdValue, out int userId))
            {
                throw new WebApiException(HttpStatusCode.Forbidden, UserErrorMessage.UserNotActive);
            }

            return userId;
        }
    }
}
