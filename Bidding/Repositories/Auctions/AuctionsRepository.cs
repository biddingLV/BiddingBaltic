using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Bidding.Models.ViewModels.Bidding.Auctions;
using Bidding.Models.ViewModels.Bidding.Auctions.Details;
using Bidding.Models.ViewModels.Bidding.Categories;
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
                CreatorFirstName = auct.CreatorFirstName,
                CreatorLastName = auct.CreatorLastName,
                CreatorId = auct.CreatorId
            })
            .AsNoTracking()
            .ToList();

            // todo: kke: add the where condition here!
            response.ItemCount = m_context.Auctions.Count(); // (auct => auct.StartDate ==);

            return response;
        }

        public IQueryable<AuctionDetailsResponseModel> Details(AuctionDetailsRequestModel request)
        {
            return m_context.AuctionDetails
                .Where(auct => auct.AuctionId == request.AuctionId)
                .Select(auct => new AuctionDetailsResponseModel
                {
                    Id = auct.Id,
                    AuctionId = auct.AuctionId,
                    VRN = auct.VehicleRegistrationNumber,
                    VIN = auct.VehicleIdentificationNumber,
                    Year = auct.Year,
                    Evaluation = auct.Evaluation,
                    AuctionType = auct.AuctionType
                });
        }

        public IQueryable<CategoryModel> Categories()
        {
            return m_context.Categories.Select(cat => new CategoryModel { Id = cat.Id, Name = cat.Name });
        }

        public bool Update(AuctionEditRequestModel request)
        {
            return true;
        }

        public bool Create(AuctionAddRequestModel request)
        {
            Auction auction = new Auction()
            {
                Name = request.AuctionName,
                Description = request.Description,
                Price = request.StartingPrice
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
