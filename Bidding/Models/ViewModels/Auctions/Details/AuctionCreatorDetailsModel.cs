using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bidding.Models.ViewModels.Auctions.Details
{
    public class AuctionCreatorDetailsModel
    {
        public string AuctionCreatorName { get; set; }
        public string AuctionCreatorAddress { get; set; }
        public string AuctionCreatorEmail { get; set; }
        public string AuctionCreatorPhone { get; set; }
        public string AuctionRequirements { get; set; }
    }
}
