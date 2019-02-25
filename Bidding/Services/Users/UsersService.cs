using Bidding.Models.ViewModels.Bidding.Users.Add;
using Bidding.Models.ViewModels.Bidding.Users.Shared;
using Bidding.Repositories.Users;
using Bidding.Shared.ErrorHandling.Errors;
using Bidding.Shared.Exceptions;
using Bidding.Shared.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
            if (email.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.BadRequest, UserErrorMessages.EmailNotSpecified); }

            return m_userRepository.UserExists(email);
        }

        public bool Create(UserAddRequestModel request)
        {
            //if (email.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.BadRequest, UserErrorMessages.IncorrectSignInEmail); }
            return m_userRepository.Create(request);
        }

        public UserProfileModel UserDetails(string email)
        {
            if (email.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.BadRequest, UserErrorMessages.EmailNotSpecified); }

            return m_userRepository.UserDetails(email).FirstOrDefault();
        }
    }
}
