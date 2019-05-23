using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiddingAPI.Models.ViewModels.BaseModels;

namespace BiddingAPI.Models.ViewModels.Bidding.Auctions
{
    public class AuctionEditRequestModel
    {
        public int AuctionId { get; set; }
        public string AuctionName { get; set; }
    }
}
