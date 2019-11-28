using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bidding.Models.ViewModels.Auctions.Edit
{
    public class AboutAuctionEditDetailsModel
    {
        public string AuctionName { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal AuctionStartingPrice { get; set; }
        public DateTime? AuctionStartDate { get; set; }
        public DateTime AuctionApplyTillDate { get; set; }
        public DateTime AuctionEndDate { get; set; }
        public int AuctionStatusId { get; set; }
    }
}
