using System;

namespace Bidding.Models.ViewModels.Auctions.Add
{
    public class AboutAuctionModel
    {
        public int AuctionTopCategoryId { get; set; }

        /// <summary>
        /// Can be nullable because 'Mantu kopa' doesnt have sub-category
        /// </summary>
        public int? AuctionSubCategoryId { get; set; }
        public string AuctionName { get; set; }

        /// <summary>
        /// Defined as a string to support 694.21 && 694,21 both formats.
        /// </summary>
        public string AuctionStartingPrice { get; set; }
        public bool AuctionValueAddedTax { get; set; }
        public int AuctionFormatId { get; set; }
        public DateTime? AuctionStartDate { get; set; }
        public DateTime AuctionApplyTillDate { get; set; }
        public DateTime AuctionEndDate { get; set; }
    }
}
