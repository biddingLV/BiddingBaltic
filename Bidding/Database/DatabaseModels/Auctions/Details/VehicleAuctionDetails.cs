using Bidding.Database.DatabaseModels.Users;
using Bidding.Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bidding.Database.DatabaseModels.Auctions.Details
{
    public class VehicleAuctionDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VehicleAuctionDetailsId { get; set; }

        public int AuctionItemId { get; set; }
        public AuctionItem AuctionItem { get; set; }

        public string Make { get; set; }
        public string Model { get; set; }
        public string Power { get; set; }
        public DateTime ManufacturingDate { get; set; }
        public bool InspectionActive { get; set; }
        public string EngineSize { get; set; }
        public string FuelType { get; set; }
        public string Transmission { get; set; }
        public string Gearbox { get; set; }

        [MaxLength(50)]
        public string RegistrationNumber { get; set; }

        [MaxLength(50)]
        public string IdentificationNumber { get; set; }
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
