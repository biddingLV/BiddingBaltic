using Bidding.Models.ViewModels.Bidding.Users.Add;
using Bidding.Models.ViewModels.Bidding.Users.Shared;
using Bidding.Shared.ErrorHandling.Errors;
using Bidding.Shared.Exceptions;
using Bidding.Shared.Utility;
using Bidding.Models.DatabaseModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Bidding.Database.Contexts;
using Bidding.Database.DatabaseModels.Users;
using Bidding.Models.ViewModels.Bidding.Users.Details;

namespace Bidding.Repositories.Users
{
    public class UsersRepository
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
                m_context.Users.Any(usr => usr.LoginEmail == email && usr.Deleted == false);
        }

        /// <summary>
        /// Used in startup.cs file to add a new user to our internal DB, called on the first time sign-in!
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public bool Create(UserAddRequestModel request)
        {
            // by default create user with User Role!
            Role defaultUserRole = m_context.Roles.FirstOrDefault(rol => rol.Name == "User");

            User newUser = new User()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                LoginEmail = request.LoginEmail.ToLower(),
                Deleted = false,
                RoleId = defaultUserRole.RoleId, // User
                UniqueIdentifier = request.UniqueIdentifier,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = 1 // todo: kke: this needs to be null!
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

        public IEnumerable<UserDetailsModel> UserDetails(int userId)
        {
            return (from usr in m_context.Users
                    where usr.UserId == userId && usr.Deleted == false
                    select new UserDetailsModel()
                    {
                        UserId = usr.UserId,
                        UserFirstName = usr.FirstName,
                        UserLastName = usr.LastName,
                        UserEmail = usr.LoginEmail
                    });
        }

        public IEnumerable<UserProfileModel> UserDetails(string email)
        {
            return (from usr in m_context.Users
                    join rol in m_context.Roles on usr.RoleId equals rol.RoleId
                    where usr.LoginEmail == email && usr.Deleted == false
                    select new UserProfileModel()
                    {
                        UserId = usr.UserId,
                        UserEmail = usr.LoginEmail,
                        UserRole = rol.Name,
                        UserUniqueIdentifier = usr.UniqueIdentifier
                    });
        }
    }
}
