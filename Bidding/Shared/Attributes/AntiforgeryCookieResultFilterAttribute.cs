using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bidding.Shared.Attributes
{
    public class AntiforgeryCookieResultFilterAttribute : Microsoft.AspNetCore.Mvc.Filters.ResultFilterAttribute
    {
        protected IAntiforgery Antiforgery { get; set; }
        public AntiforgeryCookieResultFilterAttribute(IAntiforgery antiforgery) => this.Antiforgery = antiforgery;

        public override void OnResultExecuting(Microsoft.AspNetCore.Mvc.Filters.ResultExecutingContext context)
        {
            var tokens = this.Antiforgery.GetAndStoreTokens(context.HttpContext);
            context.HttpContext.Response.Cookies.Append("XSRF-TOKEN", tokens.RequestToken, new CookieOptions() { HttpOnly = false });
        }
    }
}
