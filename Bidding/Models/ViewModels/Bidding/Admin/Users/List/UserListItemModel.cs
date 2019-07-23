using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bidding.Models.ViewModels.Bidding.Admin.Users.List
{
    public class UserListItemModel
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string ContactEmail { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UserRole { get; set; }
        public string UserStatus { get; set; }
    }
}
