using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Bidding.Models.ViewModels.Bidding.Auctions;
using Bidding.Models.ViewModels.Bidding.Auctions.Details;
using Bidding.Models.ViewModels.Bidding.Filters;
using Bidding.Shared.ErrorHandling.Errors;
using Bidding.Shared.Exceptions;
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

        public IEnumerable<Auction> ListWithSearch(AuctionListRequestModel request, int startFrom, int endAt)
        {
            // todo: kke: how to pass list with ids here?
            return m_context.Execute<Auction>
                ($"GetAuctions @startDate = {request.AuctionStartDate}, @endDate = {request.AuctionEndDate}, @start = {startFrom}, @end = {endAt}, @sortByColumn = {request.SortByColumn}, @sortingDirection = {request.SortingDirection}");
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
            return m_context.AuctionDetails
                .Where(auct => auct.AuctionId == request.AuctionId)
                .Select(auct => new AuctionDetailsResponseModel
                {
                    Id = auct.AuctionDetailsId,
                    //AuctionId = auct.AuctionId,
                    //VRN = auct.VehicleRegistrationNumber,
                    //VIN = auct.VehicleIdentificationNumber,
                    //Year = auct.Year,
                    //Evaluation = auct.Evaluation,
                    //AuctionType = auct.AuctionType
                });
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
