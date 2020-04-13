using Bidding.Models.ViewModels.Auctions.Add;
using Bidding.Models.ViewModels.Auctions.Delete;
using Bidding.Models.ViewModels.Auctions.Details;
using Bidding.Models.ViewModels.Auctions.Edit;
using Bidding.Models.ViewModels.Auctions.List;
using Bidding.Models.ViewModels.Auctions.Shared;
using Bidding.Models.ViewModels.Filters;
using System.Threading.Tasks;

namespace Bidding.Services.Auctions
{
    public interface IAuctionService
    {
        AuctionListResponseModel GetActiveAuctions(AuctionListRequestModel request);
        AuctionListResponseModel GetAllAuctions(AuctionListRequestModel request);
        AuctionFilterModel Filters();
        CategoriesWithTypesModel CategoriesWithTypes();
        AuctionFormatModel Formats();
        AuctionStatusModel Statuses();
        Task<AuctionDetailsResponseModel> DetailsAsync(AuctionDetailsRequestModel request);
        AuctionEditDetailsResponseModel EditDetails(int auctionId);
        bool UpdateAuctionDetails(AuctionEditRequestModel request);
        Task<bool> DeleteAsync(AuctionDeleteRequestModel request);
        int Create(AddAuctionRequestModel request);
    }
}
