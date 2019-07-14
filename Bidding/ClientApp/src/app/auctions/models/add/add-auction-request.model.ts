namespace Auctions {
  export interface AddAuctionRequestModel {
    aboutAuction: Auctions.AboutAuctionModel;
    vehicleAuction?: Auctions.VehicleAuctionModel;
    itemAuction?: Auctions.ItemAuctionModel;
    propertyAuction?: Auctions.PropertyAuctionModel;
    aboutAuctionCreator: Auctions.AboutAuctionCreatorModel;
  }
}
