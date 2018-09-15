using System;
using BiddingAPI.Models.DatabaseModels.Bidding;
using BiddingAPI.Models.ViewModels.Bidding.Auctions.List;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BiddingAPI.Models.DatabaseModels
{
    public partial class BiddingContext : DbContext
    {
        public BiddingContext(DbContextOptions<BiddingContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Auctions> Auctions { get; set; }
        public virtual DbSet<AuctionCategories> AuctionCategories { get; set; }
        public virtual DbSet<AuctionDetails> AuctionDetails { get; set; }
        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<CategoryTypes> CategoryTypes { get; set; }
        public virtual DbSet<Features> Features { get; set; }
        //public virtual DbSet<Images> Images { get; set; }
        public virtual DbSet<Organizations> Organizations { get; set; }
        public virtual DbSet<ProductDetails> ProductDetails { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<TypeProducts> TypeProducts { get; set; }
        public virtual DbSet<Types> Types { get; set; }
        public virtual DbSet<UserDetails> UserDetails { get; set; }
        public virtual DbSet<UserOrganizations> UserOrganizations { get; set; }
        public virtual DbSet<UserRoles> UserRoles { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        // todo: kke: queries -> move to Query() 2.1 version
        public virtual DbQuery<AuctionListViewModel> AuctionListViewModel { get; set; }
    }
}
