using BiddingAPI.Models.DatabaseModels.Bidding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bidding.Database.DatabaseModels.Auctions
{
    public partial class AuctionStatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AuctionStatusId { get; set; }

        [Required]
        [MaxLength(50)]
        public string AuctionStatusName { get; set; }

        [Required]
        public bool Status { get; set; }

        [ForeignKey("AuctionStatusId")]
        public AuctionDetails AuctionDetails { get; set; }

        public Auction Auctions { get; set; }
    }
}
