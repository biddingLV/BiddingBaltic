using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Bidding.Models.ViewModels.Bidding.Auctions;
using Bidding.Models.ViewModels.Bidding.Auctions.Add;
using Bidding.Models.ViewModels.Bidding.Auctions.Details;
using Bidding.Models.ViewModels.Bidding.Filters;
using Bidding.Services.Shared.Permissions;
using Bidding.Shared.ErrorHandling.Errors;
using Bidding.Shared.ErrorHandling.Validators;
using Bidding.Shared.Exceptions;
using Bidding.Shared.Pagination;
using Bidding.Shared.Utility;
using BiddingAPI.Models.DatabaseModels;
using BiddingAPI.Models.DatabaseModels.Bidding;
using BiddingAPI.Models.ViewModels.Bidding.Auctions;
using BiddingAPI.Repositories.Auctions;
using FluentValidation;
using FluentValidation.Results;

namespace BiddingAPI.Services.Auctions
{
    public class AuctionsService
    {
        private readonly PermissionService m_permissionService;
        private readonly AuctionsRepository m_auctionsRepository;

        /// <summary>
        /// Default page size for auction list
        /// </summary>
        private readonly int defaultPageSize = 15;

        public AuctionsService(AuctionsRepository auctionRepository, PermissionService permissionService)
        {
            m_permissionService = permissionService ?? throw new ArgumentNullException(nameof(permissionService));
            m_auctionsRepository = auctionRepository ?? throw new ArgumentNullException(nameof(auctionRepository));
        }

        public AuctionListResponseModel ListWithSearch(AuctionListRequestModel request)
        {
            ValidateAuctionListWithSearch(request);

            (int startFrom, int endAt) = Pagination.GetStartAndEnd(request);

            AuctionListResponseModel auctionsResponse = new AuctionListResponseModel()
            {
                Auctions = m_auctionsRepository.ListWithSearch(request, startFrom, endAt, request.TopCategoryIds, request.TypeIds).ToList(),
                ItemCount = m_auctionsRepository.TotalAuctionCount().Count()
            };

            Pagination.PaginateResponse(ref auctionsResponse, defaultPageSize, request.CurrentPage);

            return auctionsResponse;
        }

        /// <summary>
        /// Loads all filters, top category filter and sub-category filter
        /// </summary>
        /// <returns></returns>
        public AuctionFilterModel Filters()
        {
            m_permissionService.IsLoggedInUserActive();

            return new AuctionFilterModel()
            {
                TopCategories = m_auctionsRepository.LoadTopCategories().ToList(),
                SubCategories = m_auctionsRepository.LoadSubCategories().ToList()
            };
        }

        public AuctionCreatorModel Creators()
        {
            m_permissionService.IsLoggedInUserActive();

            return new AuctionCreatorModel()
            {
                Creators = m_auctionsRepository.Creators().ToList()
            };
        }

        public AuctionFormatModel Formats()
        {
            m_permissionService.IsLoggedInUserActive();

            return new AuctionFormatModel()
            {
                Formats = m_auctionsRepository.Formats().ToList()
            };
        }

        public AuctionStatusModel Statuses()
        {
            m_permissionService.IsLoggedInUserActive();

            return new AuctionStatusModel()
            {
                Statuses = m_auctionsRepository.Statuses().ToList()
            };
        }

        public AuctionDetailsResponseModel Details(AuctionDetailsRequestModel request)
        {
            if (request.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.MissingAuctionsInformation); }
            if (request.AuctionId.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.IncorrectAuction); }

            m_permissionService.IsLoggedInUserActive();

