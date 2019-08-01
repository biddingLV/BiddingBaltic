using Bidding.Database.DatabaseModels.Auctions;
using Bidding.Database.DatabaseModels.Item;
using Bidding.Database.DatabaseModels.Property;
using Bidding.Database.DatabaseModels.Shared;
using Bidding.Database.DatabaseModels.Users;
using Bidding.Database.DatabaseModels.Vehicle;
using Bidding.Models.DatabaseModels;
using Bidding.Models.DatabaseModels.Bidding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Type = Bidding.Models.DatabaseModels.Type;

namespace Bidding.Database.Contexts
{
    public static class ModelBuilderExtensions
    {
        private static DateTime CreatedAtDateTime = DateTime.Parse("01/01/2019");
        private static int CreatedByBiddingAdmin = 1;

        public static void Seed(this ModelBuilder modelBuilder)
        {
            PopulateRoles(modelBuilder);
            PopulateUsers(modelBuilder);
            PopulateAuctionStatuses(modelBuilder);
            PopulateVehicleTransmissions(modelBuilder);
            PopulateVehicleFuelTypes(modelBuilder);
            PopulateItemConditions(modelBuilder);
            PopulatePropertyMeasurementTypes(modelBuilder);
            PopulateRegions(modelBuilder);
            PopulateCategories(modelBuilder);
            PopulateTypes(modelBuilder);
            PopulateAuctionFormats(modelBuilder);
            PopulateAuctionConditions(modelBuilder);
            // PopulateAuctions(modelBuilder);
            // PopulateAuctionItems(modelBuilder);
            // PopulateAuctionDetails(modelBuilder);
        }

