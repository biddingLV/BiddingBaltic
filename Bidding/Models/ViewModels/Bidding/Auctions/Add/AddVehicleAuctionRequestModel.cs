using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bidding.Models.ViewModels.Bidding.Auctions.Add
{
    public class AddVehicleAuctionRequestModel
    {
        public int AuctionTopCategoryId { get; set; }
        public int AuctionSubCategoryId { get; set; }
        public string VehicleMake { get; set; }
        public string VehicleModel { get; set; }
        public string VehicleManufacturingDate { get; set; }
        public string VehicleRegistrationNumber { get; set; }
        public string VehicleIdentificationNumber { get; set; }
        public string VehicleInspectionActive { get; set; }
        public string VehiclePower { get; set; }
        public string VehicleEngineSize { get; set; }
        public string VehicleFuelType { get; set; }
        public string VehicleTransmission { get; set; }
        public string VehicleGearbox { get; set; }
        public string VehicleEvaluation { get; set; }
    }
}
