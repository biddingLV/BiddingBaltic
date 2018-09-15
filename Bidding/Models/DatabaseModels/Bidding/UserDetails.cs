using System;
using System.Collections.Generic;

namespace BiddingAPI.Models.DatabaseModels
{
    public partial class UserDetails
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int DetailId { get; set; }

        public Users User { get; set; }
    }
}
