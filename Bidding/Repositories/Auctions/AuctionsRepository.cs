using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
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

                return m_context.Query<AuctionListModel>()
                    .FromSql("GetAuctions @selectedCategories, @selectedTypes", categories, types);
            }
            catch (Exception ex)
            {
                throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.CouldNotFetchAuctionList, ex);
            }
        }

        /// <summary>
        /// Gets total auction count based on specific date/time range
        /// </summary>
        /// <param name="startDate">Start date</param>
        /// <param name="endDate">End date</param>
        /// <returns></returns>
        public IEnumerable<Auction> TotalAuctionCount(DateTime auctionStartDate, DateTime auctionEndDate)
        {
            return m_context.Auctions.Where(auct => auct.StartDate >= auctionStartDate && auct.EndDate <= auctionEndDate);
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
                           AuctionEndDate = auct.EndDate
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
        public bool Create(AuctionAddRequestModel request)
        {
            Auction auction = new Auction()
            {
                Name = request.AuctionName,
                //StartingPrice = request.AuctionStartingPrice,
                //StartDate = request.AuctionStartDate,
                //ApplyDate = request.AuctionApplyDate,
                //EndDate = request.AuctionEndDate
                // AuctionStatusId = request.AuctionStatusId
            };

            var strategy = m_context.Database.CreateExecutionStrategy();
            strategy.Execute(() =>
            {
                try
                {
                    using (var transaction = m_context.Database.BeginTransaction())
                    {
                        EntityEntry<Auction> newAuction = m_context.Add(auction);

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
    }
}
