using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Bidding.Controllers.Auth
{
    public class AuthController : Controller
    {
        private readonly string clientURL;
        private readonly string scheme;

        public AuthController(IConfiguration configuration)
        {
            // Configuration
            clientURL = configuration["ClientURL"];
            scheme = configuration["Authentication:Scheme"];
        }

        [AllowAnonymous]
        public async Task Login()
        {
            await HttpContext.ChallengeAsync(scheme, new AuthenticationProperties() { RedirectUri = clientURL });
        }

        public async Task Logout()
        {
            Response.Cookies.Delete("TXPROFILE");

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignOutAsync(scheme, new AuthenticationProperties { RedirectUri = clientURL });
        }
    }
}