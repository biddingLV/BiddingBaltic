using Bidding.Models.DatabaseModels.Auctions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bidding.Models.DatabaseModels.Categories
{
    public partial class AuctionType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TypeId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public int AuctionCategoryId { get; set; }
        public AuctionCategory Category { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        //[Required]
        //public int CreatedBy { get; set; }
        //public User User { get; set; }

        public DateTime LastUpdatedAt { get; set; }

        public int LastUpdatedBy { get; set; }

        public List<Auction> Auctions { get; set; }
        public List<AuctionItem> AuctionItems { get; set; }
    }
}
