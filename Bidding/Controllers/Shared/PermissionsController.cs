using Bidding.Services.Shared.Permissions;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Bidding.Controllers.Shared
{
    [Produces("application/json")]
    [Route("api/[Controller]/[action]")]
    public class PermissionsController : ControllerBase
    {
        private readonly PermissionService m_permissionService;

        public PermissionsController(PermissionService permissionService)
        {
            m_permissionService = permissionService ?? throw new ArgumentNullException(nameof(permissionService));
        }

        [HttpGet]
        public IActionResult UserRole()
        {
            return Ok(m_permissionService.GetUserRole());
        }
    }
}