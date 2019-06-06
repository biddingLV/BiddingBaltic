using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiddingAPI.Models.ViewModels.BaseModels;

namespace BiddingAPI.Models.ViewModels.Bidding.Auctions
{
    public class AddItemAuctionRequestModel
    {
        public int AuctionTopCategoryId { get; set; }
        public int AuctionSubCategoryId { get; set; }
        public string ItemName { get; set; }
        public string ItemModel { get; set; }
        public string ItemManufacturingDate { get; set; }
        public string ItemEvaluation { get; set; }
        public string ItemStartingPrice { get; set; }
    }
}
