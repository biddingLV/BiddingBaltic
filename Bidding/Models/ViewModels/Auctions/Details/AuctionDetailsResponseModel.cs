namespace Bidding.Models.ViewModels.Auctions.Details
{
    public class AuctionDetailsResponseModel
    {
        public AboutAuctionDetailsModel AboutAuctionDetails { get; set; }
        public VehicleAuctionDetailsModel VehicleAuction { get; set; }
        public ItemAuctionDetailsModel ItemAuction { get; set; }
        public PropertyAuctionDetailsModel PropertyAuction { get; set; }
        public AuctionCreatorDetailsModel AboutAuctionCreator { get; set; }
    }
}
