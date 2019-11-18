using Bidding.Services.Shared.Permissions;
using FeatureAuthorize;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Bidding.Controllers.Shared
{
    [Produces("application/json")]
    [Route("api/[Controller]/[action]")]
    public class PermissionsController : ControllerBase
    {
        // todo: kke: Do I need permissions Service here?
        //private readonly PermissionService m_permissionService;

        //public PermissionsController(PermissionService permissionService)
        //{
        //    m_permissionService = permissionService ?? throw new ArgumentNullException(nameof(permissionService));
        //}

        /// <summary>
        /// Frontend calls this method to load logged in user's permissions and sets those in local storage.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult LoadUserPermissions()
        {
            var packedPermissions = HttpContext.User?.Claims.SingleOrDefault(x => x.Type == PermissionConstants.PackedPermissionClaimType);

            return Ok(packedPermissions?.Value);
        }
    }
}