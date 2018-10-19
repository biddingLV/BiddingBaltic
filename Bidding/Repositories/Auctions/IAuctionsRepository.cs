using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiddingAPI.Models.ViewModels.Bidding.Auctions;

namespace BiddingAPI.Repositories.Auctions
{
    public interface IAuctionsRepository
    {
        AuctionListResponseModel Search(AuctionListRequestModel request, int? start, int? end);

        bool Update(AuctionEditRequestModel request);

        bool Create(AuctionAddRequestModel request);

        bool Delete(AuctionDeleteRequestModel request);
    }
}
