using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bidding.Models.ViewModels.Auctions.Edit
{
    public class AboutAuctionEditDetailsModel
    {
        public string AuctionName { get; set; }
        public int AuctionStartingPrice { get; set; }
        public DateTime? AuctionStartDate { get; set; }
        public DateTime AuctionApplyTillDate { get; set; }
        public DateTime AuctionEndDate { get; set; }
        public int AuctionStatusId { get; set; }
    }
}
