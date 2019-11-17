using Bidding.Models.ViewModels.Subscribe;

namespace Bidding.Services.Subscribe
{
    public interface ISubscribeService
    {
        bool UsingEmail(EmailRequestModel request);
        bool UsingWhatsApp(WhatsAppRequestModel request);
    }
}
