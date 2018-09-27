using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Bidding.Shared.Exceptions
{
    public class WebApiException : Exception
    {
        public int HttpStatusCode { get; protected set; } = 500;

        public string UserMessage { get; set; } = null;

        public WebApiException(HttpStatusCode httpStatusCode) : base(httpStatusCode.ToString())
        {
            HttpStatusCode = (int)httpStatusCode;
        }

        public WebApiException(HttpStatusCode httpStatusCode, Exception exception) : base(httpStatusCode.ToString(), exception)
        {
            HttpStatusCode = (int)httpStatusCode;
        }

        public WebApiException(int statusCode, string message) : base(message)
        {
            HttpStatusCode = statusCode;
            UserMessage = message;
        }

        public WebApiException(HttpStatusCode statusCode, string message) : this((int)statusCode, message)
        {
        }

        public WebApiException() : base()
        {
        }
    }
}
