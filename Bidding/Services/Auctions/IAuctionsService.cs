using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bidding.Models.ViewModels.Bidding.Auctions;
using Bidding.Models.ViewModels.Bidding.Auctions.Details;
using Bidding.Models.ViewModels.Bidding.Filters;
using BiddingAPI.Models.DatabaseModels;
using BiddingAPI.Models.DatabaseModels.Bidding;
using BiddingAPI.Models.ViewModels.Bidding.Auctions;

namespace BiddingAPI.Services.Auctions
{
    public interface IAuctionsService
    {
        AuctionListResponseModel ListWithSearch(AuctionListRequestModel request);

        AuctionFilterModel Filters();

        AuctionDetailsResponseModel Details(AuctionDetailsRequestModel request);

        bool Update(AuctionEditRequestModel request);

        bool Create(AuctionAddRequestModel request);

        bool Delete(AuctionDeleteRequestModel request);
    }
}
