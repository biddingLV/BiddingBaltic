using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bidding.Models.ViewModels.Bidding.Auctions.Add
{
    public class AddPropertyAuctionRequestModel
    {
        public int AuctionTopCategoryId { get; set; }
        public int AuctionSubCategoryId { get; set; }
        public string PropertyCoordinates { get; set; }
        public string PropertyRegion { get; set; }
    }
}
