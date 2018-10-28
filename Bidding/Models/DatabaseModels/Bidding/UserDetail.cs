using System;
using System.Collections.Generic;

namespace BiddingAPI.Models.DatabaseModels
{
    public partial class UserDetail
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int DetailId { get; set; }

        public User User { get; set; }
    }
}
