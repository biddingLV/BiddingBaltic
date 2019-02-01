using System;
using System.Collections.Generic;
using Bidding.Models.ViewModels.Bidding.Auctions.Details;
using Bidding.Models.ViewModels.Bidding.Categories;
using BiddingAPI.Models.DatabaseModels.Bidding;
using BiddingAPI.Models.ViewModels.Bidding.Auctions;

namespace BiddingAPI.Repositories.Auctions
{
    public interface IAuctionsRepository
    {
        IEnumerable<Auction> ListWithSearch(AuctionListRequestModel request, int start, int end);

        IEnumerable<Auction> TotalAuctionCount(DateTime startDate, DateTime endDate);

        IEnumerable<AuctionDetailsResponseModel> Details(AuctionDetailsRequestModel request);

        IEnumerable<CategoryModel> Categories();

        bool Update(AuctionEditRequestModel request);

        bool Create(AuctionAddRequestModel request);

        bool Delete(AuctionDeleteRequestModel request);
    }
}
