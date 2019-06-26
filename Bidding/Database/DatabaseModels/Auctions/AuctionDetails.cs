using Bidding.Database.DatabaseModels.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bidding.Database.DatabaseModels.Auctions
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
        public int ManufacturingYear { get; set; }

        [MaxLength(50)]
        public string RegistrationNumber { get; set; }

        [MaxLength(50)]
        public string IdentificationNumber { get; set; }
        public bool? InspectionActive { get; set; }
        public string Transmission { get; set; }
        public string FuelType { get; set; }
        public string EngineSize { get; set; }
        public string Axis { get; set; }
        public string Name { get; set; }
        public int Condition { get; set; }
        public string Coordinates { get; set; }
        public string Region { get; set; }
        public int CadastreNumber { get; set; }
        public int MeasurementValue { get; set; }
        public string MeasurementType { get; set; }
        public string Address { get; set; }
        public int FloorCount { get; set; }
        public int RoomCount { get; set; }
        public string Evaluation { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public int CreatedBy { get; set; }
        public User User { get; set; }

        public DateTime LastUpdatedAt { get; set; }

        public int LastUpdatedBy { get; set; }

        public bool Deleted { get; set; }
    }
}
