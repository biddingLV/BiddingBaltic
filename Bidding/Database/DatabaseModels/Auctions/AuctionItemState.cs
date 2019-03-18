using BiddingAPI.Models.DatabaseModels.Bidding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bidding.Database.DatabaseModels.Auctions
{
    public class AuctionItemState
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AuctionItemStateId { get; set; }

        [Required]
        public int ItemStateId { get; set; }

        [Required]
        public int AuctionId { get; set; }

        public BiddingAPI.Models.DatabaseModels.ItemState ItemState { get; set; }
        public Auction Auction { get; set; }
    }
}
