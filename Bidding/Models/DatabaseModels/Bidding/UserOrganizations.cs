using System;
using System.Collections.Generic;

namespace BiddingAPI.Models.DatabaseModels
{
    public partial class UserOrganizations
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int OrganizationId { get; set; }

        public Organizations Organization { get; set; }
        public Users User { get; set; }
    }
}
