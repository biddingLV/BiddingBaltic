using Bidding.Database.DatabaseModels.Auctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bidding.Models.ViewModels.Bidding.Auctions.Details
{
    public class AuctionDetailsModel
    {
        public Auction Auction { get; set; }
        public AuctionItem AuctionItem { get; set; }
        public AuctionDetails AuctionDetails { get; set; }
        public AuctionCreator AuctionCreator { get; set; }
    }
}
