// Copyright (c) 2019 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using System;
using System.ComponentModel.DataAnnotations;
using PermissionParts;

namespace DataLayer.ExtraAuthClasses
{
    /// <summary>
    /// This holds what modules a user or tenant has
    /// </summary>
    public class ModulesForUser
    {
        /// <summary>
        /// This links modules to a user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="allowedPaidForModules"></param>
        public ModulesForUser(int userId, PaidForModules allowedPaidForModules)
        {
            UserId = userId;
            AllowedPaidForModules = allowedPaidForModules;
        }

        [Key]
        [MaxLength(ExtraAuthConstants.UserIdSize)]
        public int UserId { get; private set; }

        public PaidForModules AllowedPaidForModules { get; private set; }
    }
}