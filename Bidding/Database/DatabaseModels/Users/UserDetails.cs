using System;
using System.Collections.Generic;

namespace BiddingAPI.Models.DatabaseModels
{
    public partial class UserDetails
    {
        // todo: kke: maybe make primary key as a foreign key?
        public int UserDetailsId { get; set; }
        public int UserId { get; set; }

        public User User { get; set; }
    }
}
