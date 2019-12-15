namespace Bidding.Models.ViewModels.Auctions.Shared.Categories
{
    public class VehicleAuctionModel
    {
        public string VehicleMake { get; set; }
        public string VehicleModel { get; set; }
        public int VehicleManufacturingYear { get; set; }
        public string VehicleRegistrationNumber { get; set; }
        public string VehicleIdentificationNumber { get; set; }
        public bool VehicleInspectionActive { get; set; }
        public int? VehicleTransmissionId { get; set; }
        public int? VehicleFuelTypeId { get; set; }
        public string VehicleEngineSize { get; set; }
        public string VehicleAxis { get; set; }
        public string VehicleDimensionValue { get; set; }
        public int? VehicleDimensionType { get; set; }
        public string VehicleEvaluation { get; set; }
    }
}