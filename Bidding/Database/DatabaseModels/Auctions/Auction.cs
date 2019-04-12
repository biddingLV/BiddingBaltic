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
    public partial class Auction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AuctionId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Range(0, 1000000)]
        public int StartingPrice { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime? ApplyDate { get; set; } // todo: kke: is this right?

        [Required]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; } // todo: kke: is this right?

        [Required]
        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; }

        [DataType(DataType.Date)]
        public DateTime? LastUpdatedAt { get; set; }
        public int? LastUpdatedBy { get; set; }
        public bool Deleted { get; set; }

        // Relationship definitions
        [Required]
        public int CreatedBy { get; set; }
        public User User { get; set; }

        public int AuctionStatusId { get; set; }
        public AuctionStatus AuctionStatus { get; set; }

        public List<AuctionCategory> AuctionCategories { get; set; }
        public List<AuctionType> AuctionTypes { get; set; }
        public AuctionDetails AuctionDetails { get; set; }
    }
}
