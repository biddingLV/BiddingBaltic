using System;
using System.Collections.Generic;
using Bidding.Models.DatabaseModels.Bidding.Subscribe;
using Bidding.Shared.Database;
using BiddingAPI.Models.DatabaseModels.Bidding;
using BiddingAPI.Models.ViewModels.Bidding.Auctions.List;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BiddingAPI.Models.DatabaseModels
{
    public partial class BiddingContext : DbContextBase<BiddingContext>
    {
        public BiddingContext(DbContextOptions<BiddingContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Auction> Auctions { get; set; }
        public virtual DbSet<AuctionCategory> AuctionCategories { get; set; }
        public virtual DbSet<AuctionDetail> AuctionDetails { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<CategoryType> CategoryTypes { get; set; }
        public virtual DbSet<Feature> Features { get; set; }
        //public virtual DbSet<Images> Images { get; set; }
        public virtual DbSet<Organization> Organizations { get; set; }
        public virtual DbSet<ProductDetail> ProductDetails { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<TypeProduct> TypeProducts { get; set; }
        public virtual DbSet<Type> Types { get; set; }
        public virtual DbSet<UserDetail> UserDetails { get; set; }
        public virtual DbSet<UserOrganization> UserOrganizations { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Newsletter> Newsletters { get; set; }

        // do I need this???
        // todo: kke: queries -> move to Query() 2.1 version
        //public virtual DbQuery<AuctionListViewModel> AuctionListViewModel { get; set; }
    }
}
