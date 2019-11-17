using Microsoft.AspNetCore.Identity;

namespace Bidding.Models.DatabaseModels.Shared
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdentityId { get; set; }
        // todo: kke: add Nickname here if needed!
    }
}
