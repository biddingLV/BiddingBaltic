using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bidding.Models.ViewModels.Bidding.Users.Shared
{
    public class UserProfileCookie
    {
        public int UserId { get; set; }
        public bool IsAuthenticated { get; set; }
        public string Email { get; set; }
    }
}
