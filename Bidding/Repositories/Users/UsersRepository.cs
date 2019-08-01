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
using System.Data.SqlClient;
using System.Data;
using Bidding.Models.ViewModels.Bidding.Admin.Users.List;

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
        /// Used in startup.cs file to add a new user to our internal DB, called on the first time sign-up!
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public bool Create(UserAddRequestModel request) // todo: kke: naming needs to be something like create on signUp!
        {
            // by default create user with User Role!
            Role defaultUserRole = m_context.Roles.FirstOrDefault(rol => rol.Name == "User");

            User newUser = new User()
            {
                LoginEmail = request.LoginEmail.ToLower(),
                ContactEmail = request.LoginEmail.ToLower(),
                RoleId = defaultUserRole.RoleId, // User role
                UniqueIdentifier = request.UniqueIdentifier,
                CreatedAt = DateTime.UtcNow,
                LastUpdatedAt = DateTime.UtcNow,
                Deleted = false
            };

            try
            {
                m_context.Add(newUser);
                m_context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new WebApiException(HttpStatusCode.BadRequest, UserErrorMessages.CouldNotCreateUser, ex);
            }

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
            return from usr in m_context.Users
                   join rol in m_context.Roles on usr.RoleId equals rol.RoleId
                   where usr.LoginEmail == email && usr.Deleted == false
                   select new UserProfileModel()
                   {
                       UserId = usr.UserId,
                       UserContactEmail = usr.ContactEmail,
                       UserRole = rol.Name
                   };
        }

        public IEnumerable<UserListItemModel> ListWithSearch(UserListRequestModel request, int startFrom, int endAt)
        {
            try
            {
                SqlParameter startPaginationFrom = new SqlParameter
                {
                    ParameterName = "start",
                    Direction = ParameterDirection.Input,
                    Value = startFrom,
                    SqlDbType = SqlDbType.Int
                };

                SqlParameter endPaginationAt = new SqlParameter
                {
                    ParameterName = "end",
                    Direction = ParameterDirection.Input,
                    Value = endAt,
                    SqlDbType = SqlDbType.Int
                };

                return m_context.Query<UserListItemModel>()
                    .FromSql("[dbo].[BID_GetUsers] @start, @end", startPaginationFrom, endPaginationAt);
            }
            catch (Exception ex)
            {
                throw new WebApiException(HttpStatusCode.BadRequest, UserErrorMessages.CouldNotFetchUserList, ex);
            }
        }

        /// <summary>
        /// Returns total count of all active and inactive users for admin panel!
        /// </summary>
        /// <returns></returns>
        public IEnumerable<User> TotalUserCount()
        {
            return m_context.Users;
        }
    }
}
