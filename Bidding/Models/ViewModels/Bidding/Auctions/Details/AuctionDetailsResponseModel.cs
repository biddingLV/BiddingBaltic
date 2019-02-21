using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bidding.Models.ViewModels.Bidding.Auctions.Details
{
    public class AuctionDetailsResponseModel
    {
        public string AuctionName { get; set; }
        public string CategoryName { get; set; }
        public string TypeName { get; set; }
        public int AuctionStartingPrice { get; set; }
        public DateTime AuctionStartDate { get; set; }
        public DateTime AuctionEndDate { get; set; }
    }
}
