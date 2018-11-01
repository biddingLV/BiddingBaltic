using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bidding.Models.ViewModels.Bidding.Auctions;
using BiddingAPI.Models.DatabaseModels.Bidding;
using BiddingAPI.Models.ViewModels.Bidding.Auctions;

namespace BiddingAPI.Services.Auctions
{
    public interface IAuctionsService
    {
        List<AuctionModel> Search(AuctionModel request);

        bool Update(AuctionEditRequestModel request);

        bool Create(AuctionAddRequestModel request);

        bool Delete(AuctionDeleteRequestModel request);
    }
}
