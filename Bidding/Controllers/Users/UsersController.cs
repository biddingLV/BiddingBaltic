using System;
using Bidding.Services.Users;
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

        [HttpGet]
        public IActionResult Details([FromQuery] int userId)
        {
            return Ok(m_userService.UserDetails(userId));
        }
    }
}
