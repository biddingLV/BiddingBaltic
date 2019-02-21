using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Bidding.Models.ViewModels.Bidding.Auctions;
using Bidding.Models.ViewModels.Bidding.Auctions.Details;
using Bidding.Models.ViewModels.Bidding.Auctions.List;
using Bidding.Models.ViewModels.Bidding.Filters;
using Bidding.Shared.ErrorHandling.Errors;
using Bidding.Shared.Exceptions;
using Bidding.Shared.Utility;
using BiddingAPI.Models.DatabaseModels;
using BiddingAPI.Models.DatabaseModels.Bidding;
using BiddingAPI.Models.ViewModels.Bidding.Auctions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BiddingAPI.Repositories.Auctions
{
    public class AuctionsRepository : IAuctionsRepository
    {
        private readonly BiddingContext m_context;

        public AuctionsRepository(BiddingContext context)
        {
            m_context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IEnumerable<AuctionListModel> ListWithSearch(AuctionListRequestModel request, int startFrom, int endAt, List<int> selectedCategoryIds, List<int> selectedTypeIds)
        {
            // todo: kke: add OrganizationIdArray as a migration!
            try
            {
                // todo: kke: move this to the stored procedure as a case / if
                if (selectedCategoryIds.IsNotSpecified())
                {
                    selectedCategoryIds = m_context.Categories.Select(cat => cat.CategoryId).ToList();
                }

                // todo: kke: move this to the stored procedure as a case / if
                if (selectedTypeIds.IsNotSpecified())
                {
                    selectedTypeIds = m_context.Types.Select(typ => typ.TypeId).ToList();
                }

                SqlParameter categories = new SqlParameter
                {
                    ParameterName = "selectedCategories",
                    Direction = ParameterDirection.Input,
                    Value = CreateIdTable(selectedCategoryIds, "OrganizationId"),
                    TypeName = "[dbo].OrganizationIdArray",
                    SqlDbType = SqlDbType.Structured
                };

                SqlParameter types = new SqlParameter
                {
                    ParameterName = "selectedTypes",
                    Direction = ParameterDirection.Input,
                    Value = CreateIdTable(selectedTypeIds, "OrganizationId"),
                    TypeName = "[dbo].OrganizationIdArray",
                    SqlDbType = SqlDbType.Structured
                };

                // todo: kke: remove after pagination fixed!
                //return m_context.Query<AuctionListModel>()
                //    .FromSql($"GetAuctions @startDate = {request.AuctionStartDate}, @endDate = {request.AuctionEndDate}, @start = {startFrom}, @end = {endAt}, @sortByColumn = {request.SortByColumn}, @sortingDirection = {request.SortingDirection}, @categories = {categoryIds}")
                //    .ToList();

                return m_context.Query<AuctionListModel>()
                    .FromSql($"GetAuctions @selectedCategories, @selectedTypes", categories, types);
            }
            catch (Exception ex)
            {
                // todo: kke: improve error message!
                throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.CouldNotCreateAuction, ex);
            }
        }

        private DataTable CreateIdTable(IEnumerable<int> ids, string nameOfId)
        {
            DataTable table = new DataTable();
            table.Columns.Add(nameOfId, typeof(int));

            foreach (int id in ids)
            {
                table.Rows.Add(id);
            }

            return table;
        }

        /// <summary>
        /// Gets total auction count based on specific date/time range
        /// </summary>
        /// <param name="startDate">Start date</param>
        /// <param name="endDate">End date</param>
        /// <returns></returns>
        public IEnumerable<Auction> TotalAuctionCount(DateTime auctionStartDate, DateTime auctionEndDate)
        {
            return m_context.Auctions.Where(auct => auct.AuctionStartDate >= auctionStartDate && auct.AuctionEndDate <= auctionEndDate);
        }

        /// <summary>
        /// Loads top categories with total count
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TopCategoryFilterModel> LoadTopCategories()
        {
            return m_context.TopCategoryFilter.FromSql("GetTopCategoriesWithCount");
        }

        /// <summary>
        /// Loads sub-categories with total count
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SubCategoryFilterModel> LoadSubCategories()
        {
            return m_context.SubCategoryFilter.FromSql("GetSubCategoriesWithCount");
        }

        public IEnumerable<AuctionDetailsResponseModel> Details(AuctionDetailsRequestModel request)
        {
            // check if even auction exists and only then do the join
            bool auctionExists = m_context.Auctions.Any(auct => auct.AuctionId == request.AuctionId);

            if (auctionExists)
            {
                // get auction details
                return from auct in m_context.Auctions
                       join acat in m_context.AuctionCategories on auct.AuctionId equals acat.AuctionId
                       join atyp in m_context.AuctionTypes on auct.AuctionId equals atyp.AuctionId
                       join cat in m_context.Categories on acat.CategoryId equals cat.CategoryId
                       join typ in m_context.Types on atyp.TypeId equals typ.TypeId
                       where auct.AuctionId == request.AuctionId
                       select new AuctionDetailsResponseModel()
                       {
                           AuctionName = auct.AuctionName,
                           CategoryName = cat.CategoryName,
                           TypeName = typ.TypeName,
                           AuctionStartingPrice = auct.AuctionStartingPrice,
                           AuctionStartDate = auct.AuctionStartDate,
                           AuctionEndDate = auct.AuctionEndDate
                       };
            }
            else
            {
                throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.IncorrectAuction);
            }
        }

        public bool Update(AuctionEditRequestModel request)
        {
            return true;
        }

        public bool Create(AuctionAddRequestModel request)
        {
            Auction auction = new Auction()
            {
                AuctionName = request.AuctionName,
                AuctionStartingPrice = request.StartingPrice
            };

            var strategy = m_context.Database.CreateExecutionStrategy();
            strategy.Execute(() =>
            {
                try
                {
                    using (var transaction = m_context.Database.BeginTransaction())
                    {
                        // save the auction
                        EntityEntry<Auction> newAuction = m_context.Add(auction);
                        m_context.SaveChanges();

                        transaction.Commit();
                    }
                }
                catch (Exception ex)
                {
                    // todo: kke: what about the inner exception here?
                    throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.CouldNotCreateAuction);
                }
            });

            return true;
        }

        public bool Delete(AuctionDeleteRequestModel request)
        {
            return true;
        }
    }
}
