using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiddingAPI.Models.DatabaseModels.Bidding
{
    public class AuctionDetail
    {
        public int Id { get; set; }
        public int AuctionId { get; set; }
        public string Model { get; set; }
        // todo: kke: define Relationships
        // one-to-one relationship
    }
}
