using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiddingAPI.Models.ViewModels.BaseModels;

namespace BiddingAPI.Models.ViewModels.Bidding.Auctions
{
    public class AuctionListResponseModel : BaseListResponseModel
    {
        public List<AuctionListModel> Auctions { get; set; }
    }

    public class AuctionListModel
    {
        public string Brand { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string Type { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime StartDate { get; set; }
    }
}
