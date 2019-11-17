using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bidding.Models.ViewModels.BaseModels;

namespace Bidding.Models.ViewModels.Auctions.Shared.Categories
{
    public class ItemAuctionModel
    {
        public string ItemModel { get; set; }
        public int ItemManufacturingYear { get; set; }
        public int? ItemConditionId { get; set; }
        public string ItemEvaluation { get; set; }
        public int ItemStartingPrice { get; set; }
    }
}
