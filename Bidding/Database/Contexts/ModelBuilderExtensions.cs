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
            PopulateAuctionCategories(modelBuilder);
            PopulateCategoryTypes(modelBuilder);
        }

        private static void PopulateRoles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new Role { RoleId = 1, RoleName = "User", CreatedAt = CreatedAtDateTime, CreatedBy = 1, LastUpdatedAt = null, LastUpdatedBy = null, Deleted = false },
                new Role { RoleId = 2, RoleName = "Admin", CreatedAt = CreatedAtDateTime, CreatedBy = 1, LastUpdatedAt = null, LastUpdatedBy = null, Deleted = false }
            );
        }

        private static void PopulateUsers(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 2,
                    UserFirstName = "Dummy",
                    UserLastName = "User",
                    UserEmail = "dummyuser@bidding.lv",
                    UserRoleId = 1,
                    Deleted = false,
                    UserUniqueIdentifier = "",
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = 1,
                    LastUpdatedAt = null,
                    LastUpdatedBy = null
                },
                new User
                {
                    UserId = 1,
                    UserFirstName = "Dummy",
                    UserLastName = "Admin",
                    UserEmail = "dummyadmin@bidding.lv",
                    UserRoleId = 2,
                    Deleted = false,
                    UserUniqueIdentifier = "",
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
                new AuctionStatus { AuctionStatusId = 1, AuctionStatusName = "Aktīva", CreatedAt = CreatedAtDateTime, CreatedBy = 1, LastUpdatedAt = null, LastUpdatedBy = null, Deleted = false },
                new AuctionStatus { AuctionStatusId = 2, AuctionStatusName = "Pārtraukta", CreatedAt = CreatedAtDateTime, CreatedBy = 1, LastUpdatedAt = null, LastUpdatedBy = null, Deleted = false },
                new AuctionStatus { AuctionStatusId = 3, AuctionStatusName = "Beigusies", CreatedAt = CreatedAtDateTime, CreatedBy = 1, LastUpdatedAt = null, LastUpdatedBy = null, Deleted = false }
            );
        }

        private static void PopulateAuctions(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Auction>().HasData(
                new Auction
                {
                    AuctionId = 1,
                    AuctionName = "Tesla Model 3",
                    AuctionStartingPrice = 15000,
                    AuctionStartDate = CreatedAtDateTime.AddMonths(1),
                    AuctionEndDate = CreatedAtDateTime.AddMonths(6),
                    AuctionStatusId = 1,
                    AuctionApplyDate = CreatedAtDateTime.AddMonths(5),
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = 1,
                    Deleted = false,
                    LastUpdatedAt = null,
                    LastUpdatedBy = null
                },
                new Auction
                {
                    AuctionId = 2,
                    AuctionName = "Penthouse tipa dzīvoklis Vecrīgas sirdī",
                    AuctionStartingPrice = 50000,
                    AuctionStartDate = CreatedAtDateTime.AddMonths(1),
                    AuctionEndDate = CreatedAtDateTime.AddMonths(6),
                    AuctionStatusId = 1,
                    AuctionApplyDate = CreatedAtDateTime.AddMonths(5),
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = 1,
                    Deleted = false,
                    LastUpdatedAt = null,
                    LastUpdatedBy = null
                },
                new Auction
                {
                    AuctionId = 3,
                    AuctionName = "Vīna skapis",
                    AuctionStartingPrice = 900,
                    AuctionStartDate = CreatedAtDateTime.AddMonths(1),
                    AuctionEndDate = CreatedAtDateTime.AddMonths(6),
                    AuctionStatusId = 1,
                    AuctionApplyDate = CreatedAtDateTime.AddMonths(5),
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = 1,
                    Deleted = false,
                    LastUpdatedAt = null,
                    LastUpdatedBy = null
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
                    CategoryName = "Transports",
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = 1,
                    Deleted = false,
                    LastUpdatedAt = null,
                    LastUpdatedBy = null
                },
                new Category
                {
                    CategoryId = 2,
                    CategoryName = "Manta",
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = 1,
                    Deleted = false,
                    LastUpdatedAt = null,
                    LastUpdatedBy = null
                },
                new Category
                {
                    CategoryId = 3,
                    CategoryName = "Nekustamais īpašums",
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
                    TypeName = "Vieglais transports līdz 3,5t",
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = 1,
                    Deleted = false,
                    LastUpdatedAt = null,
                    LastUpdatedBy = null
                },
                new Type
                {
                    TypeId = 2,
                    TypeName = "Cita manta",
                    CreatedAt = CreatedAtDateTime,
                    CreatedBy = 1,
                    Deleted = false,
                    LastUpdatedAt = null,
                    LastUpdatedBy = null
                },
                new Type
                {
                    TypeId = 3,
                    TypeName = "Dzīvoklis",
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
    }
}
