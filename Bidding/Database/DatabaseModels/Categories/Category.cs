using BiddingAPI.Models.DatabaseModels.Bidding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BiddingAPI.Models.DatabaseModels
{
    public partial class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }

        [DataType(DataType.Date)]
        public DateTime? LastUpdatedAt { get; set; }
        public int? LastUpdatedBy { get; set; }
        public bool Deleted { get; set; }

        // Relationship definitions
        public int AuctionId { get; set; }
        public Auction Auction { get; set; }
        public List<AuctionCategory> AuctionCategories { get; set; }
        public List<CategoryType> CategoryTypes { get; set; }
    }
}
