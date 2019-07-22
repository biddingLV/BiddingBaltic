using System;
using Bidding.Models.ViewModels.Bidding.Admin.Users.List;
using Bidding.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        /// <summary>
        /// todo: kke: IS THIS ONLY USED INTERNALLY? Is this even used somewhere?
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Details([FromQuery] int userId)
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
