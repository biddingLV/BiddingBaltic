using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bidding.Models.ViewModels.Bidding.Users.Shared
{
    public class UserProfileModel
    {
        public int UserId { get; set; }
        public string UserEmail { get; set; }
        public string UserRole { get; set; }
        public string UserUniqueIdentifier { get; set; }
    }
}
