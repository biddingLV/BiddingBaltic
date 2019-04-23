using BiddingAPI.Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bidding.Database.DatabaseModels.Auctions.Details
{
    public class AuctionVehicleDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AuctionVehicleDetailsId { get; set; }

        public string Make { get; set; }
        public string Model { get; set; }
        public string Power { get; set; }
        public DateTime ManufacturingDate { get; set; }
        public bool VehicleInspectionActive { get; set; }
        public string EngineSize { get; set; }
        public string FuelType { get; set; }
        public string Transmission { get; set; }
        public string Gearbox { get; set; }

        [MaxLength(50)]
        public string VehicleRegistrationNumber { get; set; }

        [MaxLength(50)]
        public string VehicleIdentificationNumber { get; set; }
        public int Evaluation { get; set; } // todo: kke: + price?

        [Required]
        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; }

        [DataType(DataType.Date)]
        public DateTime? LastUpdatedAt { get; set; }
        public int? LastUpdatedBy { get; set; }
        public bool Deleted { get; set; }

        // Relationship definitions
        [Required]
        public int CreatedBy { get; set; }
        public User User { get; set; }
    }
}
