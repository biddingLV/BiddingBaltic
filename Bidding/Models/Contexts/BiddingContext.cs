using Bidding.Models.DatabaseModels.Auctions;
using Bidding.Models.DatabaseModels.Categories;
using Bidding.Models.DatabaseModels.Item;
using Bidding.Models.DatabaseModels.Property;
using Bidding.Models.DatabaseModels.Shared;
using Bidding.Models.DatabaseModels.Subscribe;
using Bidding.Models.DatabaseModels.Vehicle;
using Bidding.Models.ViewModels.Admin.Users.List;
using Bidding.Models.ViewModels.Auctions.List;
using Bidding.Models.ViewModels.Filters;
using DataLayer.ExtraAuthClasses;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Bidding.Models.Contexts
{
    public class BiddingContext : IdentityDbContext<ApplicationUser, ApplicationRole, int, IdentityUserClaim<int>,
    ApplicationUserRole, IdentityUserLogin<int>,
    IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public BiddingContext(DbContextOptions<BiddingContext> options) : base(options) { }

        public DbSet<VehicleTransmission> VehicleTransmissions { get; set; }
        public DbSet<VehicleFuelType> VehicleFuelTypes { get; set; }
        public DbSet<VehicleDimensionType> VehicleDimensionTypes { get; set; }
        public DbSet<ItemCondition> ItemConditions { get; set; }
        public DbSet<ItemCompanyType> ItemCompanyTypes { get; set; }
        public DbSet<PropertyMeasurementType> PropertyMeasurementTypes { get; set; }
        public DbSet<AuctionStatus> AuctionStatuses { get; set; }
        public DbSet<AuctionFormat> AuctionFormats { get; set; }
        public DbSet<AuctionCondition> AuctionConditions { get; set; }
        public DbSet<AuctionDetails> AuctionDetails { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<AuctionItem> AuctionItems { get; set; }
        public DbSet<AuctionCreator> AuctionCreators { get; set; }
        public DbSet<Auction> Auctions { get; set; }
        public DbSet<AuctionCategory> Categories { get; set; }
        public DbSet<AuctionType> Types { get; set; }
        public DbSet<Newsletter> Newsletters { get; set; }

        public DbSet<RoleToPermissions> RolesToPermissions { get; set; }
        public DbSet<ModulesForUser> ModulesForUsers { get; set; }

        // public DbSet<TypeProduct> TypeProducts { get; set; }
        // public DbSet<Product> Products { get; set; }
        // public DbSet<Feature> Features { get; set; }
        // public DbSet<Organization> Organizations { get; set; }
        // public DbSet<ProductDetail> ProductDetails { get; set; }
        // public DbSet<UserDetail> UserDetails { get; set; }
        // public DbSet<UserOrganization> UserOrganizations { get; set; }

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

            modelBuilder.Entity<ApplicationUserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

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
                .HasOne(p => p.VehicleDimensionType)
                .WithMany(b => b.AuctionDetails)
                .HasForeignKey(p => p.DimensionTypeId);

            modelBuilder.Entity<AuctionDetails>() // todo: kke: is this right?
                .HasOne(p => p.ItemCondition)
                .WithMany(b => b.AuctionDetails)
                .HasForeignKey(p => p.ConditionId);

            modelBuilder.Entity<AuctionDetails>() // todo: kke: is this right?
                .HasOne(p => p.ItemCompanyType)
                .WithMany(b => b.AuctionDetails)
                .HasForeignKey(p => p.CompanyTypeId);

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
            modelBuilder.Entity<AuctionType>()
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
            modelBuilder.Entity<RoleToPermissions>()
                .Property("_permissionsInRole")
                .HasColumnName("PermissionsInRole");

            modelBuilder.Seed();
        }
    }
}
