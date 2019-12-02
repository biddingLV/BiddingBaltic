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

        public int GetUserIdFromClaimsPrincipal()
        {
            string userId = _httpContextAccessor.HttpContext.User.FindFirst(PermissionConstants.UserIdClaimType).Value;

            if (!int.TryParse(userId, out int i))
            {
                throw new WebApiException(HttpStatusCode.Forbidden, UserErrorMessage.UserNotActive);
            }

            return i;
        }
    }
}
