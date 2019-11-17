using Bidding.Models.Contexts;
using Bidding.Models.DatabaseModels.Shared;
using Bidding.Models.ViewModels.Admin.Users.List;
using Bidding.Models.ViewModels.Users.Details;
using Bidding.Models.ViewModels.Users.Shared;
using Bidding.Shared.Constants;
using Bidding.Shared.ErrorHandling.Errors;
using Bidding.Shared.Exceptions;
using Bidding.Shared.Utility.Validation.Comparers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Threading.Tasks;

namespace Bidding.Repositories.Users
{
    public class UsersRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly BiddingContext _bidContext;

        public UsersRepository(IServiceProvider services)
        {
            _userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            _bidContext = services.GetRequiredService<BiddingContext>();
        }

        /// <summary>
        /// Can be used to check if this specific user already exists
        /// </summary>
        /// <param name="email">Users e-mail</param>
        /// <returns></returns>
        public bool UserExists(string email)
        {
            return true;

            // @Permissions: WIP!
            // m_context.Users.Any(usr => usr.LoginEmail == email && usr.Deleted == false);
        }

        public async Task<ApplicationUser> HandleUserLoginAsync(ApplicationUser user)
        {
            ApplicationUser userDetails = await _userManager.FindByEmailAsync(user.Email).ConfigureAwait(false);

            if (userDetails.IsSpecified())
            {
                return await HandleExistingUserAsync(user, userDetails).ConfigureAwait(false);
            }

            return await HandleNewUserAsync(user).ConfigureAwait(false);
        }

        public IEnumerable<UserDetailsModel> UserDetails(int userId)
        {
            yield return new UserDetailsModel();

            // @Permissions: WIP!
            //return (from usr in m_context.Users
            //        where usr.UserId == userId && usr.Deleted == false
            //        select new UserDetailsModel()
            //        {
            //            UserId = usr.UserId,
            //            UserFirstName = usr.FirstName,
            //            UserLastName = usr.LastName,
            //            UserEmail = usr.LoginEmail
            //        });
        }

        public IEnumerable<UserProfileModel> UserDetails(string email)
        {
            yield return new UserProfileModel();
            // @Permissions: WIP!
            //return from usr in m_context.Users
            //       join rol in m_context.Roles on usr.RoleId equals rol.RoleId
            //       where usr.LoginEmail == email && usr.Deleted == false
            //       select new UserProfileModel()
            //       {
            //           UserId = usr.UserId,
            //           UserContactEmail = usr.ContactEmail,
            //           UserRole = rol.Name
            //       };
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

                return _bidContext.Query<UserListItemModel>()
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
        //public IEnumerable<User> TotalUserCount()
        //{
        //    yield return new User();
        //    // @Permissions: WIP!
        //    // return m_context.Users;
        //}

        private async Task<ApplicationUser> HandleExistingUserAsync(ApplicationUser user, ApplicationUser userDetails)
        {
            if (user.EmailConfirmed != userDetails.EmailConfirmed)
            {
                userDetails.EmailConfirmed = user.EmailConfirmed;

                await _userManager.UpdateAsync(userDetails).ConfigureAwait(false);
            }

            return userDetails;
        }

        private async Task<ApplicationUser> HandleNewUserAsync(ApplicationUser user)
        {
            IdentityResult result = await _userManager.CreateAsync(user).ConfigureAwait(false);

            if (result.Succeeded)
            {
                await AddBasicRoleToNewUser(user).ConfigureAwait(false);
            }

            if (!result.Succeeded)
            {
                throw new WebApiException(HttpStatusCode.InternalServerError, UserErrorMessages.CanNotSignIn);
            }

            return user;
        }

        private async Task AddBasicRoleToNewUser(ApplicationUser user)
        {
            IdentityResult result = await _userManager.AddToRoleAsync(user, ApplicationUserRoles.BasicUser).ConfigureAwait(false);

            if (!result.Succeeded)
            {
                throw new WebApiException(HttpStatusCode.InternalServerError, UserErrorMessages.CanNotSignIn);
            }
        }
    }
}
