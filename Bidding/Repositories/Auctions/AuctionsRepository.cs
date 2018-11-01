using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Bidding.Models.ViewModels.Bidding.Auctions;
using BiddingAPI.Models.DatabaseModels;
using BiddingAPI.Models.DatabaseModels.Bidding;
using BiddingAPI.Models.ViewModels.Bidding.Auctions;
using BiddingAPI.Models.ViewModels.Bidding.Auctions.List;
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

        public List<AuctionModel> Search(AuctionModel request, int? start, int? end)
        {
            return m_context.AuctionModel.FromSql($"EXEC dbo.[GetAuctions] @StartDate = {request.StartDate}, @EndDate = {request.EndDate}")
                .Select(auct => new AuctionModel()
                {
                    Id = auct.Id,
                    Brand = auct.Brand,
                    Description = auct.Description,
                    Price = auct.Price,
                    Type = auct.Type,
                    EndDate = auct.EndDate,
                    StartDate = auct.StartDate
                })
                .AsNoTracking()
                .ToList();
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
