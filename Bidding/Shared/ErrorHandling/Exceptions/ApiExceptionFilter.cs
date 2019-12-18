using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bidding.Shared.Exceptions
{
    public class ApiExceptionFilter : IExceptionFilter
    {
        private readonly ILogger m_logger;

        public ApiExceptionFilter(ILoggerFactory loggerFactory)
        {
            m_logger = loggerFactory.CreateLogger("Status");
        }

        public async void OnException(ExceptionContext context)
        {
            Exception ex = context.Exception;
            if (ex is WebApiException wex) // handled exceptions
            {
                context.HttpContext.Response.Clear();
                context.HttpContext.Response.StatusCode = wex.HttpStatusCode;
                context.ExceptionHandled = true;

                if (wex.InnerException != null)
                {
                    m_logger.LogError(wex, wex.Message);
                }
                else
                {
                    m_logger.LogError(wex.Message);
                }
                if (wex.UserMessage != null)
                {
                    await context.HttpContext.Response.WriteAsync(wex.UserMessage).ConfigureAwait(true);
                }
            }
            else // unhandled exceptions: send an email (note that unhandled exceptions are automatically logged in the logger)
            {
                // todo: kke: uncomment this when we are ready for the prod release!
                //Email.SendExceptionEmail(ex, "Unhandled exception raised in " + context.HttpContext.Request.Host.Value,
                //    context.HttpContext.Connection.RemoteIpAddress);
            }
        }
    }
}
