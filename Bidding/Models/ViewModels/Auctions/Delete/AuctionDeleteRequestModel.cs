using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bidding.Models.ViewModels.BaseModels;

namespace Bidding.Models.ViewModels.Auctions.Delete
{
    public class AuctionDeleteRequestModel
    {
        public List<int> AuctionIds { get; set; }
    }
}
