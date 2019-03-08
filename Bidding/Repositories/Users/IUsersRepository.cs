using Bidding.Models.ViewModels.Bidding.Users.Add;
using Bidding.Models.ViewModels.Bidding.Users.Edit;
using Bidding.Models.ViewModels.Bidding.Users.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bidding.Repositories.Users
{
    public interface IUsersRepository
    {
        bool UserExists(string email);
        bool Create(UserAddRequestModel request);
        IEnumerable<UserDetailsModel> UserDetails(int userId);
        IEnumerable<UserProfileModel> UserDetails(string email);
    }
}
