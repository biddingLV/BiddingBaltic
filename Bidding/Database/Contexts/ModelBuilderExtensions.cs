using Bidding.Database.DatabaseModels.Auctions;
using BiddingAPI.Models.DatabaseModels;
using BiddingAPI.Models.DatabaseModels.Bidding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Type = BiddingAPI.Models.DatabaseModels.Type;

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
            PopulateAuctions(modelBuilder);
            PopulateCategories(modelBuilder);
            PopulateTypes(modelBuilder);
            PopulateAuctionFormats(modelBuilder);
            PopulateAuctionConditions(modelBuilder);
            PopulateAuctionCategories(modelBuilder);
            PopulateCategoryTypes(modelBuilder);
            PopulateAuctionTypes(modelBuilder);
        }

        private static void PopulateRoles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new Role { RoleId = 1, Name = "User", CreatedAt = CreatedAtDateTime, CreatedBy = CreatedByBiddingAdmin, LastUpdatedAt = null, LastUpdatedBy = null, Deleted = false },
                new Role { RoleId = 2, Name = "Admin", CreatedAt = CreatedAtDateTime, CreatedBy = CreatedByBiddingAdmin, LastUpdatedAt = null, LastUpdatedBy = null, Deleted = false }
            );
        }

        private static void PopulateUsers(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    FirstName = "Dummy",
                    LastName = "Admin",
                    Email = "dummyadmin@bidding.lv",
                    RoleId = 2,
                    Deleted = false,
                    UniqueIdentifier = "",
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = null,
                    LastUpdatedAt = null,
                    LastUpdatedBy = null
                },
                new User
                {
                    UserId = 2,
                    FirstName = "Dummy",
                    LastName = "User",
                    Email = "dummyuser@bidding.lv",
                    RoleId = 1,
                    Deleted = false,
                    UniqueIdentifier = "",
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    LastUpdatedAt = null,
                    LastUpdatedBy = null
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
                    LastUpdatedAt = null,
                    LastUpdatedBy = null,
                    Deleted = false
                },
                new AuctionStatus
                {
                    AuctionStatusId = 2,
                    Name = "Pārtraukta",
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    LastUpdatedAt = null,
                    LastUpdatedBy = null,
                    Deleted = false
                },
                new AuctionStatus
                {
                    AuctionStatusId = 3,
                    Name = "Beigusies",
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    LastUpdatedAt = null,
                    LastUpdatedBy = null,
                    Deleted = false
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
                    EndDate = CreatedAtDateTime.AddMonths(6),
                    ApplyDate = CreatedAtDateTime.AddMonths(5),
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    Deleted = false,
                    LastUpdatedAt = null,
                    LastUpdatedBy = null,
                    AuctionStatusId = 1
                },
                new Auction
                {
                    AuctionId = 2,
                    Name = "Penthouse tipa dzīvoklis Vecrīgas sirdī",
                    StartingPrice = 50000,
                    StartDate = CreatedAtDateTime.AddMonths(1),
                    EndDate = CreatedAtDateTime.AddMonths(6),
                    ApplyDate = CreatedAtDateTime.AddMonths(5),
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    Deleted = false,
                    LastUpdatedAt = null,
                    LastUpdatedBy = null,
                    AuctionStatusId = 1,
                },
                new Auction
                {
                    AuctionId = 3,
                    Name = "Vīna skapis",
                    StartingPrice = 900,
                    StartDate = CreatedAtDateTime.AddMonths(1),
                    EndDate = CreatedAtDateTime.AddMonths(6),
                    ApplyDate = CreatedAtDateTime.AddMonths(5),
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    Deleted = false,
                    LastUpdatedAt = null,
                    LastUpdatedBy = null,
                    AuctionStatusId = 1
                },
                new Auction
                {
                    AuctionId = 4,
                    Name = "Audi A4",
                    StartingPrice = 350,
                    StartDate = CreatedAtDateTime.AddMonths(1),
                    EndDate = CreatedAtDateTime.AddMonths(2),
                    ApplyDate = CreatedAtDateTime.AddMonths(1).AddDays(10),
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    Deleted = false,
                    LastUpdatedAt = null,
                    LastUpdatedBy = null,
                    AuctionStatusId = 1
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
                    LastUpdatedAt = null,
                    LastUpdatedBy = null
                },
                new Category
                {
                    CategoryId = 2,
                    Name = "Manta",
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    Deleted = false,
                    LastUpdatedAt = null,
                    LastUpdatedBy = null
                },
                new Category
                {
                    CategoryId = 3,
                    Name = "Nekustamais īpašums",
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    Deleted = false,
                    LastUpdatedAt = null,
                    LastUpdatedBy = null
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
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    Deleted = false,
                    LastUpdatedAt = null,
                    LastUpdatedBy = null
                },
                new Type
                {
                    TypeId = 2,
                    Name = "Traktortehnika",
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    Deleted = false,
                    LastUpdatedAt = null,
                    LastUpdatedBy = null
                },
                new Type
                {
                    TypeId = 3,
                    Name = "Kravas auto",
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    Deleted = false,
                    LastUpdatedAt = null,
                    LastUpdatedBy = null
                },
                new Type
                {
                    TypeId = 4,
                    Name = "Mototehnika",
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    Deleted = false,
                    LastUpdatedAt = null,
                    LastUpdatedBy = null
                },
                new Type
                {
                    TypeId = 5,
                    Name = "Ūdens transports",
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    Deleted = false,
                    LastUpdatedAt = null,
                    LastUpdatedBy = null
                },
                new Type
                {
                    TypeId = 6,
                    Name = "Cits transports",
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    Deleted = false,
                    LastUpdatedAt = null,
                    LastUpdatedBy = null
                },
                new Type
                {
                    TypeId = 7,
                    Name = "Biroja tehnika",
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    Deleted = false,
                    LastUpdatedAt = null,
                    LastUpdatedBy = null
                },
                new Type
                {
                    TypeId = 8,
                    Name = "Elektrotehnika",
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    Deleted = false,
                    LastUpdatedAt = null,
                    LastUpdatedBy = null
                },
                new Type
                {
                    TypeId = 9,
                    Name = "Rūpniecības tehnika",
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    Deleted = false,
                    LastUpdatedAt = null,
                    LastUpdatedBy = null
                },
                new Type
                {
                    TypeId = 10,
                    Name = "Instrumenti",
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    Deleted = false,
                    LastUpdatedAt = null,
                    LastUpdatedBy = null
                },
                new Type
                {
                    TypeId = 11,
                    Name = "Cita manta",
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    Deleted = false,
                    LastUpdatedAt = null,
                    LastUpdatedBy = null
                },
                new Type
                {
                    TypeId = 12,
                    Name = "Dzīvoklis",
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    Deleted = false,
                    LastUpdatedAt = null,
                    LastUpdatedBy = null
                },
                new Type
                {
                    TypeId = 13,
                    Name = "Māja",
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    Deleted = false,
                    LastUpdatedAt = null,
                    LastUpdatedBy = null
                },
                new Type
                {
                    TypeId = 14,
                    Name = "Zeme",
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    Deleted = false,
                    LastUpdatedAt = null,
                    LastUpdatedBy = null
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
                    LastUpdatedAt = null,
                    LastUpdatedBy = null
                },
                new AuctionFormat
                {
                    AuctionFormatId = 2,
                    Name = "Izsole elektroniski",
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    Deleted = false,
                    LastUpdatedAt = null,
                    LastUpdatedBy = null
                },
                new AuctionFormat
                {
                    AuctionFormatId = 3,
                    Name = "Izsole klātienē",
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    Deleted = false,
                    LastUpdatedAt = null,
                    LastUpdatedBy = null
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
                    LastUpdatedAt = null,
                    LastUpdatedBy = null
                },
                new AuctionCondition
                {
                    AuctionConditionId = 2,
                    Name = "Jauna",
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    Deleted = false,
                    LastUpdatedAt = null,
                    LastUpdatedBy = null
                },
                new AuctionCondition
                {
                    AuctionConditionId = 3,
                    Name = "Apdzīvots",
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    Deleted = false,
                    LastUpdatedAt = null,
                    LastUpdatedBy = null
                },
                new AuctionCondition
                {
                    AuctionConditionId = 4,
                    Name = "Neapdzīvots",
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    Deleted = false,
                    LastUpdatedAt = null,
                    LastUpdatedBy = null
                },
                new AuctionCondition
                {
                    AuctionConditionId = 5,
                    Name = "Nepieciešams remonts",
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = CreatedByBiddingAdmin,
                    Deleted = false,
                    LastUpdatedAt = null,
                    LastUpdatedBy = null
                }
            );
        }

        private static void PopulateAuctionCategories(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuctionCategory>().HasData(
                new AuctionCategory { AuctionCategoryId = 1, CategoryId = 1, AuctionId = 1 },
                new AuctionCategory { AuctionCategoryId = 2, CategoryId = 3, AuctionId = 2 },
                new AuctionCategory { AuctionCategoryId = 3, CategoryId = 2, AuctionId = 3 },
                new AuctionCategory { AuctionCategoryId = 4, CategoryId = 1, AuctionId = 4 }
            );
        }

        private static void PopulateCategoryTypes(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoryType>().HasData(
                new CategoryType { CategoryTypeId = 1, CategoryId = 1, TypeId = 1 },
                new CategoryType { CategoryTypeId = 2, CategoryId = 1, TypeId = 2 },
                new CategoryType { CategoryTypeId = 3, CategoryId = 1, TypeId = 3 },
                new CategoryType { CategoryTypeId = 4, CategoryId = 1, TypeId = 4 },
                new CategoryType { CategoryTypeId = 5, CategoryId = 1, TypeId = 5 },
                new CategoryType { CategoryTypeId = 6, CategoryId = 1, TypeId = 6 },
                new CategoryType { CategoryTypeId = 7, CategoryId = 2, TypeId = 7 },
                new CategoryType { CategoryTypeId = 8, CategoryId = 2, TypeId = 8 },
                new CategoryType { CategoryTypeId = 9, CategoryId = 2, TypeId = 9 },
                new CategoryType { CategoryTypeId = 10, CategoryId = 2, TypeId = 10 },
                new CategoryType { CategoryTypeId = 11, CategoryId = 2, TypeId = 11 },
                new CategoryType { CategoryTypeId = 12, CategoryId = 3, TypeId = 12 },
                new CategoryType { CategoryTypeId = 13, CategoryId = 3, TypeId = 13 },
                new CategoryType { CategoryTypeId = 14, CategoryId = 3, TypeId = 14 }
            );
        }

        private static void PopulateAuctionTypes(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuctionType>().HasData(
                new AuctionType { AuctionTypeId = 1, TypeId = 1, AuctionId = 1 },
                new AuctionType { AuctionTypeId = 2, TypeId = 3, AuctionId = 2 },
                new AuctionType { AuctionTypeId = 3, TypeId = 2, AuctionId = 3 },
                new AuctionType { AuctionTypeId = 4, TypeId = 1, AuctionId = 4 }
            );
        }
    }
}
