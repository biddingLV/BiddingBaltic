using Bidding.Models.ViewModels.Subscribe;

namespace Bidding.Repositories.Subscribe
{
    public interface ISubscribeRepository
    {
        bool UsingEmail(EmailRequestModel request);

        bool UsingWhatsApp(WhatsAppRequestModel request);
    }
}
