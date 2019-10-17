using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bidding.Database.DatabaseModels.Users
{
    public class UserRole
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserRoleId { get; set; }

        // note: kke: add mapping values!

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
