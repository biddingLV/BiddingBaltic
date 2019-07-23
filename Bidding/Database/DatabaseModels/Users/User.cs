using Bidding.Database.DatabaseModels.Auctions;
using Bidding.Database.DatabaseModels.Item;
using Bidding.Database.DatabaseModels.Property;
using Bidding.Database.DatabaseModels.Shared;
using Bidding.Database.DatabaseModels.Vehicle;
using Bidding.Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Type = Bidding.Models.DatabaseModels.Type;

namespace Bidding.Database.DatabaseModels.Users
{
    public partial class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string MiddleName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LoginEmail { get; set; }

        [MaxLength(50)]
        public string ContactEmail { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }

        /// <summary>
        /// format: (identity provider)|(unique id in the provider)
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string UniqueIdentifier { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public int CreatedBy { get; set; }

        public DateTime LastUpdatedAt { get; set; }

        public int LastUpdatedBy { get; set; }

        public bool Deleted { get; set; }

        public List<Auction> Auctions { get; set; }
        public List<AuctionStatus> AuctionStatuses { get; set; }
        public List<VehicleTransmission> VehicleTransmissions { get; set; }
        public List<VehicleFuelType> VehicleFuelTypes { get; set; }
        public List<ItemCondition> ItemConditions { get; set; }
        public List<PropertyMeasurementType> PropertyMeasurementTypes { get; set; }
        public List<Region> Regions { get; set; }
        public List<Category> Categories { get; set; }
        public List<Type> Types { get; set; }
        public List<AuctionCondition> AuctionConditions { get; set; }
        public List<AuctionFormat> AuctionFormats { get; set; }
        public List<AuctionItem> AuctionItems { get; set; }
        public List<AuctionDetails> AuctionDetails { get; set; }
        public List<AuctionCreator> AuctionCreators { get; set; }
    }
}
