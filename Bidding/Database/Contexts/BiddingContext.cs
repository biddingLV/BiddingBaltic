using System;
using System.Collections.Generic;
using System.Diagnostics;
using Bidding.Database.Contexts;
using Bidding.Database.DatabaseModels.Auctions;
using Bidding.Database.DatabaseModels.Users;
using Bidding.Models.DatabaseModels.Bidding;
using Bidding.Models.DatabaseModels.Bidding.Subscribe;
using Bidding.Models.ViewModels.Bidding.Auctions;
using Bidding.Models.ViewModels.Bidding.Auctions.List;
using Bidding.Models.ViewModels.Bidding.Filters;
using Bidding.Shared.Database;
using BiddingAPI.Models.DatabaseModels.Bidding;
using Microsoft.EntityFrameworkCore;

namespace BiddingAPI.Models.DatabaseModels
{
    public partial class BiddingContext : DbContextBase<BiddingContext>
    {
        public BiddingContext(DbContextOptions<BiddingContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AuctionStatus> AuctionStatuses { get; set; }
        public virtual DbSet<AuctionDetails> AuctionDetails { get; set; }
        public virtual DbSet<Auction> Auctions { get; set; }
        public virtual DbSet<AuctionCategory> AuctionCategories { get; set; } // intermediary table
        public virtual DbSet<AuctionType> AuctionTypes { get; set; } // intermediary table
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<CategoryType> CategoryTypes { get; set; } // intermediary table
        public virtual DbSet<Type> Types { get; set; }
        public virtual DbSet<TypeProduct> TypeProducts { get; set; } // intermediary table
        public virtual DbSet<Product> Products { get; set; }

        // Users
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        //public virtual DbSet<Feature> Features { get; set; }
        //public virtual DbSet<Images> Images { get; set; }
        //public virtual DbSet<Organization> Organizations { get; set; }
        //public virtual DbSet<ProductDetail> ProductDetails { get; set; }
        //public virtual DbSet<UserDetail> UserDetails { get; set; }
        //public virtual DbSet<UserOrganization> UserOrganizations { get; set; }
        //public virtual DbSet<UserRole> UserRoles { get; set; }
        //public virtual DbSet<Newsletter> Newsletters { get; set; }
        //public virtual DbSet<AuctionCreator> AuctionCreators { get; set; }

        // Database Queries for stored procedures / views
        public virtual DbQuery<TopCategoryFilterModel> TopCategoryFilter { get; set; }
        public virtual DbQuery<SubCategoryFilterModel> SubCategoryFilter { get; set; }
        public virtual DbQuery<AuctionListModel> AuctionList { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // todo: kke: is this right?
            //if (Debugger.IsAttached)
            //{
            // debugger is attached
            modelBuilder.Seed();
            //}
        }
    }
}
