using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bidding.Models.ViewModels.Bidding.Subscribe;

namespace BiddingAPI.Repositories.Subscribe
{
    public interface ISubscribeRepository
    {
        bool UsingEmail(EmailRequestModel request);

        bool UsingWhatsApp(WhatsAppRequestModel request);
    }
}
