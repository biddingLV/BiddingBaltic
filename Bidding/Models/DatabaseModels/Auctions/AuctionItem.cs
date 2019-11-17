using Bidding.Models.DatabaseModels.Categories;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Bidding.Models.DatabaseModels.Auctions
{
    public class AuctionItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AuctionItemId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public int AuctionId { get; set; }
        public Auction Auction { get; set; }

        [Required]
        public int AuctionItemCategoryId { get; set; }
        public Category Category { get; set; }

        [Required]
        public int AuctionItemTypeId { get; set; }
        public Categories.Type Type { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        //[Required]
        //public int CreatedBy { get; set; }
        //public User User { get; set; }

        public DateTime LastUpdatedAt { get; set; }

        public int LastUpdatedBy { get; set; }

        public bool Deleted { get; set; }
    }
}
