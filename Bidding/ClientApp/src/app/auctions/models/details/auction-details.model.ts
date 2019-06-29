export interface AuctionDetailsModel {
  auctionName: string;
  auctionStartingPrice: number;
  vehicleAuction?: Auctions.VehicleAuctionModel;
  itemAuction?: Auctions.ItemAuctionModel;
  propertyAuction?: Auctions.PropertyAuctionModel;
  aboutAuction: Auctions.AboutAuctionModel;
}
