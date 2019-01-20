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

        public AuctionListResponseModel Search(AuctionListRequestModel request)
        {
            // implement this when the list request model is done!
            // todo: kke: validate all request values!

            // pagination
            int startFromThisItem = request.OffsetStart;
            int takeUntilThisItem = request.OffsetEnd;
            int startFrom = Math.Max(startFromThisItem - 1, 0) * takeUntilThisItem;
            int endAt = startFrom + takeUntilThisItem;

            AuctionListResponseModel auctionsResponse = m_auctionsRepository.Search(request, startFrom, endAt);

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

            // prob not really needed here anymore!
            //if (((startFromThisItem - 1) * takeUntilThisItem) < 0)
            //{
            //    auctionsResponse.Offset = 0;
            //}
            //else
            //{
            //    auctionsResponse.Offset = (startFromThisItem - 1) * takeUntilThisItem;
            //}

            auctionsResponse.CurrentPage = request.CurrentPage;
            return auctionsResponse;
        }

        public AuctionDetailsModel Details(int auctionId)
        {
            return m_auctionsRepository.Details(auctionId);
        }

        public List<CategoryModel> Categories()
        {
            return m_auctionsRepository.Categories();
        }

        public bool Update(AuctionEditRequestModel request)
        {
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
            return m_auctionsRepository.Delete(request);
        }

        private void ValidateAuctionCreate(AuctionAddRequestModel request)
        {
            if (request.IsNotSpecified()) { throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.MissingAuctionsInformation); }
        }
    }
}
