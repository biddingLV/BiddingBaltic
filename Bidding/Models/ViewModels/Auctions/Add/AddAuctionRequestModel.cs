using Bidding.Models.ViewModels.Auctions.Shared.Categories;

namespace Bidding.Models.ViewModels.Auctions.Add
{
    public class AddAuctionRequestModel
    {
        public AboutAuctionModel AboutAuction { get; set; }
        public VehicleAuctionModel VehicleAuction { get; set; }
        public ItemAuctionModel ItemAuction { get; set; }
        public PropertyAuctionModel PropertyAuction { get; set; }
        public AboutAuctionCreatorModel AboutAuctionCreator { get; set; }
    }
}
