using Bidding.Models.ViewModels.Bidding.Admin.Users.List;
using Bidding.Models.ViewModels.Bidding.Users.Add;
using Bidding.Models.ViewModels.Bidding.Users.Details;
using Bidding.Models.ViewModels.Bidding.Users.Shared;
using Bidding.Repositories.Users;
using Bidding.Services.Shared.Permissions;
using Bidding.Shared.Constants;
using Bidding.Shared.ErrorHandling.Errors;
using Bidding.Shared.Exceptions;
using Bidding.Shared.Pagination;
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
        private readonly PermissionService m_permissionService;

        public UsersService(UsersRepository userRepository, PermissionService permissionService)
        {
            m_permissionService = permissionService ?? throw new ArgumentNullException(nameof(permissionService));
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
            if (request.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.BadRequest, UserErrorMessages.MissingUsersInformation); }
            if (request.LoginEmail.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.BadRequest, UserErrorMessages.MissingUsersInformation); }
            if (request.UniqueIdentifier.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.BadRequest, UserErrorMessages.MissingUsersInformation); }

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

        public UserListResponseModel ListWithSearch(UserListRequestModel request)
        {
            ValidateUserListWithSearch(request);

            (int startFrom, int endAt) = Pagination.GetStartAndEnd(request);

            UserListResponseModel auctionsResponse = new UserListResponseModel()
            {
                Users = m_userRepository.ListWithSearch(request, startFrom, endAt).ToList(),
                ItemCount = m_userRepository.TotalUserCount().Count()
            };

            Pagination.PaginateResponse(ref auctionsResponse, TableItem.DEFAULT_SIZE, request.CurrentPage);

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

            m_permissionService.IsLoggedInUserActive();
        }
    }
}
