using System;
using System.Collections.Generic;

namespace BiddingAPI.Models.DatabaseModels
{
    public partial class UserOrganization
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int OrganizationId { get; set; }

        public Organization Organization { get; set; }
        public User User { get; set; }
    }
}
