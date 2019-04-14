using Bidding.Repositories.Shared;
using Bidding.Shared.ErrorHandling.Errors;
using Bidding.Shared.Exceptions;
using Bidding.Shared.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Bidding.Services.Shared.Permissions
{
    public class PermissionService
    {
        private readonly PermissionRepository m_permissionRepository;
        private readonly ClaimsPrincipal m_claimPrincipal;

        public PermissionService(PermissionRepository permissionRepository, IPrincipal principal)
        {
            m_claimPrincipal = principal as ClaimsPrincipal ?? throw new ArgumentNullException(nameof(principal));
            m_permissionRepository = permissionRepository ?? throw new ArgumentNullException(nameof(permissionRepository));
        }

        /// <summary>
        /// Check if the logged in user is active.
        /// </summary>
        /// <returns></returns>
        public void IsLoggedInUserActive()
        {
            int? loggedInUserId = GetUserIdFromClaimsPrincipal();

            if (loggedInUserId.IsNotSpecified() == false)
            {
                if (IsUserActive(loggedInUserId.Value) == false) throw new WebApiException(HttpStatusCode.Forbidden, UserErrorMessages.UserNotActive);
            }
            else
            {
                throw new WebApiException(HttpStatusCode.Forbidden, UserErrorMessages.UserNotActive);
            }
        }

        /// <summary>
        /// Checks if claim principal has user id
        /// </summary>
        /// <returns></returns>
        public int? GetUserIdFromClaimsPrincipal()
        {
            if (m_claimPrincipal?.FindFirst("UserId") != null && int.TryParse(m_claimPrincipal?.FindFirst("UserId").Value.ToLower(), out int userId))
            {
                return userId;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Check if this specific user is still active in the database
        /// </summary>
        /// <param name="loggedInUserId"></param>
        /// <returns></returns>
        private bool IsUserActive(int loggedInUserId)
        {
            return m_permissionRepository.IsUserActive(loggedInUserId);
        }
    }
}
