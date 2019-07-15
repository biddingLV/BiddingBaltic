using Bidding.Models.ViewModels.Bidding.Auctions.Add;
using Bidding.Models.ViewModels.Bidding.Auctions.Shared.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bidding.Models.ViewModels.Bidding.Auctions.Details
{
    public class AuctionDetailsResponseModel
    {
        public AboutAuctionDetailsModel AboutAuctionDetails { get; set; }
        public VehicleAuctionDetailsModel VehicleAuction { get; set; }
        public ItemAuctionDetailsModel ItemAuction { get; set; }
        public PropertyAuctionDetailsModel PropertyAuction { get; set; }
        public AboutAuctionCreatorModel AboutAuctionCreator { get; set; }
    }
}
