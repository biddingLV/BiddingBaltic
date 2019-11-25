using System;
using System.ComponentModel.DataAnnotations;

namespace PermissionParts
{
    public enum Permission
    {
        /// <summary>
        /// Error condition
        /// </summary>
        NotSet = 0,

        // Basic user
        [Display(GroupName = "BasicUser", Name = "BasicUser", Description = "BasicUser")]
        BasicUser = 10,

        // Admin panel
        [Display(GroupName = "AdminPanel", Name = "Can Access Admin Panel", Description = "Can Access Admin Panel")]
        AccessAdminPanel = 50,

        // Auction
        [Display(GroupName = "Auction", Name = "Create new", Description = "Can create a new auction item")]
        CreateAuction = 100,

        // AuctionCreator
        [Display(GroupName = "AuctionCreator", Name = "Alter own auction", Description = "Can change own auction item details")]
        ChangeOwnAuction = 150,
        [Display(GroupName = "AuctionCreator", Name = "Remove own auction", Description = "Can remove own auction items")]
        RemoveOwnAuction = 151,

        // PageAdministrator  
        [Display(GroupName = "PageAdministrator", Name = "Alter auctions", Description = "Can change auction item details for all auctions")]
        ChangeAuction = 200,
        [Display(GroupName = "PageAdministrator", Name = "Remove auctions", Description = "Can remove all auctions")]
        RemoveAuction = 201,

        /// <summary>
        /// This is a special Permission used by the SuperAdmin user. A user has this permission can access any other permission.
        /// </summary>
        [Display(GroupName = "SuperAdmin", Name = "AccessAll", Description = "This allows the user to access every feature")]
        AccessAll = Int16.MaxValue,
    }
}