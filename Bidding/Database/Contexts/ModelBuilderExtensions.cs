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
                new Role { RoleId = 1, Name = "User", CreatedAt = CreatedAtDateTime, CreatedBy = 1, LastUpdatedAt = null, LastUpdatedBy = null, Deleted = false },
                new Role { RoleId = 2, Name = "Admin", CreatedAt = CreatedAtDateTime, CreatedBy = 1, LastUpdatedAt = null, LastUpdatedBy = null, Deleted = false }
            );
        }
        private static void PopulateAuctionFormats(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuctionFormat>().HasData(
                new AuctionFormat { AuctionFormatId = 1, AuctionFormatName = "Cenu aptauja" },
                new AuctionFormat { AuctionFormatId = 2, AuctionFormatName = "Izsole elektroniski" },
                new AuctionFormat { AuctionFormatId = 3, AuctionFormatName = "Izsole klātienē" }
            );
        }
        private static void PopulateAuctionConditions(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuctionCondition>().HasData(
                new AuctionCondition { AuctionConditionId = 1, AuctionConditionName = "Lietota" },
                new AuctionCondition { AuctionConditionId = 2, AuctionConditionName = "Jauna" },
                new AuctionCondition { AuctionConditionId = 3, AuctionConditionName = "Apdzīvots" },
                new AuctionCondition { AuctionConditionId = 4, AuctionConditionName = "Neapdzīvots" },
                new AuctionCondition { AuctionConditionId = 5, AuctionConditionName = "Nepieciešams remonts" },
                new AuctionCondition { AuctionConditionId = 6, AuctionConditionName = "" }
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
                    CreatedBy = 1,
                    LastUpdatedAt = null,
                    LastUpdatedBy = null
                }
            );
        }

        private static void PopulateAuctionStatuses(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuctionStatus>().HasData(
                new AuctionStatus { AuctionStatusId = 1, Name = "Aktīva", CreatedAt = CreatedAtDateTime, CreatedBy = 1, LastUpdatedAt = null, LastUpdatedBy = null, Deleted = false },
                new AuctionStatus { AuctionStatusId = 2, Name = "Pārtraukta", CreatedAt = CreatedAtDateTime, CreatedBy = 1, LastUpdatedAt = null, LastUpdatedBy = null, Deleted = false },
                new AuctionStatus { AuctionStatusId = 3, Name = "Beigusies", CreatedAt = CreatedAtDateTime, CreatedBy = 1, LastUpdatedAt = null, LastUpdatedBy = null, Deleted = false }
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
                    CreatedBy = 1,
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
                    CreatedBy = 1,
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
                    CreatedBy = 1,
                    Deleted = false,
                    LastUpdatedAt = null,
                    LastUpdatedBy = null,
                    AuctionStatusId = 1
                }
            );
        }

        private static void PopulateCategories(ModelBuilder modelBuilder)
        {
            // Categories
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    CategoryId = 1,
                    Name = "Transports",
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = 1,
                    Deleted = false,
                    LastUpdatedAt = null,
                    LastUpdatedBy = null
                },
                new Category
                {
                    CategoryId = 2,
                    Name = "Manta",
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = 1,
                    Deleted = false,
                    LastUpdatedAt = null,
                    LastUpdatedBy = null
                },
                new Category
                {
                    CategoryId = 3,
                    Name = "Nekustamais īpašums",
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = 1,
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
                    CreatedBy = 1,
                    Deleted = false,
                    LastUpdatedAt = null,
                    LastUpdatedBy = null
                },
                new Type
                {
                    TypeId = 2,
                    Name = "Cita manta",
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = 1,
                    Deleted = false,
                    LastUpdatedAt = null,
                    LastUpdatedBy = null
                },
                new Type
                {
                    TypeId = 3,
                    Name = "Dzīvoklis",
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = 1,
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
                new AuctionCategory { AuctionCategoryId = 3, CategoryId = 2, AuctionId = 3 }
            );
        }

        private static void PopulateCategoryTypes(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoryType>().HasData(
                new CategoryType { CategoryTypeId = 1, CategoryId = 1, TypeId = 1 },
                new CategoryType { CategoryTypeId = 2, CategoryId = 2, TypeId = 2 },
                new CategoryType { CategoryTypeId = 3, CategoryId = 3, TypeId = 3 }
            );
        }

        private static void PopulateAuctionTypes(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuctionType>().HasData(
                new AuctionType { AuctionTypeId = 1, TypeId = 1, AuctionId = 1 },
                new AuctionType { AuctionTypeId = 2, TypeId = 3, AuctionId = 2 },
                new AuctionType { AuctionTypeId = 3, TypeId = 2, AuctionId = 3 }
            );
        }
    }
}
