using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiddingAPI.Models.ViewModels.Bidding.Auctions;

namespace BiddingAPI.Services.Auctions
{
    public interface IAuctionsService
    {
        AuctionListResponseModel Search(AuctionListRequestModel request);

        bool Update(AuctionEditRequestModel request);

        bool Create(AuctionAddRequestModel request);

        bool Delete(AuctionDeleteRequestModel request);
    }
}
