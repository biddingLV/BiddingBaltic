using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bidding.Models.ViewModels.Auctions.Details
{
    public class AboutAuctionDetailsModel
    {
        public string AuctionName { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal AuctionStartingPrice { get; set; }
        public string AuctionFormat { get; set; }
        public DateTime? AuctionStartDate { get; set; }
        public DateTime AuctionApplyTillDate { get; set; }
        public DateTime AuctionEndDate { get; set; }
        public string ItemEvaluation { get; set; }
        public List<string> AuctionImageUrls { get; set; }
        public List<string> AuctionDocumentUrls { get; set; }     
    }
}
