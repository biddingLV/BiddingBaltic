using FeatureAuthorize;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Bidding.Controllers.Shared
{
    [Produces("application/json")]
    [Route("api/[Controller]/[action]")]
    public class PermissionsController : ControllerBase
    {
        /// <summary>
        /// Frontend calls this method to load logged in user's permissions and sets those in local storage.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult LoadUserPermissions()
        {
            List<string> packedPermissions = HttpContext.User?.Claims
                .SingleOrDefault(x => x.Type == PermissionConstants.PackedPermissionClaimType)?
                .Value.Split(',').ToList();

            return Ok(packedPermissions);
        }
    }
}