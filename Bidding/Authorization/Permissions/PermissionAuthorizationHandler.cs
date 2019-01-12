using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Bidding.Shared.Authorization
{
    internal class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        // private readonly IPermissionRepository m_permissionRepository;
        private readonly ClaimsPrincipal m_principal;

        public PermissionAuthorizationHandler(//IPermissionRepository permissionRepository
            IPrincipal principal)
        {
            m_principal = principal as ClaimsPrincipal ?? throw new ArgumentNullException(nameof(principal));
            // m_permissionRepository = permissionRepository ?? throw new ArgumentNullException(nameof(permissionRepository));
        }

        protected async override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if (int.TryParse(m_principal?.FindFirst("OrganizationId").Value.ToLower(), out int organizationId))
            {
                // List<PermissionsViewModel> permissions = m_permissionRepository.GetOrganizationPermissions(organizationId);
                //if (permissions.Find(p => p.PermissionId == requirement.PermissionId).Access.Value)
                //{
                //    context.Succeed(requirement);
                //}
            }
        }
    }
}
