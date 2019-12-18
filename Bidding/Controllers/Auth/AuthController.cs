using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace Bidding.Controllers.Auth
{
    public class AuthController : ControllerBase
    {
        private readonly string clientURL;
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
            }).ConfigureAwait(true);
        }

        public async Task Logout()
        {
            Response.Cookies.Delete(m_profileCookieName);

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).ConfigureAwait(true);
            await HttpContext.SignOutAsync(scheme, new AuthenticationProperties
            {
                RedirectUri = clientLogoutURL
            }).ConfigureAwait(true);
        }
    }
}