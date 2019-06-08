using Bidding.Models.ViewModels.Bidding.Auctions.Add.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bidding.Models.ViewModels.Bidding.Auctions.Add
{
    public class AddAuctionRequestModel
    {
        public VehicleAuctionModel VehicleAuction { get; set; }
        public ItemAuctionModel ItemAuction { get; set; }
        public PropertyAuctionModel PropertyAuction { get; set; }
    }
}