            return m_auctionsRepository.Details(request).FirstOrDefault();
        }

        public bool Create(AuctionAddRequestModel request)
        {
            ValidateAuctionCreate(request);

            int? loggedInUserId = m_permissionService.GetUserIdFromClaimsPrincipal();

            return m_auctionsRepository.Create(request, loggedInUserId.Value);
        }

        public bool Update(AuctionEditRequestModel request)
        {
            ValidateAuctionUpdate(request);

            int? loggedInUserId = m_permissionService.GetUserIdFromClaimsPrincipal();

            return m_auctionsRepository.Update(request, loggedInUserId.Value);
        }

        public bool Delete(AuctionDeleteRequestModel request)
        {
            ValidateAuctionDelete(request);

            int? loggedInUserId = m_permissionService.GetUserIdFromClaimsPrincipal();

            return m_auctionsRepository.Delete(request, loggedInUserId.Value);
        }

        /// <summary>
        /// Validate auction list permissions and input values
        /// </summary>
        /// <param name="request"></param>
        private void ValidateAuctionListWithSearch(AuctionListRequestModel request)
        {
            if (request.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.MissingAuctionsInformation); }
            if (request.SortByColumn.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.MissingAuctionsInformation); }
            if (request.SortingDirection.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.MissingAuctionsInformation); }
            if (request.OffsetStart < 0) { throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.MissingAuctionsInformation); }
            if (request.OffsetEnd.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.MissingAuctionsInformation); }
            if (request.CurrentPage < 0) { throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.MissingAuctionsInformation); }
            if (request.SortingDirection != "asc" && request.SortingDirection != "desc") { throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.MissingAuctionsInformation); }

            List<string> allowedSortByColumns = new List<string> { "AuctionName", "AuctionStartingPrice", "AuctionStartDate", "AuctionEndDate" };
            if (allowedSortByColumns.Contains(request.SortByColumn) == false) { throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.MissingAuctionsInformation); }

            m_permissionService.IsLoggedInUserActive();
        }

        /// <summary>
        /// Validate auction add permissions
        /// </summary>
        /// <param name="request"></param>
        private void ValidateAuctionCreate(AuctionAddRequestModel request)
        {
            if (request.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.MissingAuctionsInformation); }
            if (request.AuctionName.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.MissingAuctionsInformation); }
            //if (request.AuctionTopCategoryIds.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.MissingAuctionsInformation); }
            //if (request.AuctionSubCategoryIds.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.MissingAuctionsInformation); }
            //if (request.AuctionStartingPrice.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.MissingAuctionsInformation); }
            //if (request.AuctionStartDate.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.MissingAuctionsInformation); }
            //if (request.AuctionApplyTillDate.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.MissingAuctionsInformation); }
            //if (request.AuctionEndDate.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.MissingAuctionsInformation); }
            //if (request.AuctionCreatorId.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.MissingAuctionsInformation); }
            //if (request.AuctionFormatId.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.MissingAuctionsInformation); }

            m_permissionService.IsLoggedInUserActive();

            // todo: kke: save all dates to be UTC Format
            // todo: kke: validate all required fields to be specified + max lenghts / min leghts and regular expressions

            // setup auction object for the validation
            //Auction auction = new Auction()
            //{
            //    Name = request.AuctionName,
            //    StartingPrice = request.AuctionStartingPrice,
            //    StartDate = request.AuctionStartDate
            //};

            //ValidateAuction(auction);
        }

        private void ValidateAuction(Auction auction)
        {
            AuctionValidator validator = new AuctionValidator();
            ValidationResult results = validator.Validate(auction);

            bool success = results.IsValid;
            IList<ValidationFailure> failures = results.Errors;

            validator.ValidateAndThrow(auction);

            if (results.IsValid == false)
            {
                foreach (ValidationFailure failure in results.Errors)
                {
                    throw new WebApiException(HttpStatusCode.BadRequest, failure.ErrorMessage);
                }
            }
        }

        /// <summary>
        /// Validate auction update permissions
        /// </summary>
        /// <param name="request"></param>
        private void ValidateAuctionUpdate(AuctionEditRequestModel request)
        {
            if (request.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.MissingAuctionsInformation); }
            if (request.AuctionId.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.MissingAuctionsInformation); }

            // todo: kke: add missing validation checks!

            m_permissionService.IsLoggedInUserActive();
        }

        private void ValidateAuctionDelete(AuctionDeleteRequestModel request)
        {
            if (request.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.MissingAuctionsInformation); }
            if (request.AuctionIds.IsNotSpecified()) throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.MissingAuctionsInformation);
            if (request.AuctionIds.Any(s => s.IsNotSpecified())) throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.MissingAuctionsInformation);

            m_permissionService.IsLoggedInUserActive();
        }
    }
}
