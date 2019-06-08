namespace Auctions {
  export interface VehicleAuctionModel {
    auctionTopCategoryId: number;
    auctionSubCategoryId: number;
    vehicleMake: string;
    vehicleModel: string;
    vehicleManufacturingDate: string;
    vehicleRegistrationNumber: string;
    vehicleIdentificationNumber: string;
    vehicleInspectionActive: string;
    vehiclePower: string;
    vehicleEngineSize: string;
    vehicleFuelType: string;
    vehicleTransmission: string;
    vehicleGearbox: string;
    vehicleEvaluation: string;
  }
}
