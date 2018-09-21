using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bidding.Models.ViewModels.Bidding.Subscribe;
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

        public async Task<bool> UsingEmail(EmailRequestModel request)
        {
            return await m_subscribeRepository.UsingEmail(request);
        }

        public async Task<bool> UsingWhatsApp(WhatsAppRequestModel request)
        {
            return await m_subscribeRepository.UsingWhatsApp(request);
        }

        public async Task<bool> UsingSurvey(SurveyRequestModel request)
        {
            return await m_subscribeRepository.UsingSurvey(request);
        }
    }
}
