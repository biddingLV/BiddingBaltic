using BiddingAPI.Models.ViewModels.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bidding.Models.ViewModels.Bidding.Auctions
{
    public class AuctionListResponseModel : BaseListResponseModel
    {
        public List<AuctionItemModel> Auctions = new List<AuctionItemModel>();
    }

    public class AuctionItemModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int CreatorId { get; set; }
        public string CreatorFirstName { get; set; }
        public string CreatorLastName { get; set; }
    }
}
