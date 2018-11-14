using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Bidding.Models.ViewModels.Bidding.Auctions;
using Bidding.Models.ViewModels.Bidding.Auctions.Details;
using Bidding.Models.ViewModels.Bidding.Categories;
using BiddingAPI.Models.DatabaseModels;
using BiddingAPI.Models.DatabaseModels.Bidding;
using BiddingAPI.Models.ViewModels.Bidding.Auctions;
using Microsoft.EntityFrameworkCore;

namespace BiddingAPI.Repositories.Auctions
{
    public class AuctionsRepository : IAuctionsRepository
    {
        private readonly BiddingContext m_context;

        public AuctionsRepository(BiddingContext context)
        {
            m_context = context;
        }

        public AuctionListResponseModel Search(AuctionListRequestModel request, int start, int end)
        {
            AuctionListResponseModel response = new AuctionListResponseModel();

            IQueryable<AuctionItemModel> dbResult =
                m_context
                .AuctionsList
                .FromSql($"EXEC dbo.[GetAuctions] @startDate = {request.StartDate}, @endDate = {request.EndDate}, @start = {start}, @end = {end}, @sortByColumn = {request.SortByColumn}, @sortingDirection = {request.SortingDirection}");

            // todo: kke: check if this is called two times to get result and count!
            response.Auctions =
            dbResult.Select(auct => new AuctionItemModel()
            {
                Id = auct.Id,
                Name = auct.Name,
                Description = auct.Description,
                Price = auct.Price,
                EndDate = auct.EndDate,
                StartDate = auct.StartDate,
                Creator = auct.Creator,
                CreatorId = auct.CreatorId
            })
            .AsNoTracking()
            .ToList();

            // todo: kke: add the where condition here!
            response.ItemCount = m_context.Auctions.Count(); // (auct => auct.StartDate ==);

            return response;
        }

        public AuctionDetailsModel Details(int auctionId)
        {
            return new AuctionDetailsModel();
            // return m_context.AuctionDetails.FirstOrDefault(row => row.Id == auctionId);
        }

        public List<CategoryModel> Categories()
        {
            return m_context.Categories.Select(cat => new CategoryModel { Id = cat.Id, Name = cat.Name }).ToList();
        }

        public bool Update(AuctionEditRequestModel request)
        {
            //var auction = await m_context.Auctions.FirstOrDefaultAsync(lf => lf.Id == request.Id);

            // logging
            // var copy = m_context.CopyEntity(license);

            // auction.Count = request.FeatureValue;
            // m_context.Entry(auction).Property("Count").IsModified = true;

            return m_context.SaveChanges() == 1;
        }

        public bool Create(AuctionAddRequestModel request)
        {

            return true;
            //LicenseFeatures feature = new LicenseFeatures()
            //{
            //    LicenseCode = request.LicenseCode,
            //    FeatureId = request.FeatureId,
            //    // todo: kke: check what happens with unlimited
            //    // Unlimited(frontend) === -1(db)
            //    Count = Convert.ToInt32(request.FeatureValue), // KKE: What if this fails? TryParse would be safer
            //    Visible = true
            //};

            //m_context.Add(feature);
            //// todo: kke: not sure if this is correct for add/post?
            //return await m_context.SaveChangesAsync() == 1;
        }

        public bool Delete(AuctionDeleteRequestModel request)
        {
            return true;
            //m_context
            //    .LicenseFeatures
            //    .RemoveRange(m_context
            //        .LicenseFeatures
            //        .Where(lf => request
            //            .FeatureIds
            //            .Contains(lf.FeatureId) && lf.LicenseCode == request.LicenseCode));

            //return await m_context.SaveChangesAsync() > 0; // Multiple items might be deleted, so == 1 might return false
        }
    }
}
