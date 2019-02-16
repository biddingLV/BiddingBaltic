using System;
using System.Collections.Generic;

namespace BiddingAPI.Models.DatabaseModels
{
    public partial class User
    {
        public User()
        {
            //UserDetails = new HashSet<UserDetails>();
            //UserOrganizations = new HashSet<UserOrganization>();
        }

        public int UserId { get; set; }

        //public ICollection<UserDetails> UserDetails { get; set; }
        //public ICollection<UserOrganization> UserOrganizations { get; set; }
    }
}
