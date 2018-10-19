using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using BiddingAPI.Models.DatabaseModels;
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

        public AuctionListResponseModel Search(AuctionListRequestModel request, int? start, int? end)
        {
            AuctionListResponseModel response = new AuctionListResponseModel();

            //var parameters = new[] {
            //    new SqlParameter{ ParameterName = "OffsetStart", SqlDbType = SqlDbType.Int, Value = request.OffsetStart },
            //    new SqlParameter{ ParameterName = "OffsetEnd", SqlDbType = SqlDbType.Int, Value = request.OffsetEnd },
            //    new SqlParameter{ ParameterName = "SearchValue", SqlDbType = SqlDbType.VarChar, Value = request.SearchValue },
            //    new SqlParameter{ ParameterName = "SortByColumn", SqlDbType = SqlDbType.VarChar, Value = request.SortByColumn },
            //    new SqlParameter{ ParameterName = "SortingDirection", SqlDbType = SqlDbType.VarChar, Value = request.SortingDirection }
            //};

            //try {
            //    IQueryable<AuctionListViewModel> result = m_context.AuctionListViewModel.FromSql("Bid_Auctions_SearchList @OffsetStart, @OffsetEnd, @SearchValue, @SortByColumn, @SortingDirection", parameters).AsNoTracking();

            //    response.Auctions = await result
            //        .Select(auct => new AuctionListModel()
            //        {
            //            Brand = auct.Brand,
            //            Description = auct.Description,
            //            Price = auct.Price,
            //            Type = auct.Type,
            //            EndDate = auct.EndDate,
            //            StartDate = auct.StartDate
            //        })
            //        .AsNoTracking()
            //        .ToListAsync();
            //}
            //catch(Exception ex)
            //{
            //    var magic = "";
            //}



            return response;
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
