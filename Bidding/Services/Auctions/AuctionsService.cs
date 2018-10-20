using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            var response = new AuctionListResponseModel();

            var pageNum = request.OffsetStart;

            var start = Math.Max(pageNum - 1, 0) * request.OffsetEnd;
            var end = start + request.OffsetEnd;
            response = m_auctionsRepository.Search(request, start, end);

            int totalPages = 0;
            //check if total data < pagesize
            if (response.Auctions.Count > 0)
            {
                if (response.Count > request.OffsetEnd)
                {
                    totalPages = response.Count / request.OffsetEnd;
                    if (response.Count % request.OffsetEnd > 0)
                    {
                        totalPages++;
                    }
                }
                else
                {
                    totalPages = 1;
                }
            }
            if (totalPages < pageNum)
            {
                pageNum = totalPages;
            }
            if (response.Auctions.Count == 0)
            {
                return response;
            }

            if (((pageNum - 1) * request.OffsetEnd) < 0)
            {
                response.Offset = 0;
            }
            else
            {
                response.Offset = (pageNum - 1) * request.OffsetEnd;
            }
            response.TotalPages = totalPages;
            response.CurrentPage = request.OffsetStart;
            return response;
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
