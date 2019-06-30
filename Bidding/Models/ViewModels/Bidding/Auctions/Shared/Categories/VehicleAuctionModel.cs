using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bidding.Models.ViewModels.Bidding.Auctions.Shared.Categories
{
    public class VehicleAuctionModel
    {
        public string VehicleMake { get; set; }
        public string VehicleModel { get; set; }
        public int VehicleManufacturingYear { get; set; }
        public string VehicleRegistrationNumber { get; set; }
        public string VehicleIdentificationNumber { get; set; }
        public bool VehicleInspectionActive { get; set; }
        public int VehicleTransmissionId { get; set; }
        public string VehicleFuelType { get; set; }
        public string VehicleEngineSize { get; set; }
        public string VehicleAxis { get; set; }
        public string VehicleEvaluation { get; set; }
    }
}