using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiddingAPI.Models.ApplicationModels.Configuration
{
    public class Authentication
    {
        public string Domain { get; set; }
        public string Authority { get; set; }
        public string Audience { get; set; }
    }
}
