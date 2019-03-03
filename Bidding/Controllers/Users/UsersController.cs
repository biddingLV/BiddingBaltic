using System;
using Bidding.Services.Users;
using Microsoft.AspNetCore.Mvc;

namespace BiddingAPI.Controllers.Users
{
    [Produces("application/json")]
    [Route("api/[Controller]/[action]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService m_userService;

        public UsersController(IUsersService userService)
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
