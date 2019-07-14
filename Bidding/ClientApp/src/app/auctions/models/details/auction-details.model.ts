export interface AuctionDetailsModel {
  aboutAuctionDetails: Auctions.AboutAuctionDetailsModel;
  vehicleAuction?: Auctions.VehicleAuctionDetailsModel;
  itemAuction?: Auctions.ItemAuctionModel;
  propertyAuction?: Auctions.PropertyAuctionModel;
  aboutAuctionCreator; // todo: kke: specify type when ready!
}
