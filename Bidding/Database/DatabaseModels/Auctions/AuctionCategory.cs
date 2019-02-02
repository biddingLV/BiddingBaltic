using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiddingAPI.Models.DatabaseModels.Bidding
{
    public partial class AuctionCategory
    {
        public int AuctionCategoryId { get; set; }
        public int CategoryId { get; set; }
        public int AuctionId { get; set; }

        public Category Category { get; set; }
        public Auction Auction { get; set; }
    }
}
