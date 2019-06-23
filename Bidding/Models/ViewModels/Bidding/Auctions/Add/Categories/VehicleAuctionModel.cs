using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bidding.Models.ViewModels.Bidding.Auctions.Add.Categories
{
    public class VehicleAuctionModel
    {
        public string VehicleMake { get; set; }
        public string VehicleModel { get; set; }
        public DateTime VehicleManufacturingDate { get; set; }
        public string VehicleRegistrationNumber { get; set; }
        public string VehicleIdentificationNumber { get; set; }
        public bool VehicleInspectionActive { get; set; }
        public string VehicleTransmission { get; set; }
        public string VehicleFuelType { get; set; }
        public string VehicleEngineSize { get; set; }
        public string VehicleAxis { get; set; }
        public string VehicleEvaluation { get; set; }
    }
}