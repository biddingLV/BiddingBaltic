using Bidding.Models.ViewModels.Bidding.Users.Add;
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
    }
}
