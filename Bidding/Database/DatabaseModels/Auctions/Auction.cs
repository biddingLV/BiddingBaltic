using Bidding.Database.DatabaseModels.Auctions;
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
        public Auction()
        {
            AuctionCategories = new HashSet<AuctionCategory>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AuctionId { get; set; }

        [Required]
        [MaxLength(50)]
        public string AuctionName { get; set; }

        [Range(0, 1000000)]
        public int AuctionStartingPrice { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime AuctionStartDate { get; set; }

        // todo: kke: does this needs to be required?
        [Required]
        [DataType(DataType.Date)]
        public DateTime AuctionApplyDate { get; set; }

        // todo: kke: does this needs to be required?
        [Required]
        [DataType(DataType.Date)]
        public DateTime AuctionEndDate { get; set; }

        [Required]
        [Range(1, 10000)]
        public int AuctionStatusId { get; set; } // todo: kke: this needs to be fKey or constraint based on auction statuses table!

        [Required]
        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; }

        [Required]
        public int CreatedBy { get; set; }

        [DataType(DataType.Date)]
        public DateTime? LastUpdatedAt { get; set; }
        public int? LastUpdatedBy { get; set; }
        public bool Deleted { get; set; }

        public AuctionDetails Details { get; set; }

        public User CreatedByUser { get; set; }

        [ForeignKey("AuctionId")]
        public ICollection<AuctionCategory> AuctionCategories { get; set; }
    }
}
