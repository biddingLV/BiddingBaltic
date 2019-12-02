using Bidding.Models.Contexts;
using Bidding.Models.DatabaseModels.Shared;
using Bidding.Models.ViewModels.Admin.Users.List;
using Bidding.Models.ViewModels.Users.Details;
using Bidding.Models.ViewModels.Users.Edit;
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
        private readonly UserManager<ApplicationUser> m_userManager;
        private readonly BiddingContext m_context;

        public UsersRepository(IServiceProvider services)
        {
            m_userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            m_context = services.GetRequiredService<BiddingContext>();
        }

        /// <summary>
        /// Check if user already exists in the system
        /// </summary>
        /// <param name="email">Users e-mail</param>
        /// <returns></returns>
        public async Task<bool> UserExists(int userId)
        {
            return await m_userManager.Users.AnyAsync(u => u.Id == userId).ConfigureAwait(true);
        }

        public async Task<ApplicationUser> HandleUserLoginAsync(ApplicationUser user)
        {
            ApplicationUser userDetails = await m_userManager.FindByEmailAsync(user.Email).ConfigureAwait(true);

            if (userDetails.IsSpecified())
            {
                return await HandleExistingUserAsync(user, userDetails).ConfigureAwait(false);
            }

            return await HandleNewUserAsync(user).ConfigureAwait(false);
        }

        public async Task<UserDetailsResponseModel> UserDetails(int userId)
        {
            ApplicationUser user = await m_userManager.Users.FirstOrDefaultAsync(u => u.Id == userId).ConfigureAwait(true);

            return new UserDetailsResponseModel()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.PhoneNumber
            };
        }

        public async Task<bool> Edit(UserEditRequestModel request)
        {
            ApplicationUser userForUpdate = await m_userManager.Users.FirstOrDefaultAsync(u => u.Id == request.UserId).ConfigureAwait(true);

            userForUpdate.FirstName = request.FirstName;
            userForUpdate.LastName = request.LastName;
            userForUpdate.PhoneNumber = request.Phone;

            IdentityResult result = await m_userManager.UpdateAsync(userForUpdate).ConfigureAwait(false);

            if (result.Succeeded)
            {
                return true;
            }

            if (!result.Succeeded)
            {
                throw new WebApiException(HttpStatusCode.InternalServerError, UserErrorMessage.CouldNotUpdateUser);
            }

            return true;
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
                throw new WebApiException(HttpStatusCode.BadRequest, UserErrorMessage.CouldNotFetchUserList, ex);
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

                await m_userManager.UpdateAsync(userDetails).ConfigureAwait(false);
            }

            return userDetails;
        }

        private async Task<ApplicationUser> HandleNewUserAsync(ApplicationUser user)
        {
            IdentityResult result = await m_userManager.CreateAsync(user).ConfigureAwait(false);

            if (result.Succeeded)
            {
                await AddBasicRoleToNewUser(user).ConfigureAwait(false);
            }

            if (!result.Succeeded)
            {
                throw new WebApiException(HttpStatusCode.InternalServerError, UserErrorMessage.CanNotSignIn);
            }

            return user;
        }

        private async Task AddBasicRoleToNewUser(ApplicationUser user)
        {
            IdentityResult result = await m_userManager.AddToRoleAsync(user, ApplicationUserRoles.BasicUser).ConfigureAwait(false);

            if (!result.Succeeded)
            {
                throw new WebApiException(HttpStatusCode.InternalServerError, UserErrorMessage.CanNotSignIn);
            }
        }
    }
}
