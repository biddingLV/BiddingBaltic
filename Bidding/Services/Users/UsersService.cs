using Bidding.Models.DatabaseModels.Shared;
using Bidding.Models.ViewModels.Admin.Users.List;
using Bidding.Models.ViewModels.Users.Details;
using Bidding.Models.ViewModels.Users.Edit;
using Bidding.Repositories.Users;
using Bidding.Services.Shared.Permissions;
using Bidding.Shared.Constants;
using Bidding.Shared.ErrorHandling.Errors;
using Bidding.Shared.Exceptions;
using Bidding.Shared.Pagination;
using Bidding.Shared.Utility.Validation.Comparers;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Bidding.Services.Users
{
    public class UsersService
    {
        private readonly UsersRepository m_userRepository;
        private readonly PermissionService m_permissionService;

        public UsersService(UsersRepository userRepository, PermissionService permissionService)
        {
            m_userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            m_permissionService = permissionService ?? throw new ArgumentNullException(nameof(permissionService));
        }

        /// <summary>
        /// Checks if user already exists, if not creates a new user.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<ApplicationUser> HandleUserLoginAsync(ApplicationUser user)
        {
            if (user.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.BadRequest, UserErrorMessage.MissingUsersInformation); }
            if (user.Email.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.BadRequest, UserErrorMessage.MissingUsersInformation); }
            if (user.IdentityId.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.Unauthorized, UserErrorMessage.CanNotSignIn); }
            if (user.EmailConfirmed.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.Unauthorized, UserErrorMessage.CanNotSignIn); }
            if (user.UserName.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.Unauthorized, UserErrorMessage.CanNotSignIn); }

            return await m_userRepository.HandleUserLoginAsync(user).ConfigureAwait(true);
        }

        public async Task<UserBasicDetailsResponseModel> UserDetails(int userId)
        {
            if (userId.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.BadRequest, UserErrorMessage.UserNotValid); }
            if (await UserExists(userId).ConfigureAwait(true) == false)
            {
                throw new WebApiException(HttpStatusCode.BadRequest, UserErrorMessage.UserNotActive);
            }

            return await m_userRepository.UserDetails(userId).ConfigureAwait(true);
        }

        public async Task<UserBasicDetailsResponseModel> EditBasicDetails(int userId)
        {
            // note: kke: atm it is the same as UserDetails method!
            return await UserDetails(userId).ConfigureAwait(true);
        }

        public async Task<UserAdvancedDetailsResponseModel> EditAdvancedDetails(int userId)
        {
            if (userId.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.BadRequest, UserErrorMessage.UserNotValid); }
            if (await UserExists(userId).ConfigureAwait(true) == false)
            {
                throw new WebApiException(HttpStatusCode.BadRequest, UserErrorMessage.UserNotActive);
            }

            return await m_userRepository.EditAdvancedDetails(userId).ConfigureAwait(true);
        }

        public async Task<UserListResponseModel> ListWithSearchAsync(UserListRequestModel request)
        {
            ValidateUserListWithSearch(request);

            (int startFrom, int endAt) = Pagination.GetOffsetAndSize(request);

            UserListResponseModel response = new UserListResponseModel()
            {
                Users = m_userRepository.ListWithSearch(startFrom, endAt).ToList(),
                ItemCount = await m_userRepository.GetTotalUserCountAsync().ConfigureAwait(true)
            };

            Pagination.PaginateResponse(ref response, TableItem.DefaultSize, request.CurrentPage);

            return response;
        }

        public async Task<bool> EditBasicAsync(EditBasicDetailsRequestModel request)
        {
            if (request.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.BadRequest, UserErrorMessage.MissingUsersInformation); }
            if (request.FirstName.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.BadRequest, UserErrorMessage.MissingUsersInformation); }
            if (request.LastName.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.BadRequest, UserErrorMessage.MissingUsersInformation); }

            int loggedInUserId = m_permissionService.GetAndValidateUserId();

            return await m_userRepository.EditBasicAsync(request, loggedInUserId).ConfigureAwait(true);
        }

        public async Task<bool> EditAdvancedAsync(EditAdvancedDetailsRequestModel request)
        {
            if (request.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.BadRequest, UserErrorMessage.MissingUsersInformation); }
            if (request.UserId.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.BadRequest, UserErrorMessage.MissingUsersInformation); }
            if (request.FirstName.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.BadRequest, UserErrorMessage.MissingUsersInformation); }
            if (request.LastName.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.BadRequest, UserErrorMessage.MissingUsersInformation); }
            if (request.RoleId.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.BadRequest, UserErrorMessage.MissingUsersInformation); }
            if (await UserExists(request.UserId).ConfigureAwait(true) == false)
            {
                throw new WebApiException(HttpStatusCode.BadRequest, UserErrorMessage.UserNotActive);
            }

            if (await RoleExists(request.RoleId).ConfigureAwait(true) == false)
            {
                throw new WebApiException(HttpStatusCode.BadRequest, UserErrorMessage.IncorrectUserRole);
            }

            // todo: kke: validate if date is valid, when specified!

            return await m_userRepository.EditAdvancedAsync(request).ConfigureAwait(true);
        }

        private async Task<bool> UserExists(int userId)
        {
            return await m_userRepository.UserExists(userId).ConfigureAwait(true);
        }

        private async Task<bool> RoleExists(int roleId)
        {
            return await m_userRepository.RoleExists(roleId).ConfigureAwait(true);
        }

        private void ValidateUserListWithSearch(UserListRequestModel request)
        {
            if (request.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.BadRequest, UserErrorMessage.MissingUsersInformation); }
            if (request.SortByColumn.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.BadRequest, UserErrorMessage.MissingUsersInformation); }
            if (request.SortingDirection.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.BadRequest, UserErrorMessage.MissingUsersInformation); }
            if (request.OffsetStart < 0) { throw new WebApiException(HttpStatusCode.BadRequest, UserErrorMessage.MissingUsersInformation); }
            if (request.OffsetEnd.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.BadRequest, UserErrorMessage.MissingUsersInformation); }
            if (request.CurrentPage < 0) { throw new WebApiException(HttpStatusCode.BadRequest, UserErrorMessage.MissingUsersInformation); }
            if (request.SortingDirection != "asc" && request.SortingDirection != "desc") { throw new WebApiException(HttpStatusCode.BadRequest, UserErrorMessage.MissingUsersInformation); }

            // todo: kke: implement this logic!
            //List<string> allowedSortByColumns = new List<string> { "AuctionName", "AuctionStartingPrice", "AuctionStartDate", "AuctionEndDate" };
            //if (allowedSortByColumns.Contains(request.SortByColumn) == false) { throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.MissingAuctionsInformation); }
        }
    }
}
