using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiddingAPI.Models.DatabaseModels.Bidding
{
    public partial class Auctions
    {
        public Auctions()
        {
            AuctionCategories = new HashSet<AuctionCategories>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public int Price { get; set; } // could be string also!
        public string Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public ICollection<AuctionCategories> AuctionCategories { get; set; }
    }
}
