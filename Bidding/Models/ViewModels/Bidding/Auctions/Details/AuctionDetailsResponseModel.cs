using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bidding.Models.ViewModels.Bidding.Auctions.Details
{
    public class AuctionDetailsResponseModel
    {
        public int Id { get; set; } // todo: kke: why this is ID? is it the same as AuctionId?
        public int AuctionId { get; set; }
        public string VRN { get; set; } // todo: kke: full name here!
        public string VIN { get; set; } // todo: kke: full name here!
        public string Year { get; set; } // only year needed
        public int Evaluation { get; set; }
        public string AuctionType { get; set; }
    }
}
