using Bidding.Models.ViewModels.Admin.Users.List;
using Bidding.Models.ViewModels.Users.Edit;
using Bidding.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Details([FromQuery] int userId)
        {
            return Ok(await m_userService.UserDetails(userId).ConfigureAwait(true));
        }

        [HttpGet]
        public async Task<IActionResult> EditDetails([FromQuery] int userId)
        {
            return Ok(await m_userService.UserDetails(userId).ConfigureAwait(true));
        }

        /// <summary>
        /// MAKE TWO DIFFERENT METHODS
        /// ONE FOR UPDATING OWN PROFILE
        /// ANOTHER OWN FOR UPDATEING ANOTHER USER BASED ON Permission
        /// Get rid of user id passed to this method
        /// TRY THAT!!!!!!!!
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] UserEditRequestModel request)
        {
            return Ok(await m_userService.Edit(request).ConfigureAwait(true));
        }

        [HttpGet]
        [Authorize(Roles = "User, Admin")]
        public IActionResult Search([FromQuery] UserListRequestModel request)
        {
            return Ok(m_userService.ListWithSearch(request));
        }
    }
}
