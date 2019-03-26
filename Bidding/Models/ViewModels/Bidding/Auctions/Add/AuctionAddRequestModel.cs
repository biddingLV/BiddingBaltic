using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiddingAPI.Models.ViewModels.BaseModels;

namespace BiddingAPI.Models.ViewModels.Bidding.Auctions
{
    public class AuctionAddRequestModel
    {
        public string AuctionName { get; set; }
        public int AuctionTopCategoryId { get; set; }
        public int AuctionSubCategoryId { get; set; }
        public int AuctionFormatId { get; set; }
        public int AuctionStartingPrice { get; set; }
        public DateTime AuctionStartDate { get; set; }
        /// <summary>
        /// Need to apply date/until
        /// </summary>
        public DateTime AuctionTillDate { get; set; }
        public DateTime AuctionEndDate { get; set; }
        public int AuctionCreatorId { get; set; }
        /// <summary>
        /// Auction status for AuctionStatuses table
        /// </summary>
        public int AuctionStatusId { get; set; }
        public int AuctionConditionId { get; set; }
        public string AuctionDescription { get; set; }
        // todo: kke: also add image prop here!
    }
}
