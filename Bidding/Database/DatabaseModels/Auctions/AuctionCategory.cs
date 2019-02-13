using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BiddingAPI.Models.DatabaseModels.Bidding
{
    public partial class AuctionCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AuctionCategoryId { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public int AuctionId { get; set; }

        public Category Category { get; set; }
        public Auction Auction { get; set; }
    }
}
