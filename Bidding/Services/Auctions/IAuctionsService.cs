using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiddingAPI.Models.ViewModels.Bidding.Auctions;

namespace BiddingAPI.Services.Auctions
{
    public interface IAuctionsService
    {
        Task<AuctionListResponseModel> Search(AuctionListRequestModel request);

        Task<bool> Update(AuctionEditRequestModel request);

        Task<bool> Create(AuctionAddRequestModel request);

        Task<bool> Delete(AuctionDeleteRequestModel request);
    }
}
