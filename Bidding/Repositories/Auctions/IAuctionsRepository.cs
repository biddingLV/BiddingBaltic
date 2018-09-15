using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiddingAPI.Models.ViewModels.Bidding.Auctions;

namespace BiddingAPI.Repositories.Auctions
{
    public interface IAuctionsRepository
    {
        Task<AuctionListResponseModel> Search(AuctionListRequestModel request, int? start, int? end);

        Task<bool> Update(AuctionEditRequestModel request);

        Task<bool> Create(AuctionAddRequestModel request);

        Task<bool> Delete(AuctionDeleteRequestModel request);
    }
}
