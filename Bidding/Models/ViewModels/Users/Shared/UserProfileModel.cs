using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bidding.Models.ViewModels.Users.Shared
{
    public class UserProfileModel
    {
        public int UserId { get; set; }
        public string UserContactEmail { get; set; }
        public string UserRole { get; set; }
    }
}
