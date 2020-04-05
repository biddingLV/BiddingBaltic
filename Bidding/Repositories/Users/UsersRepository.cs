using Bidding.Models.Contexts;
using Bidding.Models.DatabaseModels.Shared;
using Bidding.Models.ViewModels.Admin.Users.List;
using Bidding.Models.ViewModels.Users.Details;
using Bidding.Models.ViewModels.Users.Edit;
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
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Bidding.Repositories.Users
{
    public class UsersRepository
    {
        private readonly UserManager<ApplicationUser> m_userManager;
        private readonly RoleManager<ApplicationRole> m_roleManager;
        private readonly BiddingContext m_context;

        public UsersRepository(IServiceProvider services)
        {
            m_userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            m_roleManager = services.GetRequiredService<RoleManager<ApplicationRole>>();
            m_context = services.GetRequiredService<BiddingContext>();
        }

        /// <summary>
        /// Check if user already exists in the system
        /// </summary>
        /// <param name="email">Users e-mail</param>
        /// <returns></returns>
        public async Task<bool> UserExists(int userId)
        {
            return await m_userManager.Users.AnyAsync(usr => usr.Id == userId).ConfigureAwait(true);
        }

        /// <summary>
        /// Check if role exists in the system
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task<bool> RoleExists(int roleId)
        {
            return await m_roleManager.Roles.AnyAsync(rol => rol.Id == roleId).ConfigureAwait(true);
        }

        public async Task<ApplicationUser> HandleUserLoginAsync(ApplicationUser user)
        {
            ApplicationUser userDetails = await m_userManager.FindByEmailAsync(user.Email).ConfigureAwait(true);

            if (userDetails.IsSpecified())
            {
                return await HandleExistingUserAsync(user, userDetails).ConfigureAwait(true);
            }

            return await HandleNewUserAsync(user).ConfigureAwait(true);
        }

        public async Task<UserBasicDetailsResponseModel> UserDetails(int userId)
        {
            ApplicationUser user = await m_userManager.Users.FirstOrDefaultAsync(u => u.Id == userId).ConfigureAwait(true);

            return new UserBasicDetailsResponseModel()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.PhoneNumber
            };
        }

        public async Task<UserAdvancedDetailsResponseModel> EditAdvancedDetails(int userId)
        {
            List<RoleItemModel> roles = await ApplicationRoles();

            return await m_userManager.Users
                .Where(usr => usr.Id == userId)
                .Include(usr => usr.UserRoles)
                .ThenInclude(usr => usr.Role)
                .Select(usr => new UserAdvancedDetailsResponseModel()
                {
                    FirstName = usr.FirstName,
                    LastName = usr.LastName,
                    Email = usr.Email,
                    Phone = usr.PhoneNumber,
                    RoleId = usr.UserRoles.Where(urol => urol.UserId == userId).First().RoleId,
                    Roles = roles
                }).FirstOrDefaultAsync().ConfigureAwait(true);
        }

        public async Task<bool> EditBasicAsync(EditBasicDetailsRequestModel request, int loggedInUserId)
        {
            ApplicationUser userForUpdate = await m_userManager.Users.FirstOrDefaultAsync(u => u.Id == loggedInUserId).ConfigureAwait(true);

            userForUpdate.FirstName = request.FirstName;
            userForUpdate.LastName = request.LastName;
            userForUpdate.PhoneNumber = request.Phone;

            IdentityResult result = await m_userManager.UpdateAsync(userForUpdate).ConfigureAwait(true);

            return result.Succeeded ? true : throw new WebApiException(HttpStatusCode.InternalServerError, UserErrorMessage.CouldNotUpdateUser);
        }

        public async Task<bool> EditAdvancedAsync(EditAdvancedDetailsRequestModel request)
        {
            ApplicationUser userForUpdate = await m_userManager.Users.FirstOrDefaultAsync(u => u.Id == request.UserId).ConfigureAwait(true);

            userForUpdate.FirstName = request.FirstName;
            userForUpdate.LastName = request.LastName;
            userForUpdate.PhoneNumber = request.Phone;

            await ChangeUserRole(request, userForUpdate);

            IdentityResult result = await m_userManager.UpdateAsync(userForUpdate).ConfigureAwait(true);

            return result.Succeeded ? true : throw new WebApiException(HttpStatusCode.InternalServerError, UserErrorMessage.CouldNotUpdateUser);
        }

        public IEnumerable<UserListItemModel> ListWithSearch(int startFrom, int endAt, int usersId)
        {
            try
            {
                SqlParameter startPaginationFrom = SetupSqlParameter("start", startFrom);
                SqlParameter endPaginationAt = SetupSqlParameter("end", endAt);
                SqlParameter loggedInUserId = SetupSqlParameter("userId", usersId);

                return m_context.Query<UserListItemModel>()
                    .FromSql("[dbo].[BID_GetUsers] @start, @end, @userId", startPaginationFrom, endPaginationAt, loggedInUserId);
            }
            catch (Exception ex)
            {
                throw new WebApiException(HttpStatusCode.BadRequest, UserErrorMessage.CouldNotFetchUserList, ex);
            }
        }

        public async Task<int> GetTotalUserCountAsync(int loggedInUserId)
        {
            return await m_userManager.Users.Where(usr => usr.Id != loggedInUserId).CountAsync().ConfigureAwait(true);
        }

        private static SqlParameter SetupSqlParameter(string parameterName, int parameterValue)
        {
            return new SqlParameter
            {
                ParameterName = parameterName,
                Direction = ParameterDirection.Input,
                Value = parameterValue,
                SqlDbType = SqlDbType.Int
            };
        }

        private async Task ChangeUserRole(EditAdvancedDetailsRequestModel request, ApplicationUser userForUpdate)
        {
            var userRoles = await m_userManager.GetRolesAsync(userForUpdate);
            ApplicationRole newUserRole = await m_roleManager.FindByIdAsync(request.RoleId.ToString()).ConfigureAwait(true);

            await m_userManager.RemoveFromRoleAsync(userForUpdate, userRoles.FirstOrDefault()).ConfigureAwait(true); ;
            await m_userManager.AddToRoleAsync(userForUpdate, newUserRole.Name).ConfigureAwait(true);
        }

        private async Task<ApplicationUser> HandleExistingUserAsync(ApplicationUser user, ApplicationUser userDetails)
        {
            if (user.EmailConfirmed != userDetails.EmailConfirmed)
            {
                userDetails.EmailConfirmed = user.EmailConfirmed;

                await m_userManager.UpdateAsync(userDetails).ConfigureAwait(true);
            }

            return userDetails;
        }

        private async Task<ApplicationUser> HandleNewUserAsync(ApplicationUser user)
        {
            IdentityResult result = await m_userManager.CreateAsync(user).ConfigureAwait(true);

            if (result.Succeeded)
            {
                await AddBasicRoleToNewUser(user).ConfigureAwait(true);
            }

            if (!result.Succeeded)
            {
                throw new WebApiException(HttpStatusCode.InternalServerError, UserErrorMessage.CanNotSignIn);
            }

            return user;
        }

        private async Task AddBasicRoleToNewUser(ApplicationUser user)
        {
            IdentityResult result = await m_userManager.AddToRoleAsync(user, ApplicationUserRoles.BasicUser).ConfigureAwait(true);

            if (!result.Succeeded)
            {
                throw new WebApiException(HttpStatusCode.InternalServerError, UserErrorMessage.CanNotSignIn);
            }
        }

        private async Task<List<RoleItemModel>> ApplicationRoles()
        {
            return await m_roleManager.Roles
                .Where(rol => rol.Name != ApplicationUserRoles.SuperAdministrator)
                .Select(rol => new RoleItemModel() { RoleId = rol.Id, RoleName = rol.Name })
                .ToListAsync().ConfigureAwait(true);
        }
    }
}
