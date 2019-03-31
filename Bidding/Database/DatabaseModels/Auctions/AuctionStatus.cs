using BiddingAPI.Models.DatabaseModels;
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
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; }

        [DataType(DataType.Date)]
        public DateTime? LastUpdatedAt { get; set; }
        public int? LastUpdatedBy { get; set; }
        public bool Deleted { get; set; }

        // Relationship definitions
        public List<Auction> Auctions { get; set; }

        [Required]
        public int CreatedBy { get; set; }
        public User User { get; set; }
    }
}
