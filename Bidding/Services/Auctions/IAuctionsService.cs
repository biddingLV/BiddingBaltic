using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bidding.Models.ViewModels.Bidding.Auctions;
using Bidding.Models.ViewModels.Bidding.Categories;
using BiddingAPI.Models.DatabaseModels;
using BiddingAPI.Models.DatabaseModels.Bidding;
using BiddingAPI.Models.ViewModels.Bidding.Auctions;

namespace BiddingAPI.Services.Auctions
{
    public interface IAuctionsService
    {
        List<AuctionModel> Search(AuctionModel request);

        List<CategoryModel> Categories();

        bool Update(AuctionEditRequestModel request);

        bool Create(AuctionAddRequestModel request);

        bool Delete(AuctionDeleteRequestModel request);
    }
}
