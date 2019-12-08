namespace Bidding.Models.ViewModels.Auctions.Details
{
    public class PropertyAuctionDetailsModel
    {
        public string PropertyCoordinates { get; set; }
        public string PropertyRegionName { get; set; }
        public int PropertyCadastreNumber { get; set; }
        public decimal PropertyMeasurementValue { get; set; }
        public string PropertyMeasurementTypeName { get; set; }
        public string PropertyAddress { get; set; }
        public int? PropertyFloorCount { get; set; }
        public int? PropertyRoomCount { get; set; }
    }
}
