import { AboutAuctionModel } from "./about-auction.model";
import { AboutAuctionCreatorModel } from "./about-auction-creator.model";
import { VehicleAuctionModel } from "./categories/vehicle-auction.model";
import { ItemAuctionModel } from "./categories/item-auction.model";
import { PropertyAuctionModel } from "./categories/property-auction.model";

export interface AddAuctionRequestModel {
  aboutAuction: AboutAuctionModel;
  vehicleAuction?: VehicleAuctionModel;
  itemAuction?: ItemAuctionModel;
  propertyAuction?: PropertyAuctionModel;
  aboutAuctionCreator: AboutAuctionCreatorModel;
}
