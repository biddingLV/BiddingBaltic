namespace Auctions {
  export interface AddAuctionRequestModel {
    vehicleAuction?: Auctions.VehicleAuctionModel;
    itemAuction?: Auctions.ItemAuctionModel;
    propertyAuction?: Auctions.PropertyAuctionModel;
  }
}
