using Bidding.Models.DatabaseModels.Auctions;
using Bidding.Models.ViewModels.Auctions.Add;
using Bidding.Models.ViewModels.Auctions.Delete;
using Bidding.Models.ViewModels.Auctions.Details;
using Bidding.Models.ViewModels.Auctions.Edit;
using Bidding.Models.ViewModels.Auctions.List;
using Bidding.Models.ViewModels.Auctions.Shared;
using Bidding.Models.ViewModels.Filters;
using Bidding.Repositories.Auctions;
using Bidding.Services.Shared;
using Bidding.Services.Shared.Permissions;
using Bidding.Shared.Constants;
using Bidding.Shared.ErrorHandling.Errors;
using Bidding.Shared.ErrorHandling.Validators;
using Bidding.Shared.Exceptions;
using Bidding.Shared.Pagination;
using Bidding.Shared.Utility.Validation.Comparers;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Bidding.Services.Auctions
{
    public class AuctionsService
    {
        private readonly PermissionService m_permissionService;
        private readonly AuctionsRepository m_auctionsRepository;
        private readonly FileUploaderService m_fileUploaderService;

        public AuctionsService(AuctionsRepository auctionRepository, PermissionService permissionService, FileUploaderService fileUploaderService)
        {
            m_permissionService = permissionService ?? throw new ArgumentNullException(nameof(permissionService));
            m_auctionsRepository = auctionRepository ?? throw new ArgumentNullException(nameof(auctionRepository));
            m_fileUploaderService = fileUploaderService ?? throw new ArgumentNullException(nameof(fileUploaderService));
        }

        public AuctionListResponseModel GetAuctions(AuctionListRequestModel request)
        {
            ValidateAuctionList(request);

            // note: kke: Always set searchValue to be null here, 
            // because the user needs to be signed-in to use search functionality!
            request.SearchValue = null;

            (int startFrom, int endAt) = Pagination.GetStartAndEnd(request);

            AuctionListResponseModel auctionsResponse = new AuctionListResponseModel()
            {
                Auctions = m_auctionsRepository.GetAuctions(request, startFrom, endAt).ToList(),
                ItemCount = m_auctionsRepository.TotalAuctionCount().Count()
            };

            Pagination.PaginateResponse(ref auctionsResponse, TableItem.DEFAULT_SIZE, request.CurrentPage);

            return auctionsResponse;
        }

        public AuctionListResponseModel GetAuctionsWithSearch(AuctionListRequestModel request)
        {
            ValidateAuctionList(request);

            // todo: kke: validate request.SearchValue to be valid value!

            (int startFrom, int endAt) = Pagination.GetStartAndEnd(request);

            AuctionListResponseModel auctionsResponse = new AuctionListResponseModel()
            {
                Auctions = m_auctionsRepository.GetAuctions(request, startFrom, endAt).ToList(),
                ItemCount = m_auctionsRepository.TotalAuctionCount().Count()
            };

            Pagination.PaginateResponse(ref auctionsResponse, TableItem.DEFAULT_SIZE, request.CurrentPage);

            return auctionsResponse;
        }

        /// <summary>
        /// Loads all filters, top category filter and sub-category filter
        /// </summary>
        /// <returns></returns>
        public AuctionFilterModel Filters()
        {
            return new AuctionFilterModel()
            {
                TopCategories = m_auctionsRepository.LoadActiveTopCategoriesWithCount().ToList(),
                SubCategories = m_auctionsRepository.LoadActiveSubCategoriesWithCount().ToList()
            };
        }

        public CategoriesWithTypesModel CategoriesWithTypes()
        {
            m_permissionService.IsLoggedInUserActive();

            return new CategoriesWithTypesModel()
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

        public AuctionFormatModel CreateVehicleDetails()
        {
            m_permissionService.IsLoggedInUserActive();

            return new AuctionFormatModel()
            {
                Formats = new List<AuctionFormatItemModel>() // m_auctionsRepository.CreateVehicleDetails()
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

        public async Task<AuctionDetailsResponseModel> DetailsAsync(AuctionDetailsRequestModel request)
        {
            if (request.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.MissingAuctionsInformation); }
            if (request.AuctionId.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.IncorrectAuction); }

            // var xxx = await m_fileUploaderService.GetCloudBlobContainer2();

            return await m_auctionsRepository.DetailsAsync(request);
        }

        public AuctionEditDetailsResponseModel EditDetails(int auctionId)
        {
            if (auctionId.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.MissingAuctionsInformation); }

            m_permissionService.IsLoggedInUserActive();

            return m_auctionsRepository.EditDetails(auctionId);
        }

        public bool UpdateAuctionDetails(AuctionEditRequestModel request)
        {
            ValidateAuctionUpdate(request);

            int? loggedInUserId = m_permissionService.GetUserIdFromClaimsPrincipal();

            return m_auctionsRepository.UpdateAuctionDetails(request, loggedInUserId.Value);
        }

        public bool Delete(AuctionDeleteRequestModel request)
        {
            ValidateAuctionDelete(request);

            int? loggedInUserId = m_permissionService.GetUserIdFromClaimsPrincipal();

            return m_auctionsRepository.Delete(request, loggedInUserId.Value);
        }

        public bool Create(AddAuctionRequestModel request)
        {
            if (request.IsNotSpecified()) throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.MissingAuctionsInformation);
            if (request.AboutAuction.AuctionTopCategoryId.IsNotSpecified()) throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.MissingAuctionsInformation);

            m_permissionService.IsLoggedInUserActive();

            bool status = false;

            if (request.AboutAuction.AuctionTopCategoryId == Categories.ITEM_CATEGORY)
            {
                status = CreateItemAuction(request);
            }
            else if (request.AboutAuction.AuctionTopCategoryId == Categories.VEHICLE_CATEGORY)
            {
                status = CreateVehicleAuction(request);
            }
            else if (request.AboutAuction.AuctionTopCategoryId == Categories.PROPERTY_CATEGORY)
            {
                status = CreatePropertyAuction(request);
            }
            else
            {
                throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.MissingAuctionsInformation);
            }

            return status;
        }

        /// <summary>
        /// Validate auction list permissions and input values
        /// </summary>
        /// <param name="request"></param>
        private void ValidateAuctionList(AuctionListRequestModel request)
        {
            if (request.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.MissingAuctionsInformation); }
            if (request.SortingDirection.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.MissingAuctionsInformation); }
            if (request.OffsetStart < 0) { throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.MissingAuctionsInformation); }
            if (request.OffsetEnd.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.MissingAuctionsInformation); }
            if (request.CurrentPage < 0) { throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.MissingAuctionsInformation); }
            if (request.SortingDirection != "asc" && request.SortingDirection != "desc") { throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.MissingAuctionsInformation); }

            // todo: kke: validate sort by columns list!
            // todo: kke: validate if smth else is not missing here!
            if (request.SortByColumn.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.MissingAuctionsInformation); }
            List<string> allowedSortByColumns = new List<string> { "AuctionName", "AuctionStartingPrice", "AuctionStartDate", "AuctionEndDate" };
            if (allowedSortByColumns.Contains(request.SortByColumn) == false) { throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.MissingAuctionsInformation); }
        }

        private bool CreateItemAuction(AddAuctionRequestModel request)
        {
            // ValidateAuctionItemCreate(request);

            int? loggedInUserId = m_permissionService.GetUserIdFromClaimsPrincipal();

            return m_auctionsRepository.CreateItemAuction(request, loggedInUserId.Value);
        }

        private bool CreatePropertyAuction(AddAuctionRequestModel request)
        {
            // ValidateAuctionItemCreate(request);

            int? loggedInUserId = m_permissionService.GetUserIdFromClaimsPrincipal();

            return m_auctionsRepository.CreatePropertyAuction(request, loggedInUserId.Value);
        }

        private bool CreateVehicleAuction(AddAuctionRequestModel request)
        {
            // ValidateAuctionItemCreate(request);

            int? loggedInUserId = m_permissionService.GetUserIdFromClaimsPrincipal();

            return m_auctionsRepository.CreateVehicleAuction(request, loggedInUserId.Value);
        }

        private void ValidateAuctionItemCreate(string request)
        {
            // if (request.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.MissingAuctionsInformation); }
            // if (request.AuctionName.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.MissingAuctionsInformation); }
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
