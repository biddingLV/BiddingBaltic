using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Bidding.Database.Contexts;
using Bidding.Database.DatabaseModels.Auctions;
using Bidding.Database.DatabaseModels.Users;
using Bidding.Models.DatabaseModels.Bidding.Subscribe;
using Bidding.Models.ViewModels.Bidding.Auctions.List;
using Bidding.Models.ViewModels.Bidding.Filters;
using Bidding.Shared.Database;
using Bidding.Models.DatabaseModels;
using Microsoft.EntityFrameworkCore;
using Type = Bidding.Models.DatabaseModels.Type;
using Bidding.Database.DatabaseModels.Vehicle;

namespace Bidding.Database.Contexts
{
    public partial class BiddingContext : DbContextBase<BiddingContext>
    {
        public BiddingContext(DbContextOptions<BiddingContext> options) : base(options) { }

        public virtual DbSet<VehicleTransmission> VehicleTransmissions { get; set; }
        public virtual DbSet<VehicleFuelType> VehicleFuelTypes { get; set; }
        public virtual DbSet<AuctionStatus> AuctionStatuses { get; set; }
        public virtual DbSet<AuctionFormat> AuctionFormats { get; set; }
        public virtual DbSet<AuctionCondition> AuctionConditions { get; set; }
        public virtual DbSet<AuctionDetails> AuctionDetails { get; set; }
        public virtual DbSet<AuctionItem> AuctionItems { get; set; }
        public virtual DbSet<AuctionCreator> AuctionCreators { get; set; }
        public virtual DbSet<Auction> Auctions { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Type> Types { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        //public virtual DbSet<TypeProduct> TypeProducts { get; set; } // intermediary table
        //public virtual DbSet<Product> Products { get; set; }
        //public virtual DbSet<Permission> Permissions { get; set; }
        //public virtual DbSet<Feature> Features { get; set; }
        //public virtual DbSet<Images> Images { get; set; }
        //public virtual DbSet<Organization> Organizations { get; set; }
        //public virtual DbSet<ProductDetail> ProductDetails { get; set; }
        //public virtual DbSet<UserDetail> UserDetails { get; set; }
        //public virtual DbSet<UserOrganization> UserOrganizations { get; set; }
        public virtual DbSet<Newsletter> Newsletters { get; set; }
        //public virtual DbSet<AuctionCreator> AuctionCreators { get; set; }

        // Database Queries for stored procedures / views
        public virtual DbQuery<TopCategoryFilterModel> TopCategoryFilter { get; set; }
        public virtual DbQuery<SubCategoryFilterModel> SubCategoryFilter { get; set; }
        public virtual DbQuery<AuctionListModel> AuctionList { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelBuilder.Entity<Role>()
                .HasMany(c => c.Users)
                .WithOne(e => e.Role);

            modelBuilder.Entity<Auction>()
                .HasOne(p => p.User)
                .WithMany(b => b.Auctions)
                .HasForeignKey(p => p.CreatedBy);

            modelBuilder.Entity<Auction>()
                .HasOne(p => p.AuctionStatus)
                .WithMany(b => b.Auctions)
                .HasForeignKey(p => p.AuctionStatusId);

            modelBuilder.Entity<AuctionDetails>() // todo: kke: is this right?
                .HasOne(p => p.VehicleTransmission)
                .WithMany(b => b.AuctionDetails)
                .HasForeignKey(p => p.TransmissionId);

            modelBuilder.Entity<AuctionDetails>() // todo: kke: is this right?
                .HasOne(p => p.VehicleFuelType)
                .WithMany(b => b.AuctionDetails)
                .HasForeignKey(p => p.FuelTypeId);

            modelBuilder.Entity<Auction>()
                .HasOne(p => p.Category)
                .WithMany(b => b.Auctions)
                .HasForeignKey(p => p.AuctionCategoryId);

            modelBuilder.Entity<Auction>()
                .HasOne(p => p.Type)
                .WithMany(b => b.Auctions)
                .HasForeignKey(p => p.AuctionTypeId);

            modelBuilder.Entity<AuctionStatus>()
                .HasOne(p => p.User)
                .WithMany(b => b.AuctionStatuses)
                .HasForeignKey(p => p.CreatedBy);

            modelBuilder.Entity<VehicleTransmission>()
                .HasOne(p => p.User)
                .WithMany(b => b.VehicleTransmissions)
                .HasForeignKey(p => p.CreatedBy);

            modelBuilder.Entity<VehicleFuelType>()
                .HasOne(p => p.User)
                .WithMany(b => b.VehicleFuelTypes)
                .HasForeignKey(p => p.CreatedBy);

            modelBuilder.Entity<AuctionFormat>()
                .HasOne(p => p.User)
                .WithMany(b => b.AuctionFormats)
                .HasForeignKey(p => p.CreatedBy);

            modelBuilder.Entity<AuctionCondition>()
                .HasOne(p => p.User)
                .WithMany(b => b.AuctionConditions)
                .HasForeignKey(p => p.CreatedBy);

            modelBuilder.Entity<Category>()
                .HasOne(p => p.User)
                .WithMany(b => b.Categories)
                .HasForeignKey(p => p.CreatedBy);

            modelBuilder.Entity<Type>()
                .HasOne(p => p.User)
                .WithMany(b => b.Types)
                .HasForeignKey(p => p.CreatedBy);

            // todo: kke: not sure if this is right?
            modelBuilder.Entity<Type>()
                .HasOne(p => p.Category)
                .WithMany(b => b.Types)
                .HasForeignKey(p => p.AuctionCategoryId);

            // todo: kke: not sure if this is right?
            modelBuilder.Entity<AuctionItem>()
                .HasOne(p => p.Category)
                .WithMany(b => b.AuctionItems)
                .HasForeignKey(p => p.AuctionItemCategoryId);

            // todo: kke: not sure if this is right?
            modelBuilder.Entity<AuctionItem>()
                .HasOne(p => p.Type)
                .WithMany(b => b.AuctionItems)
                .HasForeignKey(p => p.AuctionItemTypeId);

            modelBuilder.Entity<AuctionItem>()
                .HasOne(p => p.User)
                .WithMany(b => b.AuctionItems)
                .HasForeignKey(p => p.CreatedBy);

            modelBuilder.Entity<AuctionCreator>()
                .HasOne(p => p.User)
                .WithMany(b => b.AuctionCreators)
                .HasForeignKey(p => p.CreatedBy);

            modelBuilder.Seed();
        }
    }
}
