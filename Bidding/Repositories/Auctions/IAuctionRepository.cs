using Bidding.Models.DatabaseModels.Auctions;
using Bidding.Models.ViewModels.Auctions.Add;
using Bidding.Models.ViewModels.Auctions.Delete;
using Bidding.Models.ViewModels.Auctions.Details;
using Bidding.Models.ViewModels.Auctions.Edit;
using Bidding.Models.ViewModels.Auctions.List;
using Bidding.Models.ViewModels.Auctions.Shared;
using Bidding.Models.ViewModels.Filters;
using Bidding.Models.ViewModels.Shared.Categories;
using Bidding.Models.ViewModels.Shared.Types;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bidding.Repositories.Auctions
{
    public interface IAuctionRepository
    {
        IEnumerable<AuctionListItemModel> GetActiveAuctions(AuctionListRequestModel request, int startFrom, int endAt, DateTime auctionsFromDate);
        IEnumerable<Auction> ActiveAuctionCount();
        IEnumerable<Auction> AllAuctionCount();
        IEnumerable<TopCategoryFilterModel> LoadActiveTopCategoriesWithCount();
        IEnumerable<SubCategoryFilterModel> LoadActiveSubCategoriesWithCount();
        IEnumerable<CategoryModel> LoadTopCategories();
        IEnumerable<TypeModel> LoadSubCategories();
        IEnumerable<AuctionFormatItemModel> Formats();
        IEnumerable<AuctionStatusItemModel> Statuses();
        Task<AuctionDetailsResponseModel> DetailsAsync(AuctionDetailsRequestModel request);
        int CreateAuction(AddAuctionRequestModel request, int loggedInUserId);
        AuctionEditDetailsResponseModel EditDetails(int auctionId);
        bool UpdateAuctionDetails(AuctionEditRequestModel request, int loggedInUserId);
        Task<bool> DeleteAsync(AuctionDeleteRequestModel request, int loggedInUserId);

    }
}
