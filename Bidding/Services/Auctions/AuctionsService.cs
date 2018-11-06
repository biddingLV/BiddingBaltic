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

        public List<AuctionModel> Search(AuctionModel request)
        {
            // todo: kke: validate all request values!

            int pageNum = request.OffsetStart;
            int start = Math.Max(pageNum - 1, 0) * request.OffsetEnd;
            int end = start + request.OffsetEnd;
            List<AuctionModel> response = m_auctionsRepository.Search(request, start, end);

            //int totalPages = 0;
            ////check if total data < pagesize
            //if (response.Count > 0)
            //{
            //    if (response.Count > request.OffsetEnd)
            //    {
            //        totalPages = response.Count / request.OffsetEnd;
            //        if (response.Count % request.OffsetEnd > 0)
            //        {
            //            totalPages++;
            //        }
            //    }
            //    else
            //    {
            //        totalPages = 1;
            //    }
            //}
            //if (totalPages < pageNum)
            //{
            //    pageNum = totalPages;
            //}
            //if (response.Count == 0)
            //{
            //    return response;
            //}

            // todo: kke: add this back when ready for pagination! 
            //if (((pageNum - 1) * request.OffsetEnd) < 0)
            //{
            //    response.Offset = 0;
            //}
            //else
            //{
            //    response.Offset = (pageNum - 1) * request.OffsetEnd;
            //}
            //response.TotalPages = totalPages;
            //response.CurrentPage = request.OffsetStart;
            return response;
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
