using Bidding.Models.ViewModels.Bidding.Users.Add;
using Bidding.Models.ViewModels.Bidding.Users.Edit;
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
    public class UsersService
    {
        private readonly UsersRepository m_userRepository;

        public UsersService(UsersRepository userRepository)
        {
            m_userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public bool UserExists(string email)
        {
            if (email.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.BadRequest, UserErrorMessages.EmailNotSpecified); }

            return m_userRepository.UserExists(email);
        }

        /// <summary>
        /// Used in startup.cs file to add a new user to our internal DB, called on the first time sign-in!
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public bool Create(UserAddRequestModel request)
        {
            //if (email.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.BadRequest, UserErrorMessages.IncorrectSignInEmail); }
            return m_userRepository.Create(request);
        }

        public UserDetailsModel UserDetails(int userId)
        {
            if (userId.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.BadRequest, UserErrorMessages.UserNotValid); }

            return m_userRepository.UserDetails(userId).FirstOrDefault();
        }

        public UserProfileModel UserDetails(string email)
        {
            if (email.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.BadRequest, UserErrorMessages.EmailNotSpecified); }

            return m_userRepository.UserDetails(email).FirstOrDefault();
        }
    }
}
