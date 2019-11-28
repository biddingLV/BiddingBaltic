using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bidding.Models.ViewModels.Auctions.List
{
    public class AuctionListItemModel
    {
        public int AuctionId { get; set; }
        public string AuctionName { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal AuctionStartingPrice { get; set; }
        public DateTime AuctionApplyTillDate { get; set; }
        public DateTime AuctionEndDate { get; set; }
        public string AuctionStatusName { get; set; }
    }
}