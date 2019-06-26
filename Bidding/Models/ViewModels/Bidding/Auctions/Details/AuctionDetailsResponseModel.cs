using Bidding.Models.ViewModels.Bidding.Auctions.Shared.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bidding.Models.ViewModels.Bidding.Auctions.Details
{
    public class AuctionDetailsResponseModel
    {
        //public int AuctionTopCategoryId { get; set; }

        ///// <summary>
        ///// Can be nullable because 'Mantu kopa' doesnt have sub-category
        ///// </summary>
        //public int? AuctionSubCategoryId { get; set; }
        //public string AuctionName { get; set; }
        //public int AuctionStartingPrice { get; set; }
        public VehicleAuctionModel VehicleAuction { get; set; }
        public ItemAuctionModel ItemAuction { get; set; }
        public PropertyAuctionModel PropertyAuction { get; set; }
        // public AboutAuctionModel AboutAuction { get; set; }
    }
}
