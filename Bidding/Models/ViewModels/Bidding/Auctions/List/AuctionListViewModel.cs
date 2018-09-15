using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiddingAPI.Models.ViewModels.Bidding.Auctions.List
{
    public class AuctionListViewModel
    {   
        public string Brand { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string Type { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime StartDate { get; set; }
        public int TotalCount { get; set; }
    }
}
