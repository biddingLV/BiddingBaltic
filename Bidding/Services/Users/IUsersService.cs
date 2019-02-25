using Bidding.Models.ViewModels.Bidding.Users.Add;
using Bidding.Models.ViewModels.Bidding.Users.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bidding.Services.Users
{
    public interface IUsersService
    {
        bool UserExists(string email);
        bool Create(UserAddRequestModel request);
        UserProfileModel UserDetails(string email);
    }
}
