using System;
using System.Collections.Generic;

namespace BiddingAPI.Models.DatabaseModels
{
    public partial class Organizations
    {
        public Organizations()
        {
            UserOrganizations = new HashSet<UserOrganizations>();
        }

        public int Id { get; set; }

        public ICollection<UserOrganizations> UserOrganizations { get; set; }
    }
}
