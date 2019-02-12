using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiddingAPI.Models.ViewModels.BaseModels;

namespace BiddingAPI.Models.ViewModels.Bidding.Auctions
{
    public class AuctionListRequestModel : BaseListRequestModel
    {
        public string AuctionStartDate { get; set; }
        public string AuctionEndDate { get; set; }
        public string TopCategoryIds { get; set; } // optional - ids for filter changes
    }
}
