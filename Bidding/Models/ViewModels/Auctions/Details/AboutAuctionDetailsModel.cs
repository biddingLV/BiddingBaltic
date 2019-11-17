using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bidding.Models.ViewModels.Auctions.Details
{
    public class AboutAuctionDetailsModel
    {
        public string AuctionName { get; set; }
        public int AuctionStartingPrice { get; set; }
        public string AuctionFormat { get; set; }
        public DateTime? AuctionStartDate { get; set; }
        public DateTime AuctionApplyTillDate { get; set; }
        public DateTime AuctionEndDate { get; set; }
    }
}
