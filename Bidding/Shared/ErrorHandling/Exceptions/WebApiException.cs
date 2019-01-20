using Bidding.Shared.ErrorHandling.Errors;
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

        public WebApiException(int statusCode, string message, Exception innerException = null) : base(message, innerException)
        {
            HttpStatusCode = statusCode;
            UserMessage = message;
        }

        public WebApiException(HttpStatusCode statusCode, string message) : this((int)statusCode, message)
        {
        }

        public WebApiException(HttpStatusCode statusCode, AuctionErrorMessages message, Exception innerException = null) : this((int)statusCode, EnumHelper.GetDescriptionFromValue(message), innerException)
        {
        }

        public WebApiException() : base()
        {
        }
    }
}
