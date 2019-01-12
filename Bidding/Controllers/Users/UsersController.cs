using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
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
    public class UsersController : Controller
    {
        //public readonly IPermissionService m_permissionsService;
        //private readonly IUsersService m_userService;

        //public UsersController(IPermissionService permissionsService, IUsersService userService)
        //{
        //    m_permissionsService = permissionsService ?? throw new ArgumentNullException(nameof(permissionsService));
        //    m_userService = userService ?? throw new ArgumentNullException(nameof(userService));
        //}

        //[HttpGet]
        //public IActionResult Search([FromQuery] UserListRequestModel request)
        //{
        //    return Ok(m_userService.Search(request));
        //}

        //[HttpGet]
        //public IActionResult Details([FromQuery] int userId)
        //{
        //    return Ok(m_userService.Details(userId));
        //}

        //[HttpPut]
        //[Authorize(Roles = "User, Admin, SuperAdmin")]
        //public IActionResult Edit([FromBody] UserEditRequestModel request)
        //{
        //    return Ok(m_userService.Update(request));
        //}

        //// get roles for the add and edit modals
        //[HttpGet]
        //[Authorize(Roles = "User, Admin, SuperAdmin")]
        //public IActionResult Roles()
        //{
        //    return Ok(m_userService.Roles());
        //}

        //// kke: this can be used after the upgrade to check:
        //// kk: if we already have a user with this email address(globally) for sign in email field!
        //[HttpGet]
        //[Authorize(Roles = "User, Admin, SuperAdmin")]
        //public IActionResult UserExists(string signInEmail)
        //{
        //    return Ok(m_userService.UserExists(signInEmail));
        //}

        //// Add
        //[HttpPost]
        //[Authorize(Roles = "User, Admin, SuperAdmin")]
        //public IActionResult Create([FromBody] UserAddRequestModel request)
        //{
        //    return Ok(m_userService.Create(request));
        //}

        //// Delete
        //[HttpGet]
        //[Authorize(Roles = "User, Admin, SuperAdmin")]
        //public IActionResult OrganizationUsers(int organizationId, int userId)
        //{
        //    return Ok(m_userService.OrganizationUsers(organizationId, userId));
        //}

        //// Delete
        //[HttpDelete]
        //[Authorize(Roles = "User, Admin, SuperAdmin")]
        //public async Task<IActionResult> Delete([FromBody] UserDeleteRequestModel request)
        //{
        //    return Ok(await m_userService.Delete(request));
        //}

        //[HttpPost]
        //public IActionResult Reset(int userId)
        //{
        //    return Ok(m_userService.Reset(userId));
        //}

        //[HttpPost]
        //[Authorize(Roles = "User, Admin, SuperAdmin")]
        ////[ValidateAntiForgeryToken]
        //public async Task<IActionResult> SendInvitation([FromBody] int userId) // TODO: MJU: Maybe we want to include more in this request? Message to the new user?
        //{
        //    return Ok(await m_userService.SendInvitation(userId));
        //}
    }
}
