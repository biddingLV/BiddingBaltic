using System;
using System.Collections.Generic;

namespace BiddingAPI.Models.DatabaseModels
{
    public partial class Users
    {
        public Users()
        {
            UserDetails = new HashSet<UserDetails>();
            UserOrganizations = new HashSet<UserOrganizations>();
            UserRoles = new HashSet<UserRoles>();
        }

        public int Id { get; set; }

        public ICollection<UserDetails> UserDetails { get; set; }
        public ICollection<UserOrganizations> UserOrganizations { get; set; }
        public ICollection<UserRoles> UserRoles { get; set; }
    }
}
