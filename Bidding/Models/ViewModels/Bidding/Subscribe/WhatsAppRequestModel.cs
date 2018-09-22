using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bidding.Models.ViewModels.Bidding.Subscribe
{
    public class WhatsAppRequestModel
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public List<string> Categories { get; set; }
    }
}
