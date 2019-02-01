using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Bidding.Models.ViewModels.Bidding.Auctions;
using Bidding.Models.ViewModels.Bidding.Auctions.Details;
using Bidding.Models.ViewModels.Bidding.Categories;
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

            AuctionListResponseModel auctionsResponse = new AuctionListResponseModel()
            {
                Auctions = m_auctionsRepository.ListWithSearch(request, startFrom, endAt).ToList(),
                ItemCount = m_auctionsRepository.TotalAuctionCount(request.StartDate, request.EndDate).Count()
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

        public IEnumerable<AuctionDetailsResponseModel> Details(AuctionDetailsRequestModel request)
        {
            return m_auctionsRepository.Details(request);
        }

        public IEnumerable<CategoryModel> Categories()
        {
            return m_auctionsRepository.Categories();
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

        private void ValidateAuctionListWithSearch(AuctionListRequestModel request)
        {
            if (request.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.MissingAuctionsInformation); }
            if (request.StartDate.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.MissingAuctionsInformation); }
            if (request.EndDate.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.MissingAuctionsInformation); }
            if (request.SortByColumn.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.MissingAuctionsInformation); }
            if (request.SortingDirection.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.MissingAuctionsInformation); }
            if (request.OffsetStart < 0) { throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.MissingAuctionsInformation); }
            if (request.OffsetEnd.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.MissingAuctionsInformation); }
            if (request.CurrentPage < 0) { throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.MissingAuctionsInformation); }

            // todo: kke: sorting direction can only be -> asc | desc
            // todo: kke: sort by column can only be, whats in the list?
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
