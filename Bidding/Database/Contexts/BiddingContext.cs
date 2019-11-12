using Bidding.Database.DatabaseModels;
using Bidding.Database.DatabaseModels.Auctions;
using Bidding.Database.DatabaseModels.Item;
using Bidding.Database.DatabaseModels.Property;
using Bidding.Database.DatabaseModels.Shared;
using Bidding.Database.DatabaseModels.Users;
using Bidding.Database.DatabaseModels.Vehicle;
using Bidding.Models.DatabaseModels;
using Bidding.Models.DatabaseModels.Bidding.Subscribe;
using Bidding.Models.ViewModels.Bidding.Admin.Users.List;
using Bidding.Models.ViewModels.Bidding.Auctions.List;
using Bidding.Models.ViewModels.Bidding.Filters;
using DataLayer.ExtraAuthClasses;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Type = Bidding.Models.DatabaseModels.Type;

namespace Bidding.Database.Contexts
{
    public class BiddingContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        public BiddingContext(DbContextOptions<BiddingContext> options) : base(options) { }

        public DbSet<VehicleTransmission> VehicleTransmissions { get; set; }
        public DbSet<VehicleFuelType> VehicleFuelTypes { get; set; }
        public DbSet<ItemCondition> ItemConditions { get; set; }
        public DbSet<PropertyMeasurementType> PropertyMeasurementTypes { get; set; }
        public DbSet<AuctionStatus> AuctionStatuses { get; set; }
        public DbSet<AuctionFormat> AuctionFormats { get; set; }
        public DbSet<AuctionCondition> AuctionConditions { get; set; }
        public DbSet<AuctionDetails> AuctionDetails { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<AuctionItem> AuctionItems { get; set; }
        public DbSet<AuctionCreator> AuctionCreators { get; set; }
        public DbSet<Auction> Auctions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Type> Types { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Newsletter> Newsletters { get; set; }

        public DbSet<UserToRole> UserToRoles { get; set; }
        public DbSet<RoleToPermissions> RolesToPermissions { get; set; }
        public DbSet<ModulesForUser> ModulesForUsers { get; set; }

        //public virtual DbSet<TypeProduct> TypeProducts { get; set; }
        //public virtual DbSet<Product> Products { get; set; }
        //public virtual DbSet<Feature> Features { get; set; }
        //public virtual DbSet<Images> Images { get; set; }
        //public virtual DbSet<Organization> Organizations { get; set; }
        //public virtual DbSet<ProductDetail> ProductDetails { get; set; }
        //public virtual DbSet<UserDetail> UserDetails { get; set; }
        //public virtual DbSet<UserOrganization> UserOrganizations { get; set; }

        // Database Queries for stored procedures, views and so on!
        public virtual DbQuery<TopCategoryFilterModel> TopCategoryFilter { get; set; }
        public virtual DbQuery<SubCategoryFilterModel> SubCategoryFilter { get; set; }
        public virtual DbQuery<AuctionListItemModel> AuctionList { get; set; }
        public virtual DbQuery<UserListItemModel> UserListItem { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            //modelBuilder.Entity<Auction>()
            //    .HasOne(p => p.User)
            //    .WithMany(b => b.Auctions)
            //    .HasForeignKey(p => p.CreatedBy);

            modelBuilder.Entity<Auction>()
                .HasOne(p => p.AuctionStatus)
                .WithMany(b => b.Auctions)
                .HasForeignKey(p => p.AuctionStatusId);

            modelBuilder.Entity<Auction>()
                .HasOne(p => p.AuctionCreator)
                .WithMany(b => b.Auctions)
                .HasForeignKey(p => p.AuctionCreatorId);

            modelBuilder.Entity<AuctionDetails>() // todo: kke: is this right?
                .HasOne(p => p.VehicleTransmission)
                .WithMany(b => b.AuctionDetails)
                .HasForeignKey(p => p.TransmissionId);

            modelBuilder.Entity<AuctionDetails>() // todo: kke: is this right?
                .HasOne(p => p.VehicleFuelType)
                .WithMany(b => b.AuctionDetails)
                .HasForeignKey(p => p.FuelTypeId);

            modelBuilder.Entity<AuctionDetails>() // todo: kke: is this right?
                .HasOne(p => p.ItemCondition)
                .WithMany(b => b.AuctionDetails)
                .HasForeignKey(p => p.ConditionId);

            modelBuilder.Entity<AuctionDetails>() // todo: kke: is this right?
                .HasOne(p => p.PropertyMeasurementType)
                .WithMany(b => b.AuctionDetails)
                .HasForeignKey(p => p.MeasurementTypeId);

            modelBuilder.Entity<AuctionDetails>() // todo: kke: is this right?
                .HasOne(p => p.Region)
                .WithMany(b => b.AuctionDetails)
                .HasForeignKey(p => p.RegionId);

            modelBuilder.Entity<Auction>()
                .HasOne(p => p.Category)
                .WithMany(b => b.Auctions)
                .HasForeignKey(p => p.AuctionCategoryId);

            modelBuilder.Entity<Auction>()
                .HasOne(p => p.Type)
                .WithMany(b => b.Auctions)
                .HasForeignKey(p => p.AuctionTypeId);

            //modelBuilder.Entity<AuctionStatus>()
            //    .HasOne(p => p.User)
            //    .WithMany(b => b.AuctionStatuses)
            //    .HasForeignKey(p => p.CreatedBy);

            //modelBuilder.Entity<Role>()
            //    .HasOne(p => p.User)
            //    .WithMany(b => b.Roles)
            //    .HasForeignKey(p => p.CreatedBy);

            //modelBuilder.Entity<Permission>()
            //    .HasOne(p => p.User)
            //    .WithMany(b => b.Permissions)
            //    .HasForeignKey(p => p.CreatedBy);

            //modelBuilder.Entity<UserRole>()
            //    .HasOne(p => p.User)
            //    .WithMany(b => b.UserRoles)
            //    .HasForeignKey(p => p.CreatedBy);

            //modelBuilder.Entity<RolePermission>()
            //    .HasOne(p => p.User)
            //    .WithMany(b => b.RolePermissions)
            //    .HasForeignKey(p => p.CreatedBy);

            //modelBuilder.Entity<VehicleTransmission>()
            //    .HasOne(p => p.User)
            //    .WithMany(b => b.VehicleTransmissions)
            //    .HasForeignKey(p => p.CreatedBy);

            //modelBuilder.Entity<VehicleFuelType>()
            //    .HasOne(p => p.User)
            //    .WithMany(b => b.VehicleFuelTypes)
            //    .HasForeignKey(p => p.CreatedBy);

            //modelBuilder.Entity<ItemCondition>()
            //    .HasOne(p => p.User)
            //    .WithMany(b => b.ItemConditions)
            //    .HasForeignKey(p => p.CreatedBy);

            //modelBuilder.Entity<PropertyMeasurementType>()
            //    .HasOne(p => p.User)
            //    .WithMany(b => b.PropertyMeasurementTypes)
            //    .HasForeignKey(p => p.CreatedBy);

            //modelBuilder.Entity<AuctionFormat>()
            //    .HasOne(p => p.User)
            //    .WithMany(b => b.AuctionFormats)
            //    .HasForeignKey(p => p.CreatedBy);

            //modelBuilder.Entity<AuctionCondition>()
            //    .HasOne(p => p.User)
            //    .WithMany(b => b.AuctionConditions)
            //    .HasForeignKey(p => p.CreatedBy);

            //modelBuilder.Entity<AuctionDetails>()
            //    .HasOne(p => p.User)
            //    .WithMany(b => b.AuctionDetails)
            //    .HasForeignKey(p => p.CreatedBy);

            //modelBuilder.Entity<Region>()
            //    .HasOne(p => p.User)
            //    .WithMany(b => b.Regions)
            //    .HasForeignKey(p => p.CreatedBy);

            //modelBuilder.Entity<Category>()
            //    .HasOne(p => p.User)
            //    .WithMany(b => b.Categories)
            //    .HasForeignKey(p => p.CreatedBy);

            //modelBuilder.Entity<Type>()
            //    .HasOne(p => p.User)
            //    .WithMany(b => b.Types)
            //    .HasForeignKey(p => p.CreatedBy);

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

            //modelBuilder.Entity<AuctionItem>()
            //    .HasOne(p => p.User)
            //    .WithMany(b => b.AuctionItems)
            //    .HasForeignKey(p => p.CreatedBy);

            //modelBuilder.Entity<AuctionCreator>()
            //    .HasOne(p => p.User)
            //    .WithMany(b => b.AuctionCreators)
            //    .HasForeignKey(p => p.CreatedBy);

            //ExtraAuthClasses configurations
            modelBuilder.Entity<UserToRole>().HasKey(x => new { x.UserId, x.RoleName });

            modelBuilder.Entity<RoleToPermissions>()
                .Property("_permissionsInRole")
                .HasColumnName("PermissionsInRole");

            modelBuilder.Seed();
        }
    }
}
