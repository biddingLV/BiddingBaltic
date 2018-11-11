using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bidding.Models.ViewModels.Bidding.Auctions;
using Bidding.Models.ViewModels.Bidding.Categories;
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
            // improve naming here!
            int startFromThisItem = request.OffsetStart;
            int takeUntilThisItem = request.OffsetEnd;
            int start = Math.Max(startFromThisItem - 1, 0) * takeUntilThisItem;
            int end = start + takeUntilThisItem;

            AuctionListResponseModel auctionsResponse = m_auctionsRepository.Search(request, start, end);

            int totalPages = 0;
            //check if total data < pagesize
            if (auctionsResponse.Auctions.Count > 0)
            {
                if (auctionsResponse.Auctions.Count > takeUntilThisItem)
                {
                    totalPages = auctionsResponse.Auctions.Count / takeUntilThisItem;
                    if (auctionsResponse.Auctions.Count % takeUntilThisItem > 0)
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

            if (auctionsResponse.Auctions.Count == 0)
            {
                return auctionsResponse;
            }

            if (((startFromThisItem - 1) * takeUntilThisItem) < 0)
            {
                auctionsResponse.Offset = 0;
            }
            else
            {
                auctionsResponse.Offset = (startFromThisItem - 1) * takeUntilThisItem;
            }

            auctionsResponse.TotalPages = totalPages;
            auctionsResponse.CurrentPage = request.OffsetStart;
            return auctionsResponse;
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
            return m_auctionsRepository.Create(request);
        }

        public bool Delete(AuctionDeleteRequestModel request)
        {
            return m_auctionsRepository.Delete(request);
        }
    }
}
