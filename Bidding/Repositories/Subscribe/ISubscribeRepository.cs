using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bidding.Models.ViewModels.Bidding.Subscribe;

namespace BiddingAPI.Repositories.Subscribe
{
    public interface ISubscribeRepository
    {
        Task<bool> UsingEmail(EmailRequestModel request);

        Task<bool> UsingWhatsApp(WhatsAppRequestModel request);

        Task<bool> UsingSurvey(SurveyRequestModel request);
    }
}
