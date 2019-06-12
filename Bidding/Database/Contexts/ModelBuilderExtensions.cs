using Bidding.Database.DatabaseModels.Auctions;
using Bidding.Database.DatabaseModels.Auctions.Details;
using Bidding.Database.DatabaseModels.Users;
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
            PopulateCategories(modelBuilder);
            PopulateTypes(modelBuilder);
            PopulateAuctionFormats(modelBuilder);
            PopulateAuctionConditions(modelBuilder);
            PopulateAuctions(modelBuilder);
            PopulateAuctionItems(modelBuilder);
            PopulateItemAuctionDetails(modelBuilder);
            PopulatePropertyAuctionDetails(modelBuilder);
            PopulateVehicleAuctionDetails(modelBuilder);
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
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    FirstName = "Test",
                    MiddleName = "",
                    LastName = "Admin",
                    Email = "dummyadmin@bidding.lv",
                    RoleId = 2,
                    Deleted = false,
                    UniqueIdentifier = "",
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    LastUpdatedAt = CreatedAtDateTime,
                    LastUpdatedBy = CreatedByBiddingAdmin
                },
                new User
                {
                    UserId = 2,
                    FirstName = "Test",
                    MiddleName = "",
                    LastName = "User",
                    Email = "dummyuser@bidding.lv",
                    RoleId = 1,
                    Deleted = false,
                    UniqueIdentifier = "",
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    LastUpdatedAt = CreatedAtDateTime,
                    LastUpdatedBy = CreatedByBiddingAdmin
                },
                new User
                {
                    UserId = 3,
                    FirstName = "Zane",
                    MiddleName = "",
                    LastName = "",
                    Email = "zanehaartman@gmail.com",
                    RoleId = 2,
                    Deleted = false,
                    UniqueIdentifier = "",
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    LastUpdatedAt = CreatedAtDateTime,
                    LastUpdatedBy = CreatedByBiddingAdmin
                },
                new User
                {
                    UserId = 4,
                    FirstName = "Kristaps",
                    MiddleName = "",
                    LastName = "",
                    Email = "kristaps.kerpe@gmail.com",
                    RoleId = 2,
                    Deleted = false,
                    UniqueIdentifier = "",
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    LastUpdatedAt = CreatedAtDateTime,
                    LastUpdatedBy = CreatedByBiddingAdmin
                },
                new User
                {
                    UserId = 5,
                    FirstName = "Jānis",
                    MiddleName = "",
                    LastName = "J",
                    Email = "j.jaunozols@gmail.com",
                    RoleId = 2,
                    Deleted = false,
                    UniqueIdentifier = "",
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    LastUpdatedAt = CreatedAtDateTime,
                    LastUpdatedBy = CreatedByBiddingAdmin
                },
                new User
                {
                    UserId = 6,
                    FirstName = "Jānis",
                    MiddleName = "R",
                    LastName = "B",
                    Email = "janis.rihards.blazevics@gmail.com",
                    RoleId = 2,
                    Deleted = false,
                    UniqueIdentifier = "",
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
                    Name = "Elektrotehnika",
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
                    Name = "Rūpniecības tehnika",
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
                    TypeId = 12,
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
                    TypeId = 13,
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
                    TypeId = 14,
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
                    TypeId = 15,
                    Name = "Zeme",
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

        private static void PopulateItemAuctionDetails(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ItemAuctionDetails>().HasData(
                new ItemAuctionDetails
                {
                    ItemAuctionDetailsId = 1,
                    AuctionItemId = 3,
                    Model = "In progress",
                    ManufacturingDate = DateTime.UtcNow,
                    Evaluation = "In progress",
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    Deleted = false,
                    LastUpdatedAt = CreatedAtDateTime,
                    LastUpdatedBy = CreatedByBiddingAdmin
                }
            );
        }

        private static void PopulatePropertyAuctionDetails(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PropertyAuctionDetails>().HasData(
                new PropertyAuctionDetails
                {
                    PropertyAuctionDetailsId = 1,
                    AuctionItemId = 2,
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    Deleted = false,
                    LastUpdatedAt = CreatedAtDateTime,
                    LastUpdatedBy = CreatedByBiddingAdmin
                }
            );
        }

        private static void PopulateVehicleAuctionDetails(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VehicleAuctionDetails>().HasData(
                new VehicleAuctionDetails
                {
                    VehicleAuctionDetailsId = 1,
                    AuctionItemId = 1,
                    Make = "In progress",
                    Model = "In progress",
                    ManufacturingDate = DateTime.UtcNow,
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
