using BiddingAPI.Models.DatabaseModels;
using BiddingAPI.Models.DatabaseModels.Bidding;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bidding.Database.DatabaseModels.Auctions
{
    public class AuctionType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AuctionTypeId { get; set; }

        [Required]
        public int TypeId { get; set; }
        public Type Type { get; set; }

        [Required]
        public int AuctionId { get; set; }
        public Auction Auction { get; set; }
    }
}
