using Bidding.Models.DatabaseModels.Auctions;

namespace Bidding.Models.ViewModels.Auctions.Details
{
    public class AuctionDetailsModel
    {
        public Auction Auction { get; set; }
        public AuctionItem AuctionItem { get; set; }
        public AuctionDetails AuctionDetails { get; set; }
        public AuctionCreator AuctionCreator { get; set; }
    }
}
