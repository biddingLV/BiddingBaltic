using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bidding.Models.ViewModels.Bidding.Auctions.Details
{
    public class ItemAuctionDetailsModel
    {
        public string ItemModel { get; set; }
        public int ItemManufacturingYear { get; set; }
        public string ItemConditionName { get; set; }
        public string ItemEvaluation { get; set; }
        public int ItemStartingPrice { get; set; }
    }
}
