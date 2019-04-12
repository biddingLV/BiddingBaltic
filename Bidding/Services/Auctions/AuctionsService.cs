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

        public AuctionsService(AuctionsRepository auctionRepository, PermissionService permissionService)
        {
            m_permissionService = permissionService ?? throw new ArgumentNullException(nameof(permissionService));
            m_auctionsRepository = auctionRepository ?? throw new ArgumentNullException(nameof(auctionRepository));
        }

        public AuctionListResponseModel ListWithSearch(AuctionListRequestModel request)
        {
            ValidateAuctionListWithSearch(request);

            List<int> categoryIds = new List<int>();
            List<int> typeIds = new List<int>();

            // validate top category ids only if specified in the request
            if (request.TopCategoryIds.IsNotSpecified() == false)
            {
                categoryIds = ValidateAndConvertIds(request.TopCategoryIds);
            }

            // validate top category ids only if specified in the request
            if (request.TypeIds.IsNotSpecified() == false)
            {
                typeIds = ValidateAndConvertIds(request.TypeIds);
            }

            // pagination assignments
            int startFromThisItem = request.OffsetStart;
            int takeUntilThisItem = request.OffsetEnd;
            int startFrom = Math.Max(startFromThisItem - 1, 0) * takeUntilThisItem;
            int endAt = startFrom + takeUntilThisItem;

            // todo: kke: remove after testing done!
            string dateInString = "2018-01-01";
            DateTime startDate = DateTime.Parse(dateInString);
            DateTime expiryDate = startDate.AddYears(5);
            // todo: kke: remove after testing done!

            AuctionListResponseModel auctionsResponse = new AuctionListResponseModel()
            {
                Auctions = m_auctionsRepository.ListWithSearch(request, startFrom, endAt, categoryIds, typeIds).ToList(),
                ItemCount = m_auctionsRepository.TotalAuctionCount(startDate, expiryDate).Count()
            };

            int totalPages = 0;
            //check if total data < pagesize
            if (auctionsResponse.ItemCount > 0)
            {
                if (auctionsResponse.ItemCount > takeUntilThisItem)
                {
                    totalPages = auctionsResponse.ItemCount / takeUntilThisItem;
                    if (auctionsResponse.ItemCount % takeUntilThisItem > 0)
                    {
                        totalPages++;
                    }
                }
                else
                {
                    totalPages = 1;
                }
            }

            if (totalPages < startFromThisItem)
            {
                startFromThisItem = totalPages;
            }

            if (auctionsResponse.ItemCount == 0)
            {
                return auctionsResponse;
            }

            auctionsResponse.CurrentPage = request.CurrentPage;

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

        public AuctionDetailsResponseModel Details(AuctionDetailsRequestModel request)
        {
            if (request.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.MissingAuctionsInformation); }
            if (request.AuctionId.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.IncorrectAuction); }

            m_permissionService.IsLoggedInUserActive();

            return m_auctionsRepository.Details(request).FirstOrDefault();
        }

        public bool Update(AuctionEditRequestModel request)
        {
            ValidateAuctionUpdate(request);

            return m_auctionsRepository.Update(request);
        }

        public bool Create(AuctionAddRequestModel request)
        {
            ValidateAuctionCreate(request);

            return m_auctionsRepository.Create(request);
        }

        public bool Delete(AuctionDeleteRequestModel request)
        {
            ValidateAuctionDelete(request);

            return m_auctionsRepository.Delete(request);
        }

        /// <summary>
        /// Validate auction list permissions and input values
        /// </summary>
        /// <param name="request"></param>
        private void ValidateAuctionListWithSearch(AuctionListRequestModel request)
        {
            if (request.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.MissingAuctionsInformation); }
            // if (request.AuctionStartDate.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.MissingAuctionsInformation); }
            // if (request.AuctionEndDate.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.MissingAuctionsInformation); }
            if (request.SortByColumn.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.MissingAuctionsInformation); }
            if (request.SortingDirection.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.MissingAuctionsInformation); }
            if (request.OffsetStart < 0) { throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.MissingAuctionsInformation); }
            if (request.OffsetEnd.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.MissingAuctionsInformation); }
            if (request.CurrentPage < 0) { throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.MissingAuctionsInformation); }

            m_permissionService.IsLoggedInUserActive();

            // sorting direction can only be ascending || descending
            //if (request.SortingDirection != "asc" || request.SortingDirection != "desc")
            //{
            //    throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.MissingAuctionsInformation);
            //}

            // todo: kke: maybe use here enum?
            //if (request.SortByColumn != "AuctionName" || request.SortByColumn != "AuctionStartingPrice" || request.SortByColumn != "AuctionStartDate" || request.SortByColumn != "AuctionEndDate")
            //{
            //    throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.MissingAuctionsInformation);
            //}

            // todo: kke: validate start date and end date and convert string to date time
        }

        private List<int> ValidateAndConvertIds(string ids)
        {
            List<int> listWithIds = new List<int>();

            foreach (string orgId in ids.Split(','))
            {
                // convert from string to int
                if (int.TryParse(orgId, out int convertedId))
                {
                    // add id to the array
                    listWithIds.Add(convertedId);
                }
                else
                {
                    // Something is wrong with specified top category id
                    throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.TopCategoryIdsNotCorrect);
                }
            }

            return listWithIds;
        }

        /// <summary>
        /// Validate auction add permissions
        /// </summary>
        /// <param name="request"></param>
        private void ValidateAuctionCreate(AuctionAddRequestModel request)
        {
            // todo: kke: anything else required here?
            if (request.AuctionName.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.MissingAuctionsInformation); }
            if (request.AuctionTopCategoryIds.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.MissingAuctionsInformation); }
            if (request.AuctionSubCategoryIds.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.MissingAuctionsInformation); }
            if (request.AuctionStartingPrice.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.MissingAuctionsInformation); }
            if (request.AuctionStartDate.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.MissingAuctionsInformation); }
            if (request.AuctionApplyTillDate.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.MissingAuctionsInformation); }
            if (request.AuctionEndDate.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.MissingAuctionsInformation); }
            if (request.AuctionCreatorId.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.MissingAuctionsInformation); }
            if (request.AuctionFormatId.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.MissingAuctionsInformation); }

            m_permissionService.IsLoggedInUserActive();

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

            m_permissionService.IsLoggedInUserActive();
        }

        private void ValidateAuctionDelete(AuctionDeleteRequestModel request)
        {
            if (request.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.MissingAuctionsInformation); }

            m_permissionService.IsLoggedInUserActive();
        }
    }
}
