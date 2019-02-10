using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Bidding.Models.ViewModels.Bidding.Auctions;
using Bidding.Models.ViewModels.Bidding.Auctions.Details;
using Bidding.Models.ViewModels.Bidding.Filters;
using Bidding.Shared.ErrorHandling.Errors;
using Bidding.Shared.Exceptions;
using Bidding.Shared.Utility;
using BiddingAPI.Models.DatabaseModels;
using BiddingAPI.Models.DatabaseModels.Bidding;
using BiddingAPI.Models.ViewModels.Bidding.Auctions;
using BiddingAPI.Repositories.Auctions;

namespace BiddingAPI.Services.Auctions
{
    public class AuctionsService : IAuctionsService
    {
        private readonly IAuctionsRepository m_auctionsRepository;

        public AuctionsService(IAuctionsRepository auctionRepository)
        {
            m_auctionsRepository = auctionRepository ?? throw new ArgumentNullException(nameof(auctionRepository));
        }

        public AuctionListResponseModel ListWithSearch(AuctionListRequestModel request)
        {
            // validate required inputs
            ValidateAuctionListWithSearch(request);

            // pagination assignments
            int startFromThisItem = request.OffsetStart;
            int takeUntilThisItem = request.OffsetEnd;
            int startFrom = Math.Max(startFromThisItem - 1, 0) * takeUntilThisItem;
            int endAt = startFrom + takeUntilThisItem;

            // todo: kke: remove after testing done!
            string dateInString = "01/12/2018";
            DateTime startDate = DateTime.Parse(dateInString);
            DateTime expiryDate = startDate.AddDays(365);

            AuctionListResponseModel auctionsResponse = new AuctionListResponseModel()
            {
                Auctions = m_auctionsRepository.ListWithSearch(request, startFrom, endAt).ToList(),
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
            return new AuctionFilterModel()
            {
                TopCategories = m_auctionsRepository.LoadTopCategories().ToList()
            };
        }

        public IEnumerable<AuctionDetailsResponseModel> Details(AuctionDetailsRequestModel request)
        {
            return m_auctionsRepository.Details(request);
        }



        public bool Update(AuctionEditRequestModel request)
        {
            // todo: kke: add validate that the user is active!

            // validations & permission checks
            ValidateAuctionUpdate(request);

            return m_auctionsRepository.Update(request);
        }

        public bool Create(AuctionAddRequestModel request)
        {
            // todo: kke: add validate that the user is active!

            // validations & permission checks
            ValidateAuctionCreate(request);

            return m_auctionsRepository.Create(request);
        }

        public bool Delete(AuctionDeleteRequestModel request)
        {
            // todo: kke: add validate that the user is active!

            // validations & permission checks
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

        private void ValidateAuctionCreate(AuctionAddRequestModel request)
        {
            if (request.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.MissingAuctionsInformation); }
        }

        private void ValidateAuctionUpdate(AuctionEditRequestModel request)
        {
            if (request.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.MissingAuctionsInformation); }
        }

        private void ValidateAuctionDelete(AuctionDeleteRequestModel request)
        {
            if (request.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.MissingAuctionsInformation); }
        }
    }
}
