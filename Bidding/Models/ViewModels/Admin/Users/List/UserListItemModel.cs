using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bidding.Models.ViewModels.Admin.Users.List
{
    public class UserListItemModel
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string LoginEmail { get; set; }
        public string ContactEmail { get; set; }
        public string Phone { get; set; }
        public string UserRole { get; set; }
        public string UserStatus { get; set; }
    }
}
