using System;
using System.Collections.Generic;

namespace BiddingAPI.Models.DatabaseModels
{
    public partial class Organization
    {
        public Organization()
        {
            UserOrganizations = new HashSet<UserOrganization>();
        }

        public int Id { get; set; }

        public ICollection<UserOrganization> UserOrganizations { get; set; }
    }
}
