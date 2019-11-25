using Bidding.Models.DatabaseModels.Shared;
using Bidding.Models.ViewModels.Admin.Users.List;
using Bidding.Models.ViewModels.Users.Details;
using Bidding.Models.ViewModels.Users.Shared;
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
            m_permissionService = permissionService ?? throw new ArgumentNullException(nameof(permissionService));
            m_userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        /// <summary>
        /// Checks if user already exists, if not creates a new user.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<ApplicationUser> HandleUserLoginAsync(ApplicationUser user)
        {
            if (user.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.BadRequest, UserErrorMessages.MissingUsersInformation); }
            if (user.Email.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.BadRequest, UserErrorMessages.MissingUsersInformation); }
            if (user.IdentityId.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.Unauthorized, UserErrorMessages.CanNotSignIn); }
            if (user.EmailConfirmed.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.Unauthorized, UserErrorMessages.CanNotSignIn); }
            if (user.UserName.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.Unauthorized, UserErrorMessages.CanNotSignIn); }

            return await m_userRepository.HandleUserLoginAsync(user).ConfigureAwait(false);
        }

        public bool UserExists(string email)
        {
            if (email.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.BadRequest, UserErrorMessages.EmailNotSpecified); }

            return m_userRepository.UserExists(email);
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

        public UserListResponseModel ListWithSearch(UserListRequestModel request)
        {
            ValidateUserListWithSearch(request);

            (int startFrom, int endAt) = Pagination.GetStartAndEnd(request);

            UserListResponseModel auctionsResponse = new UserListResponseModel()
            {
                Users = m_userRepository.ListWithSearch(request, startFrom, endAt).ToList(),
                ItemCount = 1// m_userRepository.TotalUserCount().Count()
            };

            Pagination.PaginateResponse(ref auctionsResponse, TableItem.DefaultSize, request.CurrentPage);

            return auctionsResponse;
        }

        private void ValidateUserListWithSearch(UserListRequestModel request)
        {
            if (request.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.BadRequest, UserErrorMessages.MissingUsersInformation); }
            if (request.SortByColumn.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.BadRequest, UserErrorMessages.MissingUsersInformation); }
            if (request.SortingDirection.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.BadRequest, UserErrorMessages.MissingUsersInformation); }
            if (request.OffsetStart < 0) { throw new WebApiException(HttpStatusCode.BadRequest, UserErrorMessages.MissingUsersInformation); }
            if (request.OffsetEnd.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.BadRequest, UserErrorMessages.MissingUsersInformation); }
            if (request.CurrentPage < 0) { throw new WebApiException(HttpStatusCode.BadRequest, UserErrorMessages.MissingUsersInformation); }
            if (request.SortingDirection != "asc" && request.SortingDirection != "desc") { throw new WebApiException(HttpStatusCode.BadRequest, UserErrorMessages.MissingUsersInformation); }

            // todo: kke: implement this logic!
            //List<string> allowedSortByColumns = new List<string> { "AuctionName", "AuctionStartingPrice", "AuctionStartDate", "AuctionEndDate" };
            //if (allowedSortByColumns.Contains(request.SortByColumn) == false) { throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.MissingAuctionsInformation); }
        }
    }
}