        private static void PopulateRoles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    RoleId = 1,
                    Name = "User",
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    LastUpdatedAt = CreatedAtDateTime,
                    LastUpdatedBy = CreatedByBiddingAdmin,
                    Deleted = false
                },
                new Role
                {
                    RoleId = 2,
                    Name = "Admin",
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    LastUpdatedAt = CreatedAtDateTime,
                    LastUpdatedBy = CreatedByBiddingAdmin,
                    Deleted = false
                }
            );
        }

        private static void PopulateUsers(ModelBuilder modelBuilder)
        {
            var adminRoleId = 2;
            var userRoleId = 1;

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    FirstName = "Kristaps",
                    LastName = "K",
                    LoginEmail = "kristaps.kerpe@gmail.com",
                    ContactEmail = "kristaps.kerpe@gmail.com",
                    Phone = "22089756",
                    RoleId = adminRoleId,
                    Deleted = false,
                    UniqueIdentifier = "auth0|5bb4fdb000997e5d8c606653",
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    LastUpdatedAt = CreatedAtDateTime,
                    LastUpdatedBy = CreatedByBiddingAdmin
                },
                new User
                {
                    UserId = 2,
                    FirstName = "Zane",
                    LastName = "H",
                    LoginEmail = "zanehaartman@gmail.com",
                    ContactEmail = "zanehaartman@gmail.com",
                    RoleId = adminRoleId,
                    Deleted = false,
                    UniqueIdentifier = "auth0|5bd605d46d76842d17365f3b",
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    LastUpdatedAt = CreatedAtDateTime,
                    LastUpdatedBy = CreatedByBiddingAdmin
                },
                new User
                {
                    UserId = 3,
                    FirstName = "Jānis",
                    LastName = "J",
                    LoginEmail = "j.jaunozols@gmail.com",
                    ContactEmail = "j.jaunozols@gmail.com",
                    RoleId = adminRoleId,
                    Deleted = false,
                    UniqueIdentifier = "uth0|5bba448bb76b8011ae24e569",
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    LastUpdatedAt = CreatedAtDateTime,
                    LastUpdatedBy = CreatedByBiddingAdmin
                },
                new User
                {
                    UserId = 4,
                    FirstName = "Jānis R",
                    LastName = "B",
                    LoginEmail = "janis.rihards.blazevics@gmail.com",
                    ContactEmail = "janis.rihards.blazevics@gmail.com",
                    RoleId = userRoleId,
                    Deleted = true,
                    UniqueIdentifier = "auth0|5c5dae63da9fe9124eccf90f",
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    LastUpdatedAt = CreatedAtDateTime,
                    LastUpdatedBy = CreatedByBiddingAdmin
                }
            );
        }

        private static void PopulateAuctionStatuses(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuctionStatus>().HasData(
                new AuctionStatus
                {
                    AuctionStatusId = 1,
                    Name = "Aktīva",
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    LastUpdatedAt = CreatedAtDateTime,
                    LastUpdatedBy = CreatedByBiddingAdmin,
                    Deleted = false
                },
                new AuctionStatus
                {
                    AuctionStatusId = 2,
                    Name = "Pārtraukta",
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    LastUpdatedAt = CreatedAtDateTime,
                    LastUpdatedBy = CreatedByBiddingAdmin,
                    Deleted = false
                },
                new AuctionStatus
                {
                    AuctionStatusId = 3,
                    Name = "Beigusies",
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    LastUpdatedAt = CreatedAtDateTime,
                    LastUpdatedBy = CreatedByBiddingAdmin,
                    Deleted = false
                }
            );
        }

        private static void PopulateVehicleTransmissions(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VehicleTransmission>().HasData(
                new VehicleTransmission
                {
                    VehicleTransmissionId = 1,
                    Name = "Automatiskā",
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    LastUpdatedAt = CreatedAtDateTime,
                    LastUpdatedBy = CreatedByBiddingAdmin,
                    Deleted = false
                },
                new VehicleTransmission
                {
                    VehicleTransmissionId = 2,
                    Name = "Mehāniskā",
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    LastUpdatedAt = CreatedAtDateTime,
                    LastUpdatedBy = CreatedByBiddingAdmin,
                    Deleted = false
                }
            );
        }

        private static void PopulateVehicleFuelTypes(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VehicleFuelType>().HasData(
                new VehicleFuelType
                {
                    VehicleFuelTypeId = 1,
                    Name = "Benzīns",
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    LastUpdatedAt = CreatedAtDateTime,
                    LastUpdatedBy = CreatedByBiddingAdmin,
                    Deleted = false
                },
                new VehicleFuelType
                {
                    VehicleFuelTypeId = 2,
                    Name = "Dīzelis",
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    LastUpdatedAt = CreatedAtDateTime,
                    LastUpdatedBy = CreatedByBiddingAdmin,
                    Deleted = false
                },
                new VehicleFuelType
                {
                    VehicleFuelTypeId = 3,
                    Name = "Benzīns/Naftas gāze",
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    LastUpdatedAt = CreatedAtDateTime,
                    LastUpdatedBy = CreatedByBiddingAdmin,
                    Deleted = false
                },
                new VehicleFuelType
                {
                    VehicleFuelTypeId = 4,
                    Name = "Elektroniskais",
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    LastUpdatedAt = CreatedAtDateTime,
                    LastUpdatedBy = CreatedByBiddingAdmin,
                    Deleted = false
                },
                new VehicleFuelType
                {
                    VehicleFuelTypeId = 5,
                    Name = "Hibrīds",
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    LastUpdatedAt = CreatedAtDateTime,
                    LastUpdatedBy = CreatedByBiddingAdmin,
                    Deleted = false
                }
            );
        }

        private static void PopulateItemConditions(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ItemCondition>().HasData(
                new ItemCondition
                {
                    ItemConditionId = 1,
                    Name = "Jauns",
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    LastUpdatedAt = CreatedAtDateTime,
                    LastUpdatedBy = CreatedByBiddingAdmin,
                    Deleted = false
                },
                new ItemCondition
                {
                    ItemConditionId = 2,
                    Name = "Lietots",
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    LastUpdatedAt = CreatedAtDateTime,
                    LastUpdatedBy = CreatedByBiddingAdmin,
                    Deleted = false
                }
            );
        }

        private static void PopulatePropertyMeasurementTypes(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PropertyMeasurementType>().HasData(
                new PropertyMeasurementType
                {
                    PropertyMeasurementTypeId = 1,
                    Name = "m2",
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    LastUpdatedAt = CreatedAtDateTime,
                    LastUpdatedBy = CreatedByBiddingAdmin,
                    Deleted = false
                },
                new PropertyMeasurementType
                {
                    PropertyMeasurementTypeId = 2,
                    Name = "a",
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    LastUpdatedAt = CreatedAtDateTime,
                    LastUpdatedBy = CreatedByBiddingAdmin,
                    Deleted = false
                },
                new PropertyMeasurementType
                {
                    PropertyMeasurementTypeId = 3,
                    Name = "ha",
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    LastUpdatedAt = CreatedAtDateTime,
                    LastUpdatedBy = CreatedByBiddingAdmin,
                    Deleted = false
                }
            );
        }

        private static void PopulateCategories(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    CategoryId = 1,
                    Name = "Transports",
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    Deleted = false,
                    LastUpdatedAt = CreatedAtDateTime,
                    LastUpdatedBy = CreatedByBiddingAdmin
                },
                new Category
                {
                    CategoryId = 2,
                    Name = "Manta",
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    Deleted = false,
                    LastUpdatedAt = CreatedAtDateTime,
                    LastUpdatedBy = CreatedByBiddingAdmin
                },
                new Category
                {
                    CategoryId = 3,
                    Name = "Nekustamais īpašums",
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    Deleted = false,
                    LastUpdatedAt = CreatedAtDateTime,
                    LastUpdatedBy = CreatedByBiddingAdmin
                }
            );
        }

        private static void PopulateTypes(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Type>().HasData(
                new Type
                {
                    TypeId = 1,
                    Name = "Vieglais transports līdz 3,5t",
                    AuctionCategoryId = 1,
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    Deleted = false,
                    LastUpdatedAt = CreatedAtDateTime,
                    LastUpdatedBy = CreatedByBiddingAdmin
                },
                new Type
                {
                    TypeId = 2,
                    Name = "Traktortehnika",
                    AuctionCategoryId = 1,
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    Deleted = false,
                    LastUpdatedAt = CreatedAtDateTime,
                    LastUpdatedBy = CreatedByBiddingAdmin
                },
                new Type
                {
                    TypeId = 3,
                    Name = "Kravas auto",
                    AuctionCategoryId = 1,
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    Deleted = false,
                    LastUpdatedAt = CreatedAtDateTime,
                    LastUpdatedBy = CreatedByBiddingAdmin
                },
                new Type
                {
                    TypeId = 4,
                    Name = "Mototehnika",
                    AuctionCategoryId = 1,
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    Deleted = false,
                    LastUpdatedAt = CreatedAtDateTime,
                    LastUpdatedBy = CreatedByBiddingAdmin
                },
                new Type
                {
                    TypeId = 5,
                    Name = "Piekabes",
                    AuctionCategoryId = 1,
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    Deleted = false,
                    LastUpdatedAt = CreatedAtDateTime,
                    LastUpdatedBy = CreatedByBiddingAdmin
                },
                new Type
                {
                    TypeId = 6,
                    Name = "Ūdens transports",
                    AuctionCategoryId = 1,
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    Deleted = false,
                    LastUpdatedAt = CreatedAtDateTime,
                    LastUpdatedBy = CreatedByBiddingAdmin
                },
                new Type
                {
                    TypeId = 7,
                    Name = "Cits transports",
                    AuctionCategoryId = 1,
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    Deleted = false,
                    LastUpdatedAt = CreatedAtDateTime,
                    LastUpdatedBy = CreatedByBiddingAdmin
                },
                new Type
                {
                    TypeId = 8,
                    Name = "Biroja tehnika",
                    AuctionCategoryId = 2,
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    Deleted = false,
                    LastUpdatedAt = CreatedAtDateTime,
                    LastUpdatedBy = CreatedByBiddingAdmin
                },
                new Type
                {
                    TypeId = 9,
                    Name = "Sadzīves tehnika",
                    AuctionCategoryId = 2,
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    Deleted = false,
                    LastUpdatedAt = CreatedAtDateTime,
                    LastUpdatedBy = CreatedByBiddingAdmin
                },
                new Type
                {
                    TypeId = 10,
                    Name = "Instrumenti",
                    AuctionCategoryId = 2,
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    Deleted = false,
                    LastUpdatedAt = CreatedAtDateTime,
                    LastUpdatedBy = CreatedByBiddingAdmin
                },
                new Type
                {
                    TypeId = 11,
                    Name = "Iekārtas",
                    AuctionCategoryId = 2,
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    Deleted = false,
                    LastUpdatedAt = CreatedAtDateTime,
                    LastUpdatedBy = CreatedByBiddingAdmin
                },
                new Type
                {
                    TypeId = 12,
                    Name = "Ražošanas materiāli",
                    AuctionCategoryId = 2,
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    Deleted = false,
                    LastUpdatedAt = CreatedAtDateTime,
                    LastUpdatedBy = CreatedByBiddingAdmin
                },
                new Type
                {
                    TypeId = 13,
                    Name = "Veikala produkcija",
                    AuctionCategoryId = 2,
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    Deleted = false,
                    LastUpdatedAt = CreatedAtDateTime,
                    LastUpdatedBy = CreatedByBiddingAdmin
                },
                new Type
                {
                    TypeId = 14,
                    Name = "Uzņēmums",
                    AuctionCategoryId = 2,
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    Deleted = false,
                    LastUpdatedAt = CreatedAtDateTime,
                    LastUpdatedBy = CreatedByBiddingAdmin
                },
                new Type
                {
                    TypeId = 15,
                    Name = "Domeins",
                    AuctionCategoryId = 2,
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    Deleted = false,
                    LastUpdatedAt = CreatedAtDateTime,
                    LastUpdatedBy = CreatedByBiddingAdmin
                },
                new Type
                {
                    TypeId = 16,
                    Name = "Preču zīme",
                    AuctionCategoryId = 2,
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    Deleted = false,
                    LastUpdatedAt = CreatedAtDateTime,
                    LastUpdatedBy = CreatedByBiddingAdmin
                },
                new Type
                {
                    TypeId = 17,
                    Name = "Sadzīves mēbeles",
                    AuctionCategoryId = 2,
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    Deleted = false,
                    LastUpdatedAt = CreatedAtDateTime,
                    LastUpdatedBy = CreatedByBiddingAdmin
                },
                new Type
                {
                    TypeId = 18,
                    Name = "Biroja mēbeles",
                    AuctionCategoryId = 2,
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    Deleted = false,
                    LastUpdatedAt = CreatedAtDateTime,
                    LastUpdatedBy = CreatedByBiddingAdmin
                },
                new Type
                {
                    TypeId = 19,
                    Name = "Cita manta",
                    AuctionCategoryId = 2,
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    Deleted = false,
                    LastUpdatedAt = CreatedAtDateTime,
                    LastUpdatedBy = CreatedByBiddingAdmin
                },
                new Type
                {
                    TypeId = 20,
                    Name = "Dzīvoklis",
                    AuctionCategoryId = 3,
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    Deleted = false,
                    LastUpdatedAt = CreatedAtDateTime,
                    LastUpdatedBy = CreatedByBiddingAdmin
                },
                new Type
                {
                    TypeId = 21,
                    Name = "Māja",
                    AuctionCategoryId = 3,
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    Deleted = false,
                    LastUpdatedAt = CreatedAtDateTime,
                    LastUpdatedBy = CreatedByBiddingAdmin
                },
                new Type
                {
                    TypeId = 22,
                    Name = "Zeme",
                    AuctionCategoryId = 3,
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    Deleted = false,
                    LastUpdatedAt = CreatedAtDateTime,
                    LastUpdatedBy = CreatedByBiddingAdmin
                },
                new Type
                {
                    TypeId = 23,
                    Name = "Telpa",
                    AuctionCategoryId = 3,
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    Deleted = false,
                    LastUpdatedAt = CreatedAtDateTime,
                    LastUpdatedBy = CreatedByBiddingAdmin
                },
                new Type
                {
                    TypeId = 24,
                    Name = "Garāža",
                    AuctionCategoryId = 3,
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    Deleted = false,
                    LastUpdatedAt = CreatedAtDateTime,
                    LastUpdatedBy = CreatedByBiddingAdmin
                }
            );
        }

        private static void PopulateAuctionFormats(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuctionFormat>().HasData(
                new AuctionFormat
                {
                    AuctionFormatId = 1,
                    Name = "Cenu aptauja",
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    Deleted = false,
                    LastUpdatedAt = CreatedAtDateTime,
                    LastUpdatedBy = CreatedByBiddingAdmin
                },
                new AuctionFormat
                {
                    AuctionFormatId = 2,
                    Name = "Izsole elektroniski",
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    Deleted = false,
                    LastUpdatedAt = CreatedAtDateTime,
                    LastUpdatedBy = CreatedByBiddingAdmin
                },
                new AuctionFormat
                {
                    AuctionFormatId = 3,
                    Name = "Izsole klātienē",
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    Deleted = false,
                    LastUpdatedAt = CreatedAtDateTime,
                    LastUpdatedBy = CreatedByBiddingAdmin
                }
            );
        }

        private static void PopulateAuctionConditions(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuctionCondition>().HasData(
                new AuctionCondition
                {
                    AuctionConditionId = 1,
                    Name = "Lietota",
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    Deleted = false,
                    LastUpdatedAt = CreatedAtDateTime,
                    LastUpdatedBy = CreatedByBiddingAdmin
                },
                new AuctionCondition
                {
                    AuctionConditionId = 2,
                    Name = "Jauna",
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    Deleted = false,
                    LastUpdatedAt = CreatedAtDateTime,
                    LastUpdatedBy = CreatedByBiddingAdmin
                },
                new AuctionCondition
                {
                    AuctionConditionId = 3,
                    Name = "Apdzīvots",
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    Deleted = false,
                    LastUpdatedAt = CreatedAtDateTime,
                    LastUpdatedBy = CreatedByBiddingAdmin
                },
                new AuctionCondition
                {
                    AuctionConditionId = 4,
                    Name = "Neapdzīvots",
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    Deleted = false,
                    LastUpdatedAt = CreatedAtDateTime,
                    LastUpdatedBy = CreatedByBiddingAdmin
                },
                new AuctionCondition
                {
                    AuctionConditionId = 5,
                    Name = "Nepieciešams remonts",
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    Deleted = false,
                    LastUpdatedAt = CreatedAtDateTime,
                    LastUpdatedBy = CreatedByBiddingAdmin
                }
            );
        }

        private static void PopulateRegions(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Region>().HasData(
                new Region
                {
                    RegionId = 1,
                    Name = "Jelgava",
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    Deleted = false,
                    LastUpdatedAt = CreatedAtDateTime,
                    LastUpdatedBy = CreatedByBiddingAdmin
                },
                new Region
                {
                    RegionId = 2,
                    Name = "Ogre",
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    Deleted = false,
                    LastUpdatedAt = CreatedAtDateTime,
                    LastUpdatedBy = CreatedByBiddingAdmin
                }
            );
        }

        private static void PopulateAuctions(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Auction>().HasData(
                new Auction
                {
                    AuctionId = 1,
                    Name = "Tesla Model 3",
                    StartingPrice = 15000,
                    StartDate = CreatedAtDateTime.AddMonths(1),
                    ApplyTillDate = CreatedAtDateTime.AddMonths(5),
                    EndDate = CreatedAtDateTime.AddMonths(12),
                    AuctionCategoryId = 1,
                    AuctionTypeId = 1,
                    AuctionStatusId = 1,
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    Deleted = false,
                    LastUpdatedAt = CreatedAtDateTime,
                    LastUpdatedBy = CreatedByBiddingAdmin
                },
                new Auction
                {
                    AuctionId = 2,
                    Name = "Penthouse tipa dzīvoklis Vecrīgas sirdī",
                    StartingPrice = 50000,
                    StartDate = CreatedAtDateTime.AddMonths(1),
                    ApplyTillDate = CreatedAtDateTime.AddMonths(5),
                    EndDate = CreatedAtDateTime.AddMonths(12),
                    AuctionCategoryId = 3,
                    AuctionTypeId = 13,
                    AuctionStatusId = 1,
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    Deleted = false,
                    LastUpdatedAt = CreatedAtDateTime,
                    LastUpdatedBy = CreatedByBiddingAdmin
                },
                new Auction
                {
                    AuctionId = 3,
                    Name = "Vīna skapis",
                    StartingPrice = 900,
                    StartDate = CreatedAtDateTime.AddMonths(1),
                    ApplyTillDate = CreatedAtDateTime.AddMonths(5),
                    EndDate = CreatedAtDateTime.AddMonths(12),
                    AuctionCategoryId = 2,
                    AuctionTypeId = 12,
                    AuctionStatusId = 1,
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    Deleted = false,
                    LastUpdatedAt = CreatedAtDateTime,
                    LastUpdatedBy = CreatedByBiddingAdmin
                }
            );
        }

        private static void PopulateAuctionItems(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuctionItem>().HasData(
                new AuctionItem
                {
                    AuctionItemId = 1,
                    Name = "Tesla Model 3",
                    AuctionId = 1,
                    AuctionItemCategoryId = 1,
                    AuctionItemTypeId = 1,
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    Deleted = false,
                    LastUpdatedAt = CreatedAtDateTime,
                    LastUpdatedBy = CreatedByBiddingAdmin
                },
                new AuctionItem
                {
                    AuctionItemId = 2,
                    Name = "Penthouse tipa dzīvoklis Vecrīgas sirdī",
                    AuctionId = 2,
                    AuctionItemCategoryId = 3,
                    AuctionItemTypeId = 13,
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    Deleted = false,
                    LastUpdatedAt = CreatedAtDateTime,
                    LastUpdatedBy = CreatedByBiddingAdmin
                },
                new AuctionItem
                {
                    AuctionItemId = 3,
                    Name = "Vīna skapis",
                    AuctionId = 3,
                    AuctionItemCategoryId = 2,
                    AuctionItemTypeId = 12,
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    Deleted = false,
                    LastUpdatedAt = CreatedAtDateTime,
                    LastUpdatedBy = CreatedByBiddingAdmin
                }
            );
        }

        private static void PopulateAuctionDetails(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuctionDetails>().HasData(
                new AuctionDetails
                {
                    AuctionDetailsId = 1,
                    AuctionItemId = 1,
                    Make = "BMW",
                    Model = "1-Series",
                    ManufacturingYear = 2012,
                    RegistrationNumber = "5990144781",
                    IdentificationNumber = "5990144781",
                    InspectionActive = true,
                    TransmissionId = 1,
                    FuelTypeId = 1,
                    EngineSize = "In progress",
                    Axis = "In progress",
                    Evaluation = "In progress",
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    Deleted = false,
                    LastUpdatedAt = CreatedAtDateTime,
                    LastUpdatedBy = CreatedByBiddingAdmin
                }
            );
        }
    }
}
