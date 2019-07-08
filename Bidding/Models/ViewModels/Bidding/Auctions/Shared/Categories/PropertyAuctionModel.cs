using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bidding.Models.ViewModels.Bidding.Auctions.Shared.Categories
{
    public class PropertyAuctionModel
    {
        public string PropertyCoordinates { get; set; }
        public int PropertyRegion { get; set; }
        public int PropertyCadastreNumber { get; set; }
        public int PropertyMeasurementValue { get; set; }
        public int PropertyMeasurementType { get; set; }
        public string PropertyAddress { get; set; }
        public int? PropertyFloorCount { get; set; }
        public int? PropertyRoomCount { get; set; }
        public string PropertyEvaluation { get; set; }
    }
}
