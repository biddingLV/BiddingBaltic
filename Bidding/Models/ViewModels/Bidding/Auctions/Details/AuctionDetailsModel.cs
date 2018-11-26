using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bidding.Models.ViewModels.Bidding.Auctions.Details
{
    public class AuctionDetailsModel
    {
        public int Id { get; set; }
        public int AuctionId { get; set; }
        public string VRN { get; set; }
        public string VIN { get; set; }
        public string Year { get; set; } // only year needed
        public int Evaluation { get; set; }
        public string AuctionType { get; set; }
    }
}
