export interface AuctionDetailsModel {
  aboutAuctionDetails: Auctions.AboutAuctionDetailsModel;
  vehicleAuction?: Auctions.VehicleAuctionDetailsModel;
  itemAuction?: Auctions.ItemAuctionDetailsModel;
  propertyAuction?: Auctions.PropertyAuctionDetailsModel;
  aboutAuctionCreator; // todo: kke: specify type when ready!
}
