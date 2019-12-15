using Bidding.Models.DatabaseModels.Item;
using Bidding.Models.DatabaseModels.Property;
using Bidding.Models.DatabaseModels.Shared;
using Bidding.Models.DatabaseModels.Vehicle;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bidding.Models.DatabaseModels.Auctions
{
    public class AuctionDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AuctionDetailsId { get; set; }

        public int AuctionItemId { get; set; }
        public AuctionItem AuctionItem { get; set; }

        public string Make { get; set; }
        public string Model { get; set; }
        public int? ManufacturingYear { get; set; }

        [MaxLength(50)]
        public string RegistrationNumber { get; set; }

        [MaxLength(50)]
        public string IdentificationNumber { get; set; }
        public bool? InspectionActive { get; set; }

        public int? TransmissionId { get; set; }
        public VehicleTransmission VehicleTransmission { get; set; }

        public int? FuelTypeId { get; set; }
        public VehicleFuelType VehicleFuelType { get; set; }

        public string EngineSize { get; set; }
        public string Axis { get; set; }
        public string DimensionValue { get; set; }

        public int? DimensionTypeId { get; set; }
        public VehicleDimensionType VehicleDimensionType { get; set; }

        public int? ConditionId { get; set; }
        public ItemCondition ItemCondition { get; set; }

        public string Volume { get; set; }

        public int? CompanyTypeId { get; set; }
        public ItemCompanyType ItemCompanyType { get; set; }

        public string Coordinates { get; set; }

        public int? RegionId { get; set; }
        public Region Region { get; set; }

        public int? CadastreNumber { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? MeasurementValue { get; set; }

        public int? MeasurementTypeId { get; set; }
        public PropertyMeasurementType PropertyMeasurementType { get; set; }

        public string Address { get; set; }
        public int? FloorCount { get; set; }
        public int? RoomCount { get; set; }
        public string Evaluation { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        //[Required]
        //public int CreatedBy { get; set; }
        //public User User { get; set; }

        public DateTime LastUpdatedAt { get; set; }

        public int LastUpdatedBy { get; set; }

        public bool Deleted { get; set; }
    }
}
