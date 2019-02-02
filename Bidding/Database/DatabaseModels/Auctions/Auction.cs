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

        [Required]
        [DataType(DataType.Date)]
        public DateTime AuctionEndDate { get; set; }

        // todo: kke: add auction creator id?

        [ForeignKey("AuctionId")] // todo: kke: do I need to specify this here?
        public ICollection<AuctionCategory> AuctionCategories { get; set; }
    }
}
