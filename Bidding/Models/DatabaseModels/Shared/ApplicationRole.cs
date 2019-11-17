using Microsoft.AspNetCore.Identity;

namespace Bidding.Models.DatabaseModels.Shared
{
    public class ApplicationRole : IdentityRole<int>
    {
        public string Description { get; set; }
        public string PermissionsInRole { get; set; }
    }
}
