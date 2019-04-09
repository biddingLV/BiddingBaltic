using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bidding.Models.ViewModels.Bidding.Auctions.Details
{
    public class AuctionDetailsRequestModel
    {
        public int AuctionId { get; set; }

        public int AuctionFormatId { get; set; } // todo: kke: why this is here?

        public int AuctionConditionId { get; set; } // todo: kke: why this is here?
    }
}
