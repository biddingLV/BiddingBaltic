using Bidding.Models.ViewModels.Admin.Users.List;
using Bidding.Services.Users;
using FeatureAuthorize.PolicyCode;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PermissionParts;
using System;

namespace Bidding.Controllers.Users
{
    [Produces("application/json")]
    [Route("api/[Controller]/[action]")]
    public class UsersController : ControllerBase
    {
        private readonly UsersService m_userService;

        public UsersController(UsersService userService)
        {
            m_userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [HttpGet]
        [HasPermission(Permission.ChangeOwnProfile)]
        public IActionResult Details([FromQuery] int userId)
        {
            return Ok(m_userService.UserDetails(userId));
        }

        [HttpGet]
        [Authorize(Roles = "User, Admin")]
        public IActionResult EditDetails([FromQuery] int userId)
        {
            return Ok(m_userService.UserDetails(userId));
        }

        [HttpGet]
        [Authorize(Roles = "User, Admin")]
        public IActionResult Search([FromQuery] UserListRequestModel request)
        {
            return Ok(m_userService.ListWithSearch(request));
        }
    }
}
