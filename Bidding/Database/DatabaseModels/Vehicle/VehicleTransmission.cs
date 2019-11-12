using Bidding.Database.DatabaseModels.Auctions;
using Bidding.Database.DatabaseModels.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bidding.Database.DatabaseModels.Vehicle
{
    public class VehicleTransmission
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VehicleTransmissionId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        //[Required]
        //public int CreatedBy { get; set; }
        //public User User { get; set; }

        public DateTime LastUpdatedAt { get; set; }

        public int LastUpdatedBy { get; set; }

        public bool Deleted { get; set; }

        public List<AuctionDetails> AuctionDetails { get; set; }
    }
}
