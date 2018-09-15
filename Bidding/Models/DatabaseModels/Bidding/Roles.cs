using System;
using System.Collections.Generic;

namespace BiddingAPI.Models.DatabaseModels
{
    public partial class Roles
    {
        public Roles()
        {
            UserRoles = new HashSet<UserRoles>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool? Status { get; set; }

        public ICollection<UserRoles> UserRoles { get; set; }
    }
}
