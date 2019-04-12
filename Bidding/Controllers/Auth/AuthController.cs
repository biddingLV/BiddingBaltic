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
    public class AuthController : ControllerBase
    {
        private readonly string clientURL; // log-in
        private readonly string clientLogoutURL;
        private readonly string clientId;
        private readonly string clientSecret;
        private readonly string domain;
        private readonly string scheme;
        private readonly string m_profileCookieName;

        public AuthController(IConfiguration configuration)
        {
            clientURL = configuration["ClientURL"];
            clientLogoutURL = configuration["ClientLogoutURL"];
            clientId = configuration["Authentication:ClientId"];
            clientSecret = configuration["Authentication:ClientSecret"];
            domain = configuration["Authentication:Domain"];
            scheme = configuration["Authentication:Scheme"];
            m_profileCookieName = configuration["Cookies:Profile"];
        }

        [AllowAnonymous]
        public async Task Login(string redirectPage = "")
        {
            Uri clientUri = new Uri(clientURL);
            Uri redirectPageUri = new Uri(clientUri, redirectPage);

            await HttpContext.ChallengeAsync(scheme, new AuthenticationProperties()
            {
                RedirectUri = redirectPageUri.AbsoluteUri
            });
        }

        public async Task Logout()
        {
            Response.Cookies.Delete(m_profileCookieName);

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignOutAsync(scheme, new AuthenticationProperties
            {
                RedirectUri = clientLogoutURL
            });
        }
    }
}