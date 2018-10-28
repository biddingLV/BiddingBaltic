using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public int Id { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public int Price { get; set; } // could be string also!
        public string Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public ICollection<AuctionCategory> AuctionCategories { get; set; }
    }
}
