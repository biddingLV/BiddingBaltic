using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BiddingAPI.Models.DatabaseModels.Bidding
{
    public class AuctionDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AuctionDetailsId { get; set; }

        [MaxLength(50)]
        public string Description { get; set; }

        // Relationship definitions
        public int AuctionId { get; set; }
        public Auction Auction { get; set; }

        [Required]
        public int AuctionFormatId { get; set; }
        public AuctionFormat AuctionFormat { get; set; }

        public int AuctionConditionId { get; set; }
        public AuctionCondition AuctionCondition { get; set; }
    }
}
