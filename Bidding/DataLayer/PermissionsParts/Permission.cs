// Copyright (c) 2018 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

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

        // BasicUser
        [Display(GroupName = "BasicUser", Name = "Read auction list", Description = "Can see auction list")]
        UseSearchOnAuctionList = 10,

        // Admin panel
        [Display(GroupName = "AdminPanel", Name = "Can Access Admin Panel", Description = "Can Access Admin Panel")]
        CanAccessAdminPanel = 200,
        [Display(GroupName = "AdminPanel", Name = "Change details for own auctions", Description = "Can create, update or delete own Auction details")]
        ChangeAuctionDetails = 105,

        // AuctionCreator
        [Display(GroupName = "AuctionCreator", Name = "Read advanced details for own auctions", Description = "Can see advanced details for own auctions")]
        ReadAdvancedDetailsForOwnAuction = 100,


        // PageAdministrator    


        /// <summary>
        /// This is a special Permission used by the SuperAdmin user. A user has this permission can access any other permission.
        /// </summary>
        [Display(GroupName = "SuperAdmin", Name = "AccessAll", Description = "This allows the user to access every feature")]
        AccessAll = Int16.MaxValue,
    }
}