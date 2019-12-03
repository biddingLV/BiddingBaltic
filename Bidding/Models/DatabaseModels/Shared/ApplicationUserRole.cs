using Microsoft.AspNetCore.Identity;

namespace Bidding.Models.DatabaseModels.Shared
{
    public class ApplicationUserRole : IdentityUserRole<int>
    {
        public virtual ApplicationUser User { get; set; }
        public virtual ApplicationRole Role { get; set; }
    }
}
