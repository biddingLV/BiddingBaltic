using BiddingAPI.Models.DatabaseModels.Bidding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bidding.Database.DatabaseModels.Auctions
{
    public class AuctionPropertyState
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AuctionPropertyStateId { get; set; }

        [Required]
        public int PropertyStateId { get; set; }

        [Required]
        public int AuctionId { get; set; }

        public BiddingAPI.Models.DatabaseModels.PropertyState PropertyState { get; set; }
        public Auction Auction { get; set; }
    }
}
