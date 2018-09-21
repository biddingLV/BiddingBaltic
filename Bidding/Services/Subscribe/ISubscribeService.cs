using System.Threading.Tasks;
using Bidding.Models.ViewModels.Bidding.Subscribe;
namespace BiddingAPI.Services.Subscribe
{
    public interface ISubscribeService
    {
        Task<bool> UsingEmail(EmailRequestModel request);

        Task<bool> UsingWhatsApp(WhatsAppRequestModel request);

        Task<bool> UsingSurvey(SurveyRequestModel request);
    }
}
