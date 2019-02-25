using Bidding.Models.ViewModels.Bidding.Users.Add;
using Bidding.Models.ViewModels.Bidding.Users.Shared;
using Bidding.Shared.ErrorHandling.Errors;
using Bidding.Shared.Exceptions;
using Bidding.Shared.Utility;
using BiddingAPI.Models.DatabaseModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Bidding.Repositories.Users
{
    public class UsersRepository : IUsersRepository
    {
        private readonly BiddingContext m_context;

        public UsersRepository(BiddingContext context)
        {
            m_context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Can be used to check if this specific user already exists
        /// </summary>
        /// <param name="email">Users e-mail</param>
        /// <returns></returns>
        public bool UserExists(string email)
        {
            return
                m_context.Users.Any(usr => usr.UserEmail == email && usr.UserStatus == true);
        }

        public bool Create(UserAddRequestModel request)
        {
            User newUser = new User()
            {
                UserFirstName = request.UserFirstName,
                UserLastName = request.UserLastName,
                UserEmail = request.UserEmail.ToLower(),
                UserStatus = true,
                UserRoleId = request.UserRoleId.IsNotSpecified() ? 1 : request.UserRoleId, // todo: kke: pre-fetch default user role here
                UserUniqueIdentifier = request.UserUniqueIdentifier
            };

            var strategy = m_context.Database.CreateExecutionStrategy();
            strategy.Execute(() =>
            {
                try
                {
                    using (var transaction = m_context.Database.BeginTransaction())
                    {
                        m_context.Add(newUser);
                        m_context.SaveChanges();
                        transaction.Commit();
                    }
                }
                catch (Exception ex)
                {
                    throw new WebApiException(HttpStatusCode.BadRequest, UserErrorMessages.CouldNotCreateUser, ex);
                }
            });

            return true;
        }

        public IEnumerable<UserProfileModel> UserDetails(string email)
        {
            return (from usr in m_context.Users
                    join rol in m_context.Roles on usr.UserRoleId equals rol.RoleId
                    where usr.UserEmail == email && usr.UserStatus == true
                    select new UserProfileModel()
                    {
                        UserId = usr.UserId,
                        UserEmail = usr.UserEmail,
                        UserRole = rol.RoleName,
                        UserUniqueIdentifier = usr.UserUniqueIdentifier
                    });
        }
    }
}
