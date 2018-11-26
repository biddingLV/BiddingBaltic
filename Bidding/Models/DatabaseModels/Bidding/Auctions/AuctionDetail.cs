using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiddingAPI.Models.DatabaseModels.Bidding
{
    public class AuctionDetail
    {
        // kke: WIP!
        public int Id { get; set; }
        public int AuctionId { get; set; }
        public string VehicleRegistrationNumber { get; set; }
        public string VehicleIdentificationNumber { get; set; }
        public string Year { get; set; } // only year needed
        public int Evaluation { get; set; }
        public string AuctionType { get; set; }
    }
}
