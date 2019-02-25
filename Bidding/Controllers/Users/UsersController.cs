using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Bidding.Models.ViewModels.Bidding.Users.Add;
using Bidding.Services.Users;
using BiddingAPI.Models;
using BiddingAPI.Models.DatabaseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

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

        [HttpPost]
        public IActionResult Create([FromBody] UserAddRequestModel request)
        {
            return Ok(m_userService.Create(request));
        }
    }
}
