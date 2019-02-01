using BiddingAPI.Models.DatabaseModels.Bidding;
using BiddingAPI.Models.ViewModels.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bidding.Models.ViewModels.Bidding.Auctions
{
    public class AuctionListResponseModel : BaseListResponseModel
    {
        public List<Auction> Auctions { get; set; }
    }
}
