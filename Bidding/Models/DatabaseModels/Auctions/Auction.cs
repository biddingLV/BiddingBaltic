using Bidding.Models.DatabaseModels.Categories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bidding.Models.DatabaseModels.Auctions
{
    public partial class Auction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AuctionId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal StartingPrice { get; set; }

        public DateTime? StartDate { get; set; }

        [Required]
        public DateTime ApplyTillDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public int AuctionCategoryId { get; set; }
        public Category Category { get; set; }

        [Required]
        public int AuctionTypeId { get; set; }
        public Categories.Type Type { get; set; }

        [Required]
        public int AuctionStatusId { get; set; }
        public AuctionStatus AuctionStatus { get; set; }

        [Required]
        public int AuctionFormatId { get; set; }
        public AuctionFormat AuctionFormat { get; set; }

        [Required]
        public int AuctionCreatorId { get; set; }
        public AuctionCreator AuctionCreator { get; set; }

        public DateTime CreatedAt { get; set; }

        //[Required]
        //public int CreatedBy { get; set; }
        //public User User { get; set; }

        public DateTime LastUpdatedAt { get; set; }

        public int LastUpdatedBy { get; set; }

        public bool Deleted { get; set; }

        public List<AuctionItem> AuctionItems { get; set; }
    }
}
