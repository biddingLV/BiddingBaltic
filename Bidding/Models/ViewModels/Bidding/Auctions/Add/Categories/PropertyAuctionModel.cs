using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bidding.Models.ViewModels.Bidding.Auctions.Add.Categories
{
    public class PropertyAuctionModel
    {
        public int AuctionTopCategoryId { get; set; }
        public int AuctionSubCategoryId { get; set; }
        public string PropertyCoordinates { get; set; }
        public string PropertyRegion { get; set; }
    }
}
