using Bidding.Database.DatabaseModels.Auctions;
using BiddingAPI.Models.DatabaseModels.Bidding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BiddingAPI.Models.DatabaseModels.Bidding
{
    public partial class AuctionCondition
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AuctionConditionId { get; set; }

        [Required]
        public string AuctionConditionName { get; set; }

        public List<AuctionDetails> AuctionDetails { get; set; }

    }
}
