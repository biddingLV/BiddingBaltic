using Bidding.Database.DatabaseModels.Auctions;
using Bidding.Database.DatabaseModels.Users;
using Bidding.Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bidding.Database.DatabaseModels.Auctions
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
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime ApplyTillDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public int AuctionCategoryId { get; set; }
        public Category Category { get; set; }

        [Required]
        public int AuctionTypeId { get; set; }
        public Bidding.Models.DatabaseModels.Type Type { get; set; }

        [Required]
        public int AuctionStatusId { get; set; }
        public AuctionStatus AuctionStatus { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public int CreatedBy { get; set; }
        public User User { get; set; }

        public DateTime LastUpdatedAt { get; set; }

        public int LastUpdatedBy { get; set; }

        public bool Deleted { get; set; }

        public List<AuctionItem> AuctionItems { get; set; }
    }
}
