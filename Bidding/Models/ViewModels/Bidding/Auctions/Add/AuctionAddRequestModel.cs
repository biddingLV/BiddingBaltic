using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiddingAPI.Models.ViewModels.BaseModels;

namespace BiddingAPI.Models.ViewModels.Bidding.Auctions
{
    public class AuctionAddRequestModel
    {
        public string AuctionName { get; set; }
        public string Description { get; set; }
        public int StartingPrice { get; set; }
        //public string StartDate { get; set; }
        public string Creator { get; set; }
    }
}
