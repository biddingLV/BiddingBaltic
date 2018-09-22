using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bidding.Models.ViewModels.Bidding.Subscribe;

namespace BiddingAPI.Repositories.Subscribe
{
    public interface ISubscribeRepository
    {
        Task<bool> UsingEmailAsync(EmailRequestModel request);

        Task<bool> UsingWhatsAppAsync(WhatsAppRequestModel request);

        Task<bool> UsingSurveyAsync(SurveyRequestModel request);
    }
}
