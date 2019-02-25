using Bidding.Models.ViewModels.Bidding.Users.Add;
using Bidding.Repositories.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bidding.Services.Users
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository m_userRepository;

        public UsersService(IUsersRepository userRepository)
        {
            m_userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public bool UserExists(string email)
        {
            return m_userRepository.UserExists(email);
        }

        public bool Create(UserAddRequestModel request)
        {
            return m_userRepository.Create(request);
        }
    }
}
