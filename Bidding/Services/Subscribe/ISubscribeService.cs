using System.Threading.Tasks;
using Bidding.Models.ViewModels.Bidding.Subscribe;
namespace BiddingAPI.Services.Subscribe
{
    public interface ISubscribeService
    {
        Task<bool> UsingEmailAsync(EmailRequestModel request);

        Task<bool> UsingWhatsAppAsync(WhatsAppRequestModel request);

        Task<bool> UsingSurveyAsync(SurveyRequestModel request);
    }
}
