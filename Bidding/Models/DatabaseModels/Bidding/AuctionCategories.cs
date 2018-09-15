using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiddingAPI.Models.DatabaseModels.Bidding
{
    public partial class AuctionCategories
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int AuctionId { get; set; }

        public Categories Category { get; set; }
        public Auctions Auction { get; set; }
    }
}
