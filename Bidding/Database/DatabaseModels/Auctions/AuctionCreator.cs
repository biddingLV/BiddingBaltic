using Bidding.Database.DatabaseModels.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bidding.Database.DatabaseModels.Auctions
{
    public partial class AuctionCreator
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AuctionCreatorId { get; set; }

        [Required]
        public int UserId { get; set; }
        public User User { get; set; }

        [Required]
        public string ContactEmail { get; set; }

        [Required]
        public string ContactPhone { get; set; }

        [Required]
        public string ContactAddress { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public int CreatedBy { get; set; }

        public DateTime LastUpdatedAt { get; set; }

        public int LastUpdatedBy { get; set; }

        public bool Deleted { get; set; }
    }
}
