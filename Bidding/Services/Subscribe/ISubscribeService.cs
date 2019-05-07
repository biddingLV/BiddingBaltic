using System.Threading.Tasks;
using Bidding.Models.ViewModels.Bidding.Subscribe;
namespace BiddingAPI.Services.Subscribe
{
    public interface ISubscribeService
    {
        bool UsingEmail(EmailRequestModel request);
        bool UsingWhatsApp(WhatsAppRequestModel request);
    }
}
