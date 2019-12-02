using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Bidding.Models.DatabaseModels.Shared
{
    public class ApplicationUser : IdentityUser<int>
    {
        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }

        [MaxLength(150)]
        public string IdentityId { get; set; }

        [MaxLength(100)]
        public override string PhoneNumber { get; set; }
    }
}
