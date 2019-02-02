using System;
using System.Collections.Generic;

namespace BiddingAPI.Models.DatabaseModels
{
    public partial class User
    {
        public User()
        {
            UserDetails = new HashSet<UserDetail>();
            UserOrganizations = new HashSet<UserOrganization>();
            UserRoles = new HashSet<UserRole>();
        }

        public int Id { get; set; }

        public ICollection<UserDetail> UserDetails { get; set; }
        public ICollection<UserOrganization> UserOrganizations { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
