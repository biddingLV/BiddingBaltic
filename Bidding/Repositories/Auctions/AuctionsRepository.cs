using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Bidding.Database.DatabaseModels.Auctions;
using Bidding.Shared.ErrorHandling.Errors;
using Bidding.Shared.Exceptions;
using Bidding.Shared.Utility;
using Bidding.Models.DatabaseModels;
using Bidding.Models.DatabaseModels.Bidding;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Bidding.Database.Contexts;
using Bidding.Shared.Constants;
using Bidding.Models.ViewModels.Bidding.Auctions.List;
using Bidding.Models.ViewModels.Bidding.Filters;
using Bidding.Models.ViewModels.Bidding.Shared.Categories;
using Bidding.Models.ViewModels.Bidding.Shared.Types;
using Bidding.Models.ViewModels.Bidding.Auctions.Shared;
using Bidding.Models.ViewModels.Bidding.Auctions.Details;
using Bidding.Models.ViewModels.Bidding.Auctions.Add;
using Bidding.Models.ViewModels.Bidding.Auctions.Shared.Categories;
using Bidding.Models.ViewModels.Bidding.Auctions.Edit;
using Bidding.Models.ViewModels.Bidding.Auctions.Delete;

namespace Bidding.Repositories.Auctions
{
    public class AuctionsRepository
    {
        private readonly BiddingContext m_context;

