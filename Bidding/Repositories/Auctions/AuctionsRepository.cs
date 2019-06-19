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
using Bidding.Models.ViewModels.Bidding.Auctions.Add.Categories;
using Bidding.Models.ViewModels.Bidding.Auctions.Details;
using Bidding.Models.ViewModels.Bidding.Auctions.List;
using Bidding.Models.ViewModels.Bidding.Filters;
using Bidding.Shared.ErrorHandling.Errors;
using Bidding.Shared.Exceptions;
using Bidding.Shared.Utility;
using Bidding.Models.DatabaseModels;
using Bidding.Models.DatabaseModels.Bidding;
using Bidding.Models.ViewModels.Bidding.Auctions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Bidding.Database.Contexts;
using Bidding.Models.ViewModels.Bidding.Shared.Categories;
using Bidding.Models.ViewModels.Bidding.Shared.Types;

namespace Bidding.Repositories.Auctions
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
                //if (selectedCategoryIds.IsNotSpecified())
                //{
                //    selectedCategoryIds = m_context.Categories.Select(cat => cat.CategoryId).ToList();
                //}

                // todo: kke: move this to the stored procedure as a case / if
                //if (selectedTypeIds.IsNotSpecified())
                //{
                //    selectedTypeIds = m_context.Types.Select(typ => typ.TypeId).ToList();
                //}

                SqlParameter categories = new SqlParameter
                {
                    ParameterName = "selectedCategories",
                    Direction = ParameterDirection.Input,
                    Value = CreateIdTable(selectedCategoryIds, "CategoryId"),
                    TypeName = "BID_CategoryIdArray",
                    SqlDbType = SqlDbType.Structured
                };

                SqlParameter types = new SqlParameter
                {
                    ParameterName = "selectedTypes",
                    Direction = ParameterDirection.Input,
                    Value = CreateIdTable(selectedTypeIds, "TypeId"),
                    TypeName = "BID_TypeIdArray",
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
                    .FromSql("BID_GetAuctions @selectedCategories, @selectedTypes, @start, @end", categories, types, startPaginationFrom, endPaginationAt);
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
        public IEnumerable<TopCategoryFilterModel> LoadActiveTopCategoriesWithCount()
        {
            // todo: kke: what about status here?
            return m_context.TopCategoryFilter.FromSql("BID_GetTopCategoriesWithCount");
        }

        /// <summary>
        /// Loads sub-categories with total count
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SubCategoryFilterModel> LoadActiveSubCategoriesWithCount()
        {
            // todo: kke: what about status here?
            return m_context.SubCategoryFilter.FromSql("BID_GetSubCategoriesWithCount");
        }

        /// <summary>
        /// Loads all active top-categories
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CategoryModel> LoadTopCategories()
        {
            return m_context.Categories
                .Select(cat => new CategoryModel
                {
                    CategoryId = cat.CategoryId,
                    CategoryName = cat.Name
                });
        }

        /// <summary>
        /// Loads all active sub-categories / types with category connection
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TypeModel> LoadSubCategories()
        {
            return m_context.Types
                .Select(typ => new TypeModel
                {
                    CategoryId = typ.AuctionCategoryId,
                    TypeId = typ.TypeId,
                    TypeName = typ.Name
                });
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

        /// <summary>
        /// Loads all active auction statuses
        /// </summary>
        /// <returns></returns>
        public IEnumerable<AuctionStatusItemModel> Statuses()
        {
            return m_context.AuctionStatuses
                .Where(asta => asta.Deleted == false)
                .Select(asta => new AuctionStatusItemModel { AuctionStatusId = asta.AuctionStatusId, AuctionStatusName = asta.Name });
        }

        public IEnumerable<AuctionDetailsResponseModel> Details(AuctionDetailsRequestModel request)
        {
            // check if even auction exists and only then do the full join
            bool auctionExists = m_context.Auctions.Any(auct => auct.AuctionId == request.AuctionId);

            if (auctionExists)
            {
                return from auct in m_context.Auctions
                           //join acat in m_context.AuctionCategories on auct.AuctionId equals acat.AuctionId
                           //join atyp in m_context.AuctionTypes on auct.AuctionId equals atyp.AuctionId
                           //join cat in m_context.Categories on acat.CategoryId equals cat.CategoryId
                           //join typ in m_context.Types on atyp.TypeId equals typ.TypeId
                           // join adet in m_context.AuctionDetails on auct.AuctionId equals adet.AuctionId
                       where auct.AuctionId == request.AuctionId
                       select new AuctionDetailsResponseModel()
                       {
                           AuctionName = auct.Name,
                           //CategoryName = cat.Name,
                           //TypeName = typ.Name,
                           AuctionStartingPrice = auct.StartingPrice,
                           AuctionStartDate = auct.StartDate,
                           AuctionEndDate = auct.EndDate,
                           AuctionDescription = "" // adet.Description
                       };
            }
            else
            {
                throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.IncorrectAuction);
            }
        }

        public bool CreateItemAuction(ItemAuctionModel request)
        {
            int defaultAuctionStatusId = ValidateAndFetchAuctionStatus();

            Auction newAuction = new Auction()
            {
                Name = request.ItemName,
                StartingPrice = request.ItemStartingPrice,
                StartDate = DateTime.UtcNow,
                ApplyTillDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow,
                AuctionCategoryId = request.AuctionTopCategoryId,
                AuctionTypeId = request.AuctionSubCategoryId,
                AuctionStatusId = defaultAuctionStatusId,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = 1,
                LastUpdatedAt = DateTime.UtcNow,
                LastUpdatedBy = 1
            };

            var strategy = m_context.Database.CreateExecutionStrategy();
            strategy.Execute(() =>
            {
                try
                {
                    using (var transaction = m_context.Database.BeginTransaction())
                    {
                        // add Auction
                        m_context.Auctions.Add(newAuction);
                        m_context.SaveChanges();

                        AuctionItem newAuctionItem = new AuctionItem()
                        {
                            Name = request.ItemName,
                            AuctionId = newAuction.AuctionId,
                            AuctionItemCategoryId = request.AuctionTopCategoryId,
                            AuctionItemTypeId = request.AuctionSubCategoryId,
                            CreatedAt = DateTime.UtcNow,
                            CreatedBy = 1,
                            LastUpdatedAt = DateTime.UtcNow,
                            LastUpdatedBy = 1
                        };

                        // add Auction Item
                        m_context.AuctionItems.Add(newAuctionItem);
                        m_context.SaveChanges();

                        var itemAuctionDetails = new AuctionDetails()
                        {
                            AuctionItemId = newAuctionItem.AuctionItemId,
                            Model = request.ItemModel,
                            ManufacturingDate = DateTime.UtcNow,
                            Condition = "ADD ME",
                            Evaluation = request.ItemEvaluation,
                            CreatedAt = DateTime.UtcNow,
                            CreatedBy = 1,
                            LastUpdatedAt = DateTime.UtcNow,
                            LastUpdatedBy = 1
                        };

                        // add Auction item details
                        m_context.AuctionDetails.Add(itemAuctionDetails);
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

        public bool CreatePropertyAuction(PropertyAuctionModel request)
        {
            return true;
        }

        public bool CreateVehicleAuction(VehicleAuctionModel request)
        {
            return true;
        }

        public bool Update(AuctionEditRequestModel request, int loggedInUserId)
        {
            var strategy = m_context.Database.CreateExecutionStrategy();
            strategy.Execute(() =>
            {
                try
                {
                    using (var transaction = m_context.Database.BeginTransaction())
                    {
                        Auction auctionForUpdate = m_context.Auctions.FirstOrDefault(auct => auct.AuctionId == request.AuctionId);

                        if (auctionForUpdate.IsNotSpecified() == false)
                        {
                            auctionForUpdate.Name = request.AuctionName;
                            auctionForUpdate.StartingPrice = request.AuctionStartingPrice;
                            auctionForUpdate.StartDate = request.AuctionStartDate;
                            auctionForUpdate.EndDate = request.AuctionEndDate;

                            m_context.SaveChanges();
                            transaction.Commit();
                        }
                        else
                        {
                            throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.CouldNotUpdateAuction);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.CouldNotUpdateAuction, ex);
                }
            });

            return true;
        }

        public bool Delete(AuctionDeleteRequestModel request, int loggedInUserId)
        {
            var strategy = m_context.Database.CreateExecutionStrategy();
            strategy.Execute(() =>
            {
                try
                {
                    using (var transaction = m_context.Database.BeginTransaction())
                    {
                        Auction auctionForDelete = m_context.Auctions.FirstOrDefault(auct => auct.AuctionId == 1);

                        if (auctionForDelete.IsNotSpecified() == false)
                        {
                            m_context.Auctions.Where(auct => request.AuctionIds.Contains(auct.AuctionId)).ToList()
                            .ForEach(auct => { auct.Deleted = true; auct.LastUpdatedAt = DateTime.UtcNow; auct.LastUpdatedBy = loggedInUserId; });

                            m_context.SaveChanges();
                            transaction.Commit();
                        }
                        else
                        {
                            throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.CouldNotDeleteAuction);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.CouldNotDeleteAuction, ex);
                }
            });

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

            if (ids.IsNotSpecified() == false)
            {
                foreach (int id in ids)
                {
                    table.Rows.Add(id);
                }
            }

            return table;
        }

        /// <summary>
        /// Load default auction status id and also validate if it is still active in database
        /// </summary>
        /// <returns></returns>
        private int ValidateAndFetchAuctionStatus()
        {
            // todo: kke: add to be constant!
            int defaultAuctionStatusId =
                m_context.AuctionStatuses.Where(sta => sta.Name == "Aktīva").Select(sta => sta.AuctionStatusId).FirstOrDefault();

            if (defaultAuctionStatusId.IsNotSpecified())
            {
                throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.MissingRequiredAuctionStatus);
            }

            return defaultAuctionStatusId;
        }
    }
}
