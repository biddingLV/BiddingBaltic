using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bidding.Database.DatabaseModels
{
    public class ApplicationRole : IdentityRole<int>
    {
        public string Description { get; set; }
        public string PermissionsInRole { get; set; }
    }
}
