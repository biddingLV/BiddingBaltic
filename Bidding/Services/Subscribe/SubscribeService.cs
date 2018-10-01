using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Bidding.Models.ViewModels.Bidding.Subscribe;
using Bidding.Shared.Exceptions;
using Bidding.Shared.Utility;
using BiddingAPI.Models.ViewModels.Bidding.Auctions;
using BiddingAPI.Repositories.Auctions;
using BiddingAPI.Repositories.Subscribe;

namespace BiddingAPI.Services.Subscribe
{
    public class SubscribeService : ISubscribeService
    {
        private readonly ISubscribeRepository m_subscribeRepository;

        public SubscribeService(ISubscribeRepository subscribeRepository)
        {
            m_subscribeRepository = subscribeRepository ?? throw new ArgumentNullException(nameof(subscribeRepository));
        }

        public async Task<bool> UsingEmailAsync(EmailRequestModel request)
        {
            if (request.Categories.IsEmpty()) { throw new WebApiException(HttpStatusCode.BadRequest, "Incorrect categories"); }
            if (request.Email.IsEmpty()) { throw new WebApiException(HttpStatusCode.BadRequest, "Incorrect email"); }
            if (request.Name.IsEmpty()) { throw new WebApiException(HttpStatusCode.BadRequest, "Incorrect name"); }

            ValidateCategories(request.Categories);

            return await m_subscribeRepository.UsingEmailAsync(request);
        }

        public async Task<bool> UsingWhatsAppAsync(WhatsAppRequestModel request)
        {
            if (request.Categories.IsEmpty()) { throw new WebApiException(HttpStatusCode.BadRequest, "Incorrect categories"); }
            if (request.Phone.IsEmpty()) { throw new WebApiException(HttpStatusCode.BadRequest, "Incorrect phone"); }
            if (request.Name.IsEmpty()) { throw new WebApiException(HttpStatusCode.BadRequest, "Incorrect name"); }

            ValidateCategories(request.Categories);

            return await m_subscribeRepository.UsingWhatsAppAsync(request);
        }

        public async Task<bool> UsingSurveyAsync(SurveyRequestModel request)
        {
            // todo: kke: check request params!
            //if (request.Categories.IsEmpty()) { throw new WebApiException(HttpStatusCode.BadRequest, "Incorrect categories"); }
            //if (request.Phone.IsEmpty()) { throw new WebApiException(HttpStatusCode.BadRequest, "Incorrect phone"); }
            //if (request.Name.IsEmpty()) { throw new WebApiException(HttpStatusCode.BadRequest, "Incorrect name"); }

            return await m_subscribeRepository.UsingSurveyAsync(request);
        }

        private bool ValidateCategories(List<string> categories)
        {
            List<string> validCategories = new List<string>(new string[] { "vehicles", "items", "companies", "estate", "brands" });

            foreach (string category in categories)
            {
                if (!validCategories.Contains(category))
                {
                    throw new WebApiException(HttpStatusCode.BadRequest, "Something is wrong with the categories!");
                }
                else
                {
                    return true;
                }
            }

            return true;
        }
    }
}