        public AuctionsRepository(BiddingContext context)
        {
            m_context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IEnumerable<AuctionListItemModel> GetAuctions(AuctionListRequestModel request, int startFrom, int endAt)
        {
            try
            {
                string convertedCategoryIds = request.TopCategoryIds.IsNotSpecified() ? null : string.Join(',', request.TopCategoryIds.Select(t => t.ToString()));
                string convertedTypeIds = request.TypeIds.IsNotSpecified() ? null : string.Join(',', request.TypeIds.Select(t => t.ToString()));

                SqlParameter categoryIds = new SqlParameter
                {
                    ParameterName = "selectedCategories",
                    Direction = ParameterDirection.Input,
                    Value = HandleNull(convertedCategoryIds),
                    SqlDbType = SqlDbType.Text
                };

                SqlParameter typeIds = new SqlParameter
                {
                    ParameterName = "selectedTypes",
                    Direction = ParameterDirection.Input,
                    Value = HandleNull(convertedTypeIds),
                    SqlDbType = SqlDbType.Text
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

                SqlParameter searchBy = new SqlParameter
                {
                    ParameterName = "searchValue",
                    Direction = ParameterDirection.Input,
                    Value = HandleNull(request.SearchValue),
                    SqlDbType = SqlDbType.Text
                };

                return m_context.Query<AuctionListItemModel>()
                    .FromSql("[dbo].[BID_GetAuctions] @selectedCategories, @selectedTypes, @start, @end, @searchValue", categoryIds, typeIds, startPaginationFrom, endPaginationAt, searchBy);
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
            return m_context.TopCategoryFilter.FromSql("BID_GetTopCategoriesWithCount");
        }

        /// <summary>
        /// Loads sub-categories with total count
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SubCategoryFilterModel> LoadActiveSubCategoriesWithCount()
        {
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

        public IEnumerable<AuctionFormatItemModel> CreateVehicleDetails()
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

        public async Task<AuctionDetailsResponseModel> DetailsAsync(AuctionDetailsRequestModel request)
        {
            // check if even auction exists and only then do the full join
            bool auctionExists = m_context.Auctions.Any(auct => auct.AuctionId == request.AuctionId);

            if (auctionExists)
            {
                AuctionDetailsModel details = (from auct in m_context.Auctions
                                               join aitem in m_context.AuctionItems on auct.AuctionId equals aitem.AuctionId
                                               join adet in m_context.AuctionDetails on aitem.AuctionItemId equals adet.AuctionItemId
                                               join acrea in m_context.AuctionCreators on auct.AuctionCreatorId equals acrea.AuctionCreatorId
                                               where auct.AuctionId == request.AuctionId
                                               select new AuctionDetailsModel
                                               {
                                                   Auction = auct,
                                                   AuctionItem = aitem,
                                                   AuctionDetails = adet,
                                                   AuctionCreator = acrea
                                               }).FirstOrDefault();

                if (details.Auction.AuctionCategoryId == Categories.ITEM_CATEGORY)
                {
                    return SetupItemAuctionDetails(details);
                }
                else if (details.Auction.AuctionCategoryId == Categories.VEHICLE_CATEGORY)
                {
                    return SetupVehicleAuctionDetails(details);
                }
                else if (details.Auction.AuctionCategoryId == Categories.PROPERTY_CATEGORY)
                {
                    return SetupPropertyAuctionDetails(details);
                }
                else
                {
                    throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.MissingAuctionsInformation);
                }
            }
            else
            {
                throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.IncorrectAuction);
            }
        }

        public bool CreateItemAuction(AddAuctionRequestModel request, int loggedInUserId)
        {
            var strategy = m_context.Database.CreateExecutionStrategy();
            strategy.Execute(() =>
            {
                try
                {
                    using (var transaction = m_context.Database.BeginTransaction())
                    {
                        // setup & add - Auction Creator
                        AuctionCreator auctionCreator = SetupNewAuctionCreator(request, loggedInUserId);
                        m_context.AuctionCreators.Add(auctionCreator);
                        m_context.SaveChanges();

                        // setup & add - Auction
                        Auction newAuction = SetupNewAuction(request, loggedInUserId, auctionCreator.AuctionCreatorId);
                        m_context.Auctions.Add(newAuction);
                        m_context.SaveChanges();

                        // setup & add - Auction Item
                        AuctionItem newAuctionItem = SetupNewAuctionItem(newAuction, loggedInUserId);
                        m_context.AuctionItems.Add(newAuctionItem);
                        m_context.SaveChanges();

                        // setup & add - Auction Item Details
                        AuctionDetails itemAuctionDetails = SetupNewItemAuctionDetails(request, newAuctionItem, loggedInUserId);
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

        public bool CreatePropertyAuction(AddAuctionRequestModel request, int loggedInUserId)
        {
            var strategy = m_context.Database.CreateExecutionStrategy();
            strategy.Execute(() =>
            {
                try
                {
                    using (var transaction = m_context.Database.BeginTransaction())
                    {
                        // setup & add - Auction Creator
                        AuctionCreator auctionCreator = SetupNewAuctionCreator(request, loggedInUserId);
                        m_context.AuctionCreators.Add(auctionCreator);
                        m_context.SaveChanges();

                        // setup & add - Auction
                        Auction newAuction = SetupNewAuction(request, loggedInUserId, auctionCreator.AuctionCreatorId);
                        m_context.Auctions.Add(newAuction);
                        m_context.SaveChanges();

                        // setup & add - Auction Item
                        AuctionItem newAuctionItem = SetupNewAuctionItem(newAuction, loggedInUserId);
                        m_context.AuctionItems.Add(newAuctionItem);
                        m_context.SaveChanges();

                        // setup & add - Auction Item Details
                        AuctionDetails itemAuctionDetails = SetupNewPropertyAuctionDetails(request, newAuctionItem, loggedInUserId);
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

        public bool CreateVehicleAuction(AddAuctionRequestModel request, int loggedInUserId)
        {
            var strategy = m_context.Database.CreateExecutionStrategy();
            strategy.Execute(() =>
            {
                try
                {
                    using (var transaction = m_context.Database.BeginTransaction())
                    {
                        // setup & add - Auction Creator
                        AuctionCreator auctionCreator = SetupNewAuctionCreator(request, loggedInUserId);
                        m_context.AuctionCreators.Add(auctionCreator);
                        m_context.SaveChanges();

                        // setup & add - Auction
                        Auction newAuction = SetupNewAuction(request, loggedInUserId, auctionCreator.AuctionCreatorId);
                        m_context.Auctions.Add(newAuction);
                        m_context.SaveChanges();

                        // setup & add - Auction Item
                        AuctionItem newAuctionItem = SetupNewAuctionItem(newAuction, loggedInUserId);
                        m_context.AuctionItems.Add(newAuctionItem);
                        m_context.SaveChanges();

                        // setup & add - Auction Item Details
                        AuctionDetails itemAuctionDetails = SetupNewVehicleAuctionDetails(request, newAuctionItem, loggedInUserId);
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

        public AuctionEditDetailsResponseModel EditDetails(int auctionId)
        {
            // check if even auction exists and only then do the full join
            bool auctionExists = m_context.Auctions.Any(auct => auct.AuctionId == auctionId);

            if (auctionExists)
            {
                return (from auct in m_context.Auctions
                        where auct.AuctionId == auctionId
                        select new AuctionEditDetailsResponseModel
                        {
                            Auction = new AboutAuctionEditDetailsModel
                            {
                                AuctionName = auct.Name,
                                AuctionStartingPrice = auct.StartingPrice,
                                AuctionStartDate = auct.StartDate,
                                AuctionApplyTillDate = auct.ApplyTillDate,
                                AuctionEndDate = auct.EndDate,
                                AuctionStatusId = auct.AuctionStatusId
                            }
                        }).FirstOrDefault();
            }
            else
            {
                throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.IncorrectAuction);
            }
        }

        public bool UpdateAuctionDetails(AuctionEditRequestModel request, int loggedInUserId)
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
                            auctionForUpdate.ApplyTillDate = request.AuctionApplyTillDate;
                            auctionForUpdate.EndDate = request.AuctionEndDate;
                            // todo: kke: add missing status change here!

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
                        foreach (int auctionId in request.AuctionIds)
                        {
                            bool auctionExists = m_context.Auctions.Any(auct => auct.AuctionId == auctionId);

                            if (auctionExists)
                            {
                                Auction auctionForDelete = m_context.Auctions.Where(auct => auct.AuctionId == auctionId).FirstOrDefault();
                                auctionForDelete.Deleted = true;
                                auctionForDelete.LastUpdatedAt = DateTime.UtcNow;
                                auctionForDelete.LastUpdatedBy = loggedInUserId;

                                AuctionItem auctionItemForDelete = m_context.AuctionItems.Where(aitem => aitem.AuctionId == auctionId).FirstOrDefault();
                                auctionItemForDelete.Deleted = true;
                                auctionItemForDelete.LastUpdatedAt = DateTime.UtcNow;
                                auctionItemForDelete.LastUpdatedBy = loggedInUserId;

                                AuctionDetails auctionDetailsForDelete = m_context.AuctionDetails.Where(adet => adet.AuctionItemId == auctionItemForDelete.AuctionItemId).FirstOrDefault();
                                auctionDetailsForDelete.Deleted = true;
                                auctionDetailsForDelete.LastUpdatedAt = DateTime.UtcNow;
                                auctionDetailsForDelete.LastUpdatedBy = loggedInUserId;

                                m_context.SaveChanges();
                            }
                            else
                            {
                                throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.NotActiveAuction);
                            }
                        }

                        transaction.Commit();
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

        private Auction SetupNewAuction(AddAuctionRequestModel request, int loggedInUserId, int auctionCreatorId)
        {
            int defaultAuctionStatusId = ValidateAndFetchAuctionStatus();

            return new Auction()
            {
                Name = request.AboutAuction.AuctionName,
                StartingPrice = request.AboutAuction.AuctionStartingPrice,
                StartDate = request.AboutAuction.AuctionStartDate ?? null,
                ApplyTillDate = request.AboutAuction.AuctionApplyTillDate,
                EndDate = request.AboutAuction.AuctionEndDate,
                AuctionCategoryId = request.AboutAuction.AuctionTopCategoryId,
                AuctionTypeId = request.AboutAuction.AuctionSubCategoryId ?? request.AboutAuction.AuctionSubCategoryId.Value,
                AuctionStatusId = defaultAuctionStatusId,
                AuctionFormatId = request.AboutAuction.AuctionFormatId,
                AuctionCreatorId = auctionCreatorId,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = loggedInUserId,
                LastUpdatedAt = DateTime.UtcNow,
                LastUpdatedBy = loggedInUserId
            };
        }

        private AuctionItem SetupNewAuctionItem(Auction newAuction, int loggedInUserId)
        {
            return new AuctionItem()
            {
                Name = newAuction.Name,
                AuctionId = newAuction.AuctionId,
                AuctionItemCategoryId = newAuction.AuctionCategoryId,
                AuctionItemTypeId = newAuction.AuctionTypeId,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = loggedInUserId,
                LastUpdatedAt = DateTime.UtcNow,
                LastUpdatedBy = loggedInUserId
            };
        }

        private AuctionDetails SetupNewVehicleAuctionDetails(AddAuctionRequestModel request, AuctionItem newAuctionItem, int loggedInUserId)
        {
            return new AuctionDetails()
            {
                AuctionItemId = newAuctionItem.AuctionItemId,
                Make = request.VehicleAuction.VehicleMake,
                Model = request.VehicleAuction.VehicleModel,
                ManufacturingYear = request.VehicleAuction.VehicleManufacturingYear,
                RegistrationNumber = request.VehicleAuction.VehicleRegistrationNumber,
                IdentificationNumber = request.VehicleAuction.VehicleIdentificationNumber,
                InspectionActive = request.VehicleAuction.VehicleInspectionActive,
                TransmissionId = request.VehicleAuction.VehicleTransmissionId,
                FuelTypeId = request.VehicleAuction.VehicleFuelTypeId,
                EngineSize = request.VehicleAuction.VehicleEngineSize,
                Axis = request.VehicleAuction.VehicleAxis,
                Evaluation = request.VehicleAuction.VehicleEvaluation,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = loggedInUserId,
                LastUpdatedAt = DateTime.UtcNow,
                LastUpdatedBy = loggedInUserId
            };
        }

        private AuctionDetails SetupNewItemAuctionDetails(AddAuctionRequestModel request, AuctionItem newAuctionItem, int loggedInUserId)
        {
            return new AuctionDetails()
            {
                AuctionItemId = newAuctionItem.AuctionItemId,
                Model = request.ItemAuction.ItemModel,
                ManufacturingYear = request.ItemAuction.ItemManufacturingYear,
                ConditionId = request.ItemAuction.ItemConditionId,
                Evaluation = request.ItemAuction.ItemEvaluation,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = loggedInUserId,
                LastUpdatedAt = DateTime.UtcNow,
                LastUpdatedBy = loggedInUserId
            };
        }

        private AuctionCreator SetupNewAuctionCreator(AddAuctionRequestModel request, int loggedInUserId)
        {
            return new AuctionCreator()
            {
                Name = request.AboutAuctionCreator.AuctionCreatorName,
                ContactEmail = request.AboutAuctionCreator.AuctionCreatorEmail,
                ContactPhone = request.AboutAuctionCreator.AuctionCreatorPhone,
                ContactAddress = request.AboutAuctionCreator.AuctionCreatorAddress,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = loggedInUserId,
                LastUpdatedAt = DateTime.UtcNow,
                LastUpdatedBy = loggedInUserId
            };
        }

        private AuctionDetails SetupNewPropertyAuctionDetails(AddAuctionRequestModel request, AuctionItem newAuctionItem, int loggedInUserId)
        {
            return new AuctionDetails()
            {
                AuctionItemId = newAuctionItem.AuctionItemId,
                Coordinates = request.PropertyAuction.PropertyCoordinates,
                RegionId = request.PropertyAuction.PropertyRegionId,
                CadastreNumber = request.PropertyAuction.PropertyCadastreNumber,
                MeasurementValue = request.PropertyAuction.PropertyMeasurementValue,
                MeasurementTypeId = request.PropertyAuction.PropertyMeasurementTypeId,
                Address = request.PropertyAuction.PropertyAddress,
                FloorCount = request.PropertyAuction.PropertyFloorCount,
                RoomCount = request.PropertyAuction.PropertyRoomCount,
                Evaluation = request.PropertyAuction.PropertyEvaluation,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = loggedInUserId,
                LastUpdatedAt = DateTime.UtcNow,
                LastUpdatedBy = loggedInUserId
            };
        }

        private string LoadAuctionFormatName(int auctionFormatId)
        {
            return m_context.AuctionFormats.FirstOrDefault(form => form.AuctionFormatId == auctionFormatId).Name;
        }

        private string LoadVehicleTransmissionName(int transmissionId)
        {
            return m_context.VehicleTransmissions.FirstOrDefault(vtra => vtra.VehicleTransmissionId == transmissionId).Name;
        }

        private string LoadVehicleFuelTypeName(int fuelTypeId)
        {
            return m_context.VehicleFuelTypes.FirstOrDefault(vfue => vfue.VehicleFuelTypeId == fuelTypeId).Name;
        }

        private string LoadItemConditionName(int conditionId)
        {
            return m_context.ItemConditions.FirstOrDefault(icon => icon.ItemConditionId == conditionId).Name;
        }

        private string LoadPropertyMeasurementTypeName(int measurementTypeId)
        {
            return m_context.PropertyMeasurementTypes.FirstOrDefault(pmea => pmea.PropertyMeasurementTypeId == measurementTypeId).Name;
        }

        private string LoadPropertyRegionName(int regionId)
        {
            return m_context.Regions.FirstOrDefault(preg => preg.RegionId == regionId).Name;
        }

        private AuctionDetailsResponseModel SetupVehicleAuctionDetails(AuctionDetailsModel details)
        {
            string auctionFormatName = LoadAuctionFormatName(details.Auction.AuctionFormatId);
            string transmissionName = details.AuctionDetails.TransmissionId.IsNotSpecified() ? null : LoadVehicleTransmissionName(details.AuctionDetails.TransmissionId.Value);
            string fuelTypeName = details.AuctionDetails.FuelTypeId.IsNotSpecified() ? null : LoadVehicleFuelTypeName(details.AuctionDetails.FuelTypeId.Value);

            return new AuctionDetailsResponseModel
            {
                AboutAuctionDetails = new AboutAuctionDetailsModel
                {
                    AuctionName = details.Auction.Name,
                    AuctionStartingPrice = details.Auction.StartingPrice,
                    AuctionStartDate = details.Auction.StartDate,
                    AuctionApplyTillDate = details.Auction.ApplyTillDate,
                    AuctionEndDate = details.Auction.EndDate,
                    AuctionFormat = auctionFormatName
                },
                VehicleAuction = new VehicleAuctionDetailsModel
                {
                    VehicleMake = details.AuctionDetails.Make,
                    VehicleModel = details.AuctionDetails.Model,
                    VehicleManufacturingYear = details.AuctionDetails.ManufacturingYear.Value,
                    VehicleRegistrationNumber = details.AuctionDetails.RegistrationNumber,
                    VehicleIdentificationNumber = details.AuctionDetails.IdentificationNumber,
                    VehicleInspectionActive = details.AuctionDetails.InspectionActive.HasValue ? "Ir" : "Nav",
                    VehicleTransmissionName = transmissionName,
                    VehicleFuelType = fuelTypeName,
                    VehicleEngineSize = details.AuctionDetails.EngineSize,
                    VehicleAxis = details.AuctionDetails.Axis,
                    VehicleEvaluation = details.AuctionDetails.Evaluation
                },
                AboutAuctionCreator = new AuctionCreatorDetailsModel
                {
                    AuctionCreatorName = details.AuctionCreator.Name,
                    AuctionCreatorAddress = details.AuctionCreator.ContactAddress,
                    AuctionCreatorEmail = details.AuctionCreator.ContactEmail,
                    AuctionCreatorPhone = details.AuctionCreator.ContactPhone
                }
            };
        }

        private AuctionDetailsResponseModel SetupItemAuctionDetails(AuctionDetailsModel details)
        {
            string auctionFormatName = LoadAuctionFormatName(details.Auction.AuctionFormatId);
            string conditionName = details.AuctionDetails.ConditionId.IsNotSpecified() ? null : LoadItemConditionName(details.AuctionDetails.ConditionId.Value);

            return new AuctionDetailsResponseModel
            {
                AboutAuctionDetails = new AboutAuctionDetailsModel
                {
                    AuctionName = details.Auction.Name,
                    AuctionStartingPrice = details.Auction.StartingPrice,
                    AuctionStartDate = details.Auction.StartDate,
                    AuctionApplyTillDate = details.Auction.ApplyTillDate,
                    AuctionEndDate = details.Auction.EndDate,
                    AuctionFormat = auctionFormatName
                },
                ItemAuction = new ItemAuctionDetailsModel
                {
                    ItemModel = details.AuctionDetails.Model,
                    ItemManufacturingYear = details.AuctionDetails.ManufacturingYear.Value,
                    ItemConditionName = conditionName,
                    ItemEvaluation = details.AuctionDetails.Evaluation,
                    ItemStartingPrice = details.Auction.StartingPrice
                },
                AboutAuctionCreator = new AuctionCreatorDetailsModel
                {
                    AuctionCreatorName = details.AuctionCreator.Name,
                    AuctionCreatorAddress = details.AuctionCreator.ContactAddress,
                    AuctionCreatorEmail = details.AuctionCreator.ContactEmail,
                    AuctionCreatorPhone = details.AuctionCreator.ContactPhone
                }
            };
        }

        private object HandleNull<T>(T value)
        {
            if (value == null)
                return DBNull.Value;
            return value;
        }

        private AuctionDetailsResponseModel SetupPropertyAuctionDetails(AuctionDetailsModel details)
        {
            string auctionFormatName = LoadAuctionFormatName(details.Auction.AuctionFormatId);
            string measurementTypeName = details.AuctionDetails.MeasurementTypeId.IsNotSpecified() ? null : LoadPropertyMeasurementTypeName(details.AuctionDetails.MeasurementTypeId.Value);
            string regionName = details.AuctionDetails.RegionId.IsNotSpecified() ? null : LoadPropertyRegionName(details.AuctionDetails.RegionId.Value);

            return new AuctionDetailsResponseModel
            {
                AboutAuctionDetails = new AboutAuctionDetailsModel
                {
                    AuctionName = details.Auction.Name,
                    AuctionStartingPrice = details.Auction.StartingPrice,
                    AuctionStartDate = details.Auction.StartDate,
                    AuctionApplyTillDate = details.Auction.ApplyTillDate,
                    AuctionEndDate = details.Auction.EndDate,
                    AuctionFormat = auctionFormatName
                },
                PropertyAuction = new PropertyAuctionDetailsModel
                {
                    PropertyCoordinates = details.AuctionDetails.Coordinates,
                    PropertyRegionName = regionName,
                    PropertyCadastreNumber = details.AuctionDetails.CadastreNumber.Value,
                    PropertyMeasurementValue = details.AuctionDetails.MeasurementValue.Value,
                    PropertyMeasurementTypeName = measurementTypeName,
                    PropertyAddress = details.AuctionDetails.Address,
                    PropertyFloorCount = details.AuctionDetails.FloorCount ?? null,
                    PropertyRoomCount = details.AuctionDetails.RoomCount ?? null,
                    PropertyEvaluation = details.AuctionDetails.Evaluation
                },
                AboutAuctionCreator = new AuctionCreatorDetailsModel
                {
                    AuctionCreatorName = details.AuctionCreator.Name,
                    AuctionCreatorAddress = details.AuctionCreator.ContactAddress,
                    AuctionCreatorEmail = details.AuctionCreator.ContactEmail,
                    AuctionCreatorPhone = details.AuctionCreator.ContactPhone
                }
            };
        }
    }
}
