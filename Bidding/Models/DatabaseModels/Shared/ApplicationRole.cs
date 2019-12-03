using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Bidding.Models.DatabaseModels.Shared
{
    public class ApplicationRole : IdentityRole<int>
    {
        public string Description { get; set; }
        public string PermissionsInRole { get; set; }

        public ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}
