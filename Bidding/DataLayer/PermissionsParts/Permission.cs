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
        ReadAuctionList = 10,
        [Display(GroupName = "BasicUser", Name = "Read basic auction details", Description = "Can see basic details for all auctions")]
        ReadBasicAuctionDetails = 15,
        [Display(GroupName = "BasicUser", Name = "Change own profile details", Description = "Can update own profile")]
        ChangeOwnProfile = 20,

        // AuctionCreator
        [Display(GroupName = "AuctionCreator", Name = "Read advanced details for own auctions", Description = "Can see advanced details for own auctions")]
        ReadAdvancedDetailsForOwnAuction = 50,
        [Display(GroupName = "AuctionCreator", Name = "Change details for own auctions", Description = "Can create, update or delete own Auction details")]
        ChangeOwnAuction = 51,

        /// <summary>
        /// This is a special Permission used by the SuperAdmin user. A user has this permission can access any other permission.
        /// </summary>
        [Display(GroupName = "SuperAdmin", Name = "AccessAll", Description = "This allows the user to access every feature")]
        AccessAll = Int16.MaxValue,
    }
}