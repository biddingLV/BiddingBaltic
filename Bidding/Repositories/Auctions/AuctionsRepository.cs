using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Bidding.Database.DatabaseModels.Auctions;
using Bidding.Models.ViewModels.Bidding.Auctions;
using Bidding.Models.ViewModels.Bidding.Auctions.Add;
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
    public class AuctionsRepository
    {
        private readonly BiddingContext m_context;

        public AuctionsRepository(BiddingContext context)
        {
            m_context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="startFrom">pagination start from</param>
        /// <param name="endAt"> pagination end at</param>
        /// <param name="categoryIds">top category ids</param>
        /// <param name="typeIds">sub-category ids</param>
        /// <returns></returns>
        public IEnumerable<AuctionListModel> ListWithSearch(AuctionListRequestModel request, int startFrom, int endAt, List<int> selectedCategoryIds, List<int> selectedTypeIds)
        {
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
                    Value = CreateIdTable(selectedCategoryIds, "CategoryId"),
                    TypeName = "CategoryIdArray",
                    SqlDbType = SqlDbType.Structured
                };

                SqlParameter types = new SqlParameter
                {
                    ParameterName = "selectedTypes",
                    Direction = ParameterDirection.Input,
                    Value = CreateIdTable(selectedTypeIds, "TypeId"),
                    TypeName = "TypeIdArray",
                    SqlDbType = SqlDbType.Structured
                };

                SqlParameter startPaginationFrom = new SqlParameter
                {
                    ParameterName = "start",
                    Direction = ParameterDirection.Input,
                    Value = startFrom,
                    SqlDbType = SqlDbType.Int
                };

                SqlParameter endPaginationAt = new SqlParameter
                {
                    ParameterName = "end",
                    Direction = ParameterDirection.Input,
                    Value = endAt,
                    SqlDbType = SqlDbType.Int
                };

                return m_context.Query<AuctionListModel>()
                    .FromSql("GetAuctions @selectedCategories, @selectedTypes, @start, @end", categories, types, startPaginationFrom, endPaginationAt);
            }
            catch (Exception ex)
            {
                throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.CouldNotFetchAuctionList, ex);
            }
        }

        /// <summary>
        /// Gets total count of active auctions
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Auction> TotalAuctionCount()
        {
            return m_context.Auctions.Where(auct => auct.Deleted == false && auct.EndDate >= DateTime.Now.Date);
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

        /// <summary>
        /// Loads all active auction creators
        /// </summary>
        /// <returns></returns>
        public IEnumerable<AuctionCreatorItemModel> Creators()
        {
            int adminRoleId = m_context.Roles
                .Where(rol => rol.Name == "Admin" && rol.Deleted == false)
                .Select(rol => rol.RoleId).FirstOrDefault();

            if (adminRoleId.IsNotSpecified())
            {
                throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.CouldNotFetchAuctionCreatorList);
            }
            else
            {
                return m_context.Users
                    .Where(usr => usr.Deleted == false && usr.RoleId == adminRoleId)
                    .Select(usr => new AuctionCreatorItemModel
                    {
                        AuctionCreatorId = usr.UserId,
                        FirstName = usr.FirstName,
                        MiddleName = usr.MiddleName,
                        LastName = usr.LastName
                    });
            }
        }

        /// <summary>
        /// Loads all active auction formats
        /// </summary>
        /// <returns></returns>
        public IEnumerable<AuctionFormatItemModel> Formats()
        {
            return m_context.AuctionFormats
                .Where(afor => afor.Deleted == false)
                .Select(afor => new AuctionFormatItemModel { AuctionFormatId = afor.AuctionFormatId, AuctionFormatName = afor.Name });
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
                           AuctionName = auct.Name,
                           CategoryName = cat.Name,
                           TypeName = typ.Name,
                           AuctionStartingPrice = auct.StartingPrice,
                           AuctionStartDate = auct.StartDate,
                           AuctionEndDate = auct.EndDate ?? auct.EndDate.Value
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

        /// <summary>
        /// Adds a new auction
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public bool Create(AuctionAddRequestModel request, int loggedInUserId)
        {
            int defaultAuctionStatusId = ValidateAndFetchAuctionStatus();

            Auction newAuction = new Auction()
            {
                Name = request.AuctionName,
                StartingPrice = request.AuctionStartingPrice,
                StartDate = request.AuctionStartDate,
                ApplyDate = request.AuctionApplyTillDate ?? request.AuctionApplyTillDate.Value,
                EndDate = request.AuctionEndDate ?? request.AuctionEndDate.Value,
                CreatedAt = DateTime.UtcNow, // utc time always
                CreatedBy = loggedInUserId,
                Deleted = false,
                AuctionStatusId = defaultAuctionStatusId,
                AuctionCategories = PopulateAuctionCategories(request.AuctionTopCategoryIds).ToList(),
                AuctionTypes = new List<AuctionType>()
                {
                    new AuctionType { TypeId = request.AuctionTopCategoryIds.First() } // needs to support multiple cat in one go!
                }
            };

            // todo: kke: does this make sense?
            // that auction id also in auction details table?
            // and and auction details id missing from auction table?
            //AuctionDetails auctionDetails = new AuctionDetails()
            //{
            //    AuctionId = auction.AuctionId,
            //    AuctionFormatId = request.AuctionFormatId
            //    // AuctionConditionId
            //};

            var strategy = m_context.Database.CreateExecutionStrategy();
            strategy.Execute(() =>
            {
                try
                {
                    using (var transaction = m_context.Database.BeginTransaction())
                    {
                        m_context.Add(newAuction);
                        m_context.SaveChanges();
                        transaction.Commit();
                    }
                }
                catch (Exception ex)
                {
                    throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.CouldNotCreateAuction, ex);
                }
            });

            return true;
        }

        public bool Delete(AuctionDeleteRequestModel request)
        {
            return true;
        }

        /// <summary>
        /// Creates Sql Server array table, used to pass list of ids to Sql Server
        /// </summary>
        /// <param name="ids">List with ids</param>
        /// <param name="nameOfId">Name of the id column</param>
        /// <returns></returns>
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
        /// Load default auction status id and also validate if it is still active in database
        /// </summary>
        /// <returns></returns>
        private int ValidateAndFetchAuctionStatus()
        {
            int defaultAuctionStatusId =
                m_context.AuctionStatuses.Where(sta => sta.Name == "Aktīva").Select(sta => sta.AuctionStatusId).FirstOrDefault();

            if (defaultAuctionStatusId.IsNotSpecified())
            {
                throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.MissingRequiredAuctionStatus);
            }

            return defaultAuctionStatusId;
        }

        /// <summary>
        /// Populate AuctionCategories mapping / intermediary table
        /// </summary>
        /// <param name="selectedCategoryIds"></param>
        /// <returns></returns>
        private IEnumerable<AuctionCategory> PopulateAuctionCategories(List<int> selectedCategoryIds)
        {
            return selectedCategoryIds.Select(cat => new AuctionCategory() { CategoryId = cat });
        }
    }
}
