﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bidding.Shared.Permissions
{
    /// <summary>
    /// This is the code that calculates what feature permissions a user has
    /// </summary>
    public class CalcAllowedPermissions
    {
        private readonly ExtraAuthorizeDbContext _context;

        public CalcAllowedPermissions(ExtraAuthorizeDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// This is called if the Permissions that a user needs calculating.
        /// It looks at what permissions the user has, and then filters out any permissions
        /// they aren't allowed because they haven't get access to the module that permission is linked to.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>a string containing the packed permissions</returns>
        public async Task<string> CalcPermissionsForUserAsync(string userId)
        {
            //This gets all the permissions, with a distinct to remove duplicates
            var permissionsForUser = (await _context.UserToRoles.Where(x => x.UserId == userId)
                .Select(x => x.Role.PermissionsInRole)
                .ToListAsync())
                //Because the permissions are packed we have to put these parts of the query after the ToListAsync()
                .SelectMany(x => x).Distinct();

            //we get the modules this user is allowed to see
            var userModules = _context.ModulesForUsers.Find(userId)
                    ?.AllowedPaidForModules ?? PaidForModules.None;
            //Now we remove permissions that are linked to modules that the user has no access to
            var filteredPermissions =
                from permission in permissionsForUser
                let moduleAttr = typeof(Permissions).GetMember(permission.ToString())[0]
                    .GetCustomAttribute<LinkedToModuleAttribute>()
                where moduleAttr == null || userModules.HasFlag(moduleAttr.PaidForModule)
                select permission;

            return filteredPermissions.PackPermissionsIntoString();
        }

    }
}
