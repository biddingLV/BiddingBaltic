using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bidding.Models.ViewModels.Bidding.Auctions.Add.About
{
    public class AboutAuctionModel
    {
        public string AuctionCreator { get; set; }
        public string AuctionAddress { get; set; }
        public string AuctionCreatorEmail { get; set; }
        public string AuctionCreatorPhone { get; set; }
        public int AuctionFormat { get; set; }
        public DateTime? AuctionStartDate { get; set; }
        public DateTime AuctionApplyTillDate { get; set; }
        public DateTime AuctionEndDate { get; set; }
    }
}
