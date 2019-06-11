using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bidding.Models.ViewModels.BaseModels;

namespace Bidding.Models.ViewModels.Bidding.Auctions.Add.Categories
{
    public class ItemAuctionModel
    {
        public int AuctionTopCategoryId { get; set; }
        public int AuctionSubCategoryId { get; set; }
        public string ItemName { get; set; } // todo: kke: move to the global level?
        public string ItemModel { get; set; }
        public string ItemManufacturingDate { get; set; }
        public string ItemEvaluation { get; set; }
        public int ItemStartingPrice { get; set; } // todo: kke: move to the global level?
    }
}
