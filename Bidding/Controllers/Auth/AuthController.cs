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
        //private readonly string clientId;
        //private readonly string clientSecret;
        //private readonly string domain;
        //private readonly string linkCallbackURL;
        private readonly string scheme;

        public AuthController(
            IConfiguration configuration // ,
                                         // IUsersService usersService,
                                         // Services.Shared.Authentication.IAuthenticationService authenticationService
        )
        {
            // Services
            // m_usersService = usersService ?? throw new ArgumentNullException(nameof(usersService));
            // m_authService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));

            // Configuration
            clientURL = configuration["ClientURL"];
            // linkCallbackURL = configuration["ClientURL"] + "/start/auth/linkcallback";  // TODO: MJU: Make relative part of path configurable.
            //clientId = configuration["Authentication:ClientId"];
            //clientSecret = configuration["Authentication:ClientSecret"];
            //domain = configuration["Authentication:Domain"];
            scheme = configuration["Authentication:Scheme"];
        }

        [AllowAnonymous]
        public async Task Login()
        {
            await HttpContext.ChallengeAsync(scheme, new AuthenticationProperties() { RedirectUri = clientURL });
        }

        public async Task Logout()
        {
            Response.Cookies.Delete("TXPROFILE");   // TODO: MJU: Name in configuration.

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignOutAsync(scheme, new AuthenticationProperties { RedirectUri = clientURL }); // TODO: MJU: Use scheme from configuration!
        }
    }
}