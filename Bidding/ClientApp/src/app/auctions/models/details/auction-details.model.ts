import { AboutAuctionDetailsModel } from "./about-auction-details.model";
import { VehicleAuctionDetailsModel } from "./vehicle-auction-details.model";
import { ItemAuctionDetailsModel } from "./item-auction-details.model";
import { PropertyAuctionDetailsModel } from "./property-auction-details.model";
import { AuctionCreatorDetailsModel } from "./auction-creator-details.model";

export interface AuctionDetailsModel {
  aboutAuctionDetails: AboutAuctionDetailsModel;
  vehicleAuction?: VehicleAuctionDetailsModel;
  itemAuction?: ItemAuctionDetailsModel;
  propertyAuction?: PropertyAuctionDetailsModel;
  aboutAuctionCreator: AuctionCreatorDetailsModel;
}
