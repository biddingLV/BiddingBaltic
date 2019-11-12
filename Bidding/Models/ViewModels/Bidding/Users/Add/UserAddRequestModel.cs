using Bidding.Database.DatabaseModels.Users;
using Bidding.Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bidding.Models.ViewModels.Bidding.Users.Add
{
    public class UserAddRequestModel// : User
    {
        public string LoginEmail { get; set; }
        public string UniqueIdentifier { get; set; }
    }
}
