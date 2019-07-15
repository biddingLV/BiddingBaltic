using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bidding.Models.ViewModels.Bidding.Auctions.Details
{
    public class PropertyAuctionDetailsModel
    {
        public string PropertyCoordinates { get; set; }
        public string PropertyRegionName { get; set; }
        public int PropertyCadastreNumber { get; set; }
        public int PropertyMeasurementValue { get; set; }
        public string PropertyMeasurementTypeName { get; set; }
        public string PropertyAddress { get; set; }
        public int? PropertyFloorCount { get; set; }
        public int? PropertyRoomCount { get; set; }
        public string PropertyEvaluation { get; set; }
    }
}
