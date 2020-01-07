using Bidding.Models.Contexts;
using Bidding.Models.DatabaseModels.Auctions;
using Bidding.Models.ViewModels.Auctions.Add;
using Bidding.Models.ViewModels.Auctions.Delete;
using Bidding.Models.ViewModels.Auctions.Details;
using Bidding.Models.ViewModels.Auctions.Edit;
using Bidding.Models.ViewModels.Auctions.List;
using Bidding.Models.ViewModels.Auctions.Shared;
using Bidding.Models.ViewModels.Filters;
using Bidding.Models.ViewModels.Shared.Categories;
using Bidding.Models.ViewModels.Shared.Types;
using Bidding.Shared.Constants;
using Bidding.Shared.ErrorHandling.Errors;
using Bidding.Shared.Exceptions;
using Bidding.Shared.Utility.Validation.Comparers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Bidding.Repositories.Auctions
{
    public class AuctionsRepository
    {
        private readonly BiddingContext m_context;
        private readonly IConfiguration m_configuration;
        private readonly string m_azureStorageConnectionString;

        public AuctionsRepository(BiddingContext context, IConfiguration configuration)
        {
            m_configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            m_azureStorageConnectionString = m_configuration["AzureStorage:ConnectionString"];
            m_context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IEnumerable<AuctionListItemModel> GetActiveAuctions(AuctionListRequestModel request, int startFrom, int endAt, DateTime auctionsFromDate)
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

                SqlParameter loadFromDate = new SqlParameter
                {
                    ParameterName = "fromDate",
                    Direction = ParameterDirection.Input,
                    Value = auctionsFromDate,
                    SqlDbType = SqlDbType.DateTime
                };

                return m_context.Query<AuctionListItemModel>()
                    .FromSql("[dbo].[BID_GetAuctions] @selectedCategories, @selectedTypes, @start, @end, @searchValue, @fromDate", categoryIds, typeIds, startPaginationFrom, endPaginationAt, searchBy, loadFromDate);
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
        public IEnumerable<Auction> ActiveAuctionCount()
        {
            return m_context.Auctions.Where(auct => auct.EndDate >= DateTime.Now.Date);
        }

        /// <summary>
        /// Gets total count of ALL possible auctions
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Auction> AllAuctionCount()
        {
            return m_context.Auctions;
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
        /// Loads all active auction formats
        /// </summary>
        /// <returns></returns>
        public IEnumerable<AuctionFormatItemModel> Formats()
        {
            return m_context.AuctionFormats
                .Select(afor => new AuctionFormatItemModel { AuctionFormatId = afor.AuctionFormatId, AuctionFormatName = afor.Name });
        }

        /// <summary>
        /// Loads all active auction statuses
        /// </summary>
        /// <returns></returns>
        public IEnumerable<AuctionStatusItemModel> Statuses()
        {
            return m_context.AuctionStatuses
                .Select(asta => new AuctionStatusItemModel { AuctionStatusId = asta.AuctionStatusId, AuctionStatusName = asta.Name });
        }

        public async Task<AuctionDetailsResponseModel> DetailsAsync(AuctionDetailsRequestModel request)
        {
            // check if auction exists and only then do the full join
            bool auctionExists = await m_context.Auctions.AnyAsync(auct => auct.AuctionId == request.AuctionId).ConfigureAwait(true);

            if (auctionExists)
            {
                AuctionDetailsModel details = await (
                    from auct in m_context.Auctions
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
                    }
                ).FirstOrDefaultAsync().ConfigureAwait(true);

                if (details.Auction.AuctionCategoryId == AuctionCategories.Item)
                {
                    return await SetupItemAuctionDetailsAsync(details, request).ConfigureAwait(true);
                }

                if (details.Auction.AuctionCategoryId == AuctionCategories.Vehicle)
                {
                    return await SetupVehicleAuctionDetailsAsync(details, request).ConfigureAwait(true);
                }

                if (details.Auction.AuctionCategoryId == AuctionCategories.Property)
                {
                    return await SetupPropertyAuctionDetailsAsync(details, request).ConfigureAwait(true);
                }

                throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.MissingAuctionsInformation);
            }
            else
            {
                throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.IncorrectAuction);
            }
        }

        public int CreateAuction(AddAuctionRequestModel request, int loggedInUserId)
        {
            // if the new auction id(result) is 0, something is wrong with the auction information!
            int newAuctionId = 0;

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
                        EntityEntry<Auction> auction = m_context.Auctions.Add(newAuction);
                        m_context.SaveChanges();

                        // setup & add - Auction Item
                        AuctionItem newAuctionItem = SetupNewAuctionItem(newAuction, loggedInUserId);
                        m_context.AuctionItems.Add(newAuctionItem);
                        m_context.SaveChanges();

                        // setup & add - Auction Item Details
                        if (request.AboutAuction.AuctionTopCategoryId == AuctionCategories.Item) SetItemAuctionDetails(request, loggedInUserId, newAuctionItem);
                        if (request.AboutAuction.AuctionTopCategoryId == AuctionCategories.Vehicle) SetVehicleAuctionDetails(request, loggedInUserId, newAuctionItem);
                        if (request.AboutAuction.AuctionTopCategoryId == AuctionCategories.Property) SetPropertyAuctionDetails(request, loggedInUserId, newAuctionItem);

                        newAuctionId = auction.Entity.AuctionId;

                        m_context.SaveChanges();
                        transaction.Commit();

                        return newAuctionId;
                    }
                }
                catch (Exception ex)
                {
                    throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.CouldNotCreateAuction, ex);
                }
            });

            return newAuctionId;
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

        public async Task<bool> DeleteAsync(AuctionDeleteRequestModel request, int loggedInUserId)
        {
            var strategy = m_context.Database.CreateExecutionStrategy();
            await strategy.Execute(async () =>
            {
                try
                {
                    using (var transaction = m_context.Database.BeginTransaction())
                    {
                        foreach (int auctionId in request.AuctionIds)
                        {
                            bool auctionExists = await m_context.Auctions.AnyAsync(auct => auct.AuctionId == auctionId).ConfigureAwait(true);

                            if (auctionExists)
                            {
                                Auction auctionForDelete = await m_context.Auctions
                                    .Where(auct => auct.AuctionId == auctionId)
                                    .FirstOrDefaultAsync()
                                    .ConfigureAwait(true);

                                if (auctionForDelete.AuctionImageContainer.IsSpecified())
                                {
                                    await HandleAuctionDeleteImages(auctionForDelete.AuctionImageContainer).ConfigureAwait(true);
                                }

                                AuctionItem auctionItemDetails = await m_context.AuctionItems
                                    .Where(aitem => aitem.AuctionId == auctionId)
                                    .FirstOrDefaultAsync()
                                    .ConfigureAwait(true);

                                AuctionDetails auctionDetails = await m_context.AuctionDetails
                                    .Where(adet => adet.AuctionItemId == auctionItemDetails.AuctionItemId)
                                    .FirstOrDefaultAsync()
                                    .ConfigureAwait(true);

                                m_context.Remove(auctionDetails);
                                m_context.Remove(auctionItemDetails);
                                m_context.Remove(auctionForDelete);

                                await m_context.SaveChangesAsync().ConfigureAwait(true);
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
            }).ConfigureAwait(true);

            return true;
        }

        private async Task HandleAuctionDeleteImages(string imageContainerName)
        {
            if (CloudStorageAccount.TryParse(m_azureStorageConnectionString, out CloudStorageAccount cloudStorageAccount))
            {
                try
                {
                    CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
                    CloudBlobContainer container = cloudBlobClient.GetContainerReference(imageContainerName);
                    await container.DeleteIfExistsAsync().ConfigureAwait(true);
                }
                catch (StorageException ex)
                {
                    throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.CouldNotDeleteAuction, ex);
                }
            }
            else
            {
                throw new WebApiException(HttpStatusCode.InternalServerError, FileUploadErrorMessage.GenericUploadErrorMessage);
            }
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
                StartingPrice = ConvertStringToDecimal(request.AboutAuction.AuctionStartingPrice),
                ValueAddedTax = request.AboutAuction.AuctionValueAddedTax,
                StartDate = request.AboutAuction.AuctionStartDate ?? null,
                ApplyTillDate = request.AboutAuction.AuctionApplyTillDate,
                EndDate = request.AboutAuction.AuctionEndDate,
                AuctionCategoryId = request.AboutAuction.AuctionTopCategoryId,
                AuctionTypeId = request.AboutAuction.AuctionSubCategoryId ?? request.AboutAuction.AuctionSubCategoryId.Value,
                AuctionStatusId = defaultAuctionStatusId,
                AuctionFormatId = request.AboutAuction.AuctionFormatId,
                AuctionCreatorId = auctionCreatorId,
                AuctionExternalWebsite = request.AboutAuctionCreator.AuctionExternalWebsite, // NOTE: KKE: No idea if this is correct placement!
                Requirements = request.AboutAuctionCreator.AuctionRequirements,
                CreatedAt = DateTime.UtcNow,
                // CreatedBy = loggedInUserId,
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
                // CreatedBy = loggedInUserId,
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
                DimensionValue = request.VehicleAuction.VehicleDimensionValue,
                DimensionTypeId = request.VehicleAuction.VehicleDimensionType,
                Evaluation = request.VehicleAuction.VehicleEvaluation,
                CreatedAt = DateTime.UtcNow,
                // CreatedBy = loggedInUserId,
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
                Volume = request.ItemAuction.ItemVolume,
                CompanyTypeId = request.ItemAuction.ItemCompanyTypeId,
                Evaluation = request.ItemAuction.ItemEvaluation,
                CreatedAt = DateTime.UtcNow,
                // CreatedBy = loggedInUserId,
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
                // CreatedBy = loggedInUserId,
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
                MeasurementValue = ConvertStringToDecimal(request.PropertyAuction.PropertyMeasurementValue),
                MeasurementTypeId = request.PropertyAuction.PropertyMeasurementTypeId,
                Address = request.PropertyAuction.PropertyAddress,
                FloorCount = request.PropertyAuction.PropertyFloorCount,
                RoomCount = request.PropertyAuction.PropertyRoomCount,
                Evaluation = request.PropertyAuction.PropertyEvaluation,
                CreatedAt = DateTime.UtcNow,
                // CreatedBy = loggedInUserId,
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

        private string LoadVehicleDimensionTypeName(int dimensionTypeId)
        {
            return m_context.VehicleDimensionTypes.FirstOrDefault(vdim => vdim.VehicleDimensionTypeId == dimensionTypeId).Name;
        }

        private string LoadItemConditionName(int conditionId)
        {
            return m_context.ItemConditions.FirstOrDefault(icon => icon.ItemConditionId == conditionId).Name;
        }

        private string LoadCompanyTypeName(int typeId)
        {
            return m_context.ItemCompanyTypes.FirstOrDefault(icty => icty.ItemCompanyTypeId == typeId).Name;
        }

        private string LoadPropertyMeasurementTypeName(int measurementTypeId)
        {
            return m_context.PropertyMeasurementTypes.FirstOrDefault(pmea => pmea.PropertyMeasurementTypeId == measurementTypeId).Name;
        }

        private string LoadPropertyRegionName(int regionId)
        {
            return m_context.Regions.FirstOrDefault(preg => preg.RegionId == regionId).Name;
        }

        private async Task<AuctionDetailsResponseModel> SetupVehicleAuctionDetailsAsync(AuctionDetailsModel details, AuctionDetailsRequestModel request)
        {
            string auctionFormatName = LoadAuctionFormatName(details.Auction.AuctionFormatId);
            string transmissionName = details.AuctionDetails.TransmissionId.IsNotSpecified() ? null : LoadVehicleTransmissionName(details.AuctionDetails.TransmissionId.Value);
            string fuelTypeName = details.AuctionDetails.FuelTypeId.IsNotSpecified() ? null : LoadVehicleFuelTypeName(details.AuctionDetails.FuelTypeId.Value);
            string dimensionName = details.AuctionDetails.DimensionTypeId.IsNotSpecified() ? null : LoadVehicleDimensionTypeName(details.AuctionDetails.DimensionTypeId.Value);
            string inspectionActive = details.AuctionDetails.InspectionActive.HasValue && details.AuctionDetails.InspectionActive.Value ? "Ir" : "Nav";


            Tuple<List<string>, List<AuctionDocumentModel>> auctionFiles = await LoadAuctionFilesAsync(request.AuctionId).ConfigureAwait(true);

            return new AuctionDetailsResponseModel
            {
                AboutAuctionDetails = SetAboutAuctionDetails(details, auctionFormatName, auctionFiles),
                VehicleAuction = new VehicleAuctionDetailsModel
                {
                    VehicleMake = details.AuctionDetails.Make,
                    VehicleModel = details.AuctionDetails.Model,
                    VehicleManufacturingYear = details.AuctionDetails.ManufacturingYear.Value,
                    VehicleRegistrationNumber = details.AuctionDetails.RegistrationNumber,
                    VehicleIdentificationNumber = details.AuctionDetails.IdentificationNumber,
                    VehicleInspectionActive = inspectionActive,
                    VehicleTransmissionName = transmissionName,
                    VehicleFuelType = fuelTypeName,
                    VehicleDimensionValue = details.AuctionDetails.DimensionValue,
                    VehicleDimensionType = dimensionName,
                    VehicleEngineSize = details.AuctionDetails.EngineSize,
                    VehicleAxis = details.AuctionDetails.Axis
                },
                AboutAuctionCreator = SetAuctionCreatorDetails(details)
            };
        }

        private async Task<AuctionDetailsResponseModel> SetupItemAuctionDetailsAsync(AuctionDetailsModel details, AuctionDetailsRequestModel request)
        {
            string auctionFormatName = LoadAuctionFormatName(details.Auction.AuctionFormatId);
            string conditionName = details.AuctionDetails.ConditionId.IsNotSpecified() ? null : LoadItemConditionName(details.AuctionDetails.ConditionId.Value);
            string companyTypeName = details.AuctionDetails.CompanyTypeId.IsNotSpecified() ? null : LoadCompanyTypeName(details.AuctionDetails.CompanyTypeId.Value);

            Tuple<List<string>, List<AuctionDocumentModel>> auctionFiles = await LoadAuctionFilesAsync(request.AuctionId).ConfigureAwait(true);

            return new AuctionDetailsResponseModel
            {
                AboutAuctionDetails = SetAboutAuctionDetails(details, auctionFormatName, auctionFiles),
                ItemAuction = new ItemAuctionDetailsModel
                {
                    ItemModel = details.AuctionDetails.Model,
                    ItemManufacturingYear = details.AuctionDetails.ManufacturingYear ?? null,
                    ItemConditionName = conditionName,
                    ItemStartingPrice = details.Auction.StartingPrice,
                    ItemVolume = details.AuctionDetails.Volume,
                    ItemCompanyType = companyTypeName
                },
                AboutAuctionCreator = SetAuctionCreatorDetails(details)
            };
        }

        private async Task<AuctionDetailsResponseModel> SetupPropertyAuctionDetailsAsync(AuctionDetailsModel details, AuctionDetailsRequestModel request)
        {
            string auctionFormatName = LoadAuctionFormatName(details.Auction.AuctionFormatId);
            string measurementTypeName = details.AuctionDetails.MeasurementTypeId.IsNotSpecified() ? null : LoadPropertyMeasurementTypeName(details.AuctionDetails.MeasurementTypeId.Value);
            string regionName = details.AuctionDetails.RegionId.IsNotSpecified() ? null : LoadPropertyRegionName(details.AuctionDetails.RegionId.Value);

            Tuple<List<string>, List<AuctionDocumentModel>> auctionFiles = await LoadAuctionFilesAsync(request.AuctionId).ConfigureAwait(true);

            return new AuctionDetailsResponseModel
            {
                AboutAuctionDetails = SetAboutAuctionDetails(details, auctionFormatName, auctionFiles),
                PropertyAuction = new PropertyAuctionDetailsModel
                {
                    PropertyCoordinates = details.AuctionDetails.Coordinates,
                    PropertyRegionName = regionName,
                    PropertyCadastreNumber = details.AuctionDetails.CadastreNumber.Value,
                    PropertyMeasurementValue = details.AuctionDetails.MeasurementValue.Value,
                    PropertyMeasurementTypeName = measurementTypeName,
                    PropertyAddress = details.AuctionDetails.Address,
                    PropertyFloorCount = details.AuctionDetails.FloorCount ?? null,
                    PropertyRoomCount = details.AuctionDetails.RoomCount ?? null
                },
                AboutAuctionCreator = SetAuctionCreatorDetails(details)
            };
        }

        private AboutAuctionDetailsModel SetAboutAuctionDetails(AuctionDetailsModel details, string auctionFormatName, Tuple<List<string>, List<AuctionDocumentModel>> auctionFiles)
        {
            return new AboutAuctionDetailsModel
            {
                AuctionName = details.Auction.Name,
                AuctionStartingPrice = details.Auction.StartingPrice,
                AuctionValueAddedTax = details.Auction.ValueAddedTax ? "Ir" : "Nav",
                AuctionStartDate = details.Auction.StartDate,
                AuctionApplyTillDate = details.Auction.ApplyTillDate,
                AuctionEndDate = details.Auction.EndDate,
                AuctionFormat = auctionFormatName,
                AuctionExternalWebsite = details.Auction.AuctionExternalWebsite,
                ItemEvaluation = details.AuctionDetails.Evaluation,
                AuctionImageUrls = auctionFiles.Item1,
                AuctionDocuments = auctionFiles.Item2
            };
        }

        private AuctionCreatorDetailsModel SetAuctionCreatorDetails(AuctionDetailsModel details)
        {
            return new AuctionCreatorDetailsModel
            {
                AuctionCreatorName = details.AuctionCreator.Name,
                AuctionCreatorAddress = details.AuctionCreator.ContactAddress,
                AuctionCreatorEmail = details.AuctionCreator.ContactEmail,
                AuctionCreatorPhone = details.AuctionCreator.ContactPhone,
                AuctionRequirements = details.Auction.Requirements
            };
        }

        /// <summary>
        /// Could be made global!
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        private object HandleNull<T>(T value)
        {
            if (value == null)
                return DBNull.Value;
            return value;
        }

        /// <summary>
        /// Could be made global - atm used for currency convert!
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private decimal ConvertStringToDecimal(string item)
        {
            string newItem = item.Replace(',', '.');

            if (decimal.TryParse(newItem, NumberStyles.Currency, CultureInfo.InvariantCulture, out decimal number))
                return number;
            else
                throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.PriceFormatIncorrect);
        }

        private void SetItemAuctionDetails(AddAuctionRequestModel request, int loggedInUserId, AuctionItem newAuctionItem)
        {
            AuctionDetails itemAuctionDetails = SetupNewItemAuctionDetails(request, newAuctionItem, loggedInUserId);
            m_context.AuctionDetails.Add(itemAuctionDetails);
        }

        private void SetPropertyAuctionDetails(AddAuctionRequestModel request, int loggedInUserId, AuctionItem newAuctionItem)
        {
            AuctionDetails itemAuctionDetails = SetupNewPropertyAuctionDetails(request, newAuctionItem, loggedInUserId);
            m_context.AuctionDetails.Add(itemAuctionDetails);
        }

        private void SetVehicleAuctionDetails(AddAuctionRequestModel request, int loggedInUserId, AuctionItem newAuctionItem)
        {
            AuctionDetails itemAuctionDetails = SetupNewVehicleAuctionDetails(request, newAuctionItem, loggedInUserId);
            m_context.AuctionDetails.Add(itemAuctionDetails);
        }

        private async Task<Tuple<List<string>, List<AuctionDocumentModel>>> LoadAuctionFilesAsync(int auctionId)
        {
            var auctionImageUrls = new List<string>();
            var auctionDocumentUrls = new List<AuctionDocumentModel>();

            string imageContainer = await m_context.Auctions
                .Where(auct => auct.AuctionId == auctionId)
                .Select(auct => auct.AuctionImageContainer).SingleOrDefaultAsync().ConfigureAwait(true);

            if (CloudStorageAccount.TryParse(m_azureStorageConnectionString, out CloudStorageAccount cloudStorageAccount))
            {
                OperationContext context = new OperationContext();
                BlobRequestOptions options = new BlobRequestOptions();
                CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
                CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(imageContainer);

                BlobContinuationToken blobContinuationToken = null;

                do
                {
                    BlobResultSegment results = await cloudBlobContainer
                        .ListBlobsSegmentedAsync(null, true, BlobListingDetails.All, null, blobContinuationToken, options, context)
                        .ConfigureAwait(true);

                    blobContinuationToken = results.ContinuationToken;

                    foreach (IListBlobItem item in results.Results)
                    {
                        if (item is CloudBlockBlob blobItem)
                        {
                            if (blobItem.Properties.ContentType == "image/jpeg")
                            {
                                auctionImageUrls.Add(blobItem.Uri.AbsoluteUri);
                            }

                            if (blobItem.Properties.ContentType == "application/pdf")
                            {
                                auctionDocumentUrls.Add(new AuctionDocumentModel()
                                {
                                    DocumentName = blobItem.Name,
                                    DocumentUrl = blobItem.Uri.AbsoluteUri
                                });
                            }
                        }
                    }
                } while (blobContinuationToken != null);
            }
            else
            {
                throw new WebApiException(HttpStatusCode.InternalServerError, AuctionErrorMessages.CouldNotFetchAuctionDetails);
            }

            var result = new Tuple<List<string>, List<AuctionDocumentModel>>(auctionImageUrls, auctionDocumentUrls);
            return await Task.FromResult(result).ConfigureAwait(true);
        }
    }
}
