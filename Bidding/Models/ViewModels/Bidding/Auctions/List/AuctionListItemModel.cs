using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bidding.Models.ViewModels.Bidding.Auctions.List
{
    public class AuctionListItemModel
    {
        public int AuctionId { get; set; }
        public string AuctionName { get; set; }
        public int AuctionStartingPrice { get; set; }
        public DateTime AuctionApplyTillDate { get; set; }
        public DateTime AuctionEndDate { get; set; }
        public string AuctionStatusName { get; set; }
    }
}