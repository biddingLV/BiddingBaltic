import { AboutAuctionDetailsModel } from "./about-auction-details.model";

export interface AuctionDetailsModel {
  aboutAuctionDetails: AboutAuctionDetailsModel;
  vehicleAuction?: Auctions.VehicleAuctionDetailsModel;
  itemAuction?: Auctions.ItemAuctionDetailsModel;
  propertyAuction?: Auctions.PropertyAuctionDetailsModel;
  aboutAuctionCreator: Auctions.AuctionCreatorDetailsModel;
}
