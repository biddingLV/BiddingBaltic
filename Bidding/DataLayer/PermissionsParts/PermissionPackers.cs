// Copyright (c) 2018 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace PermissionParts
{
    public static class PermissionPackers
    {
        public static IEnumerable<Permission> UnpackPermissionsFromString(this string packedPermissions)
        {
            if (packedPermissions == null)
                throw new ArgumentNullException(nameof(packedPermissions));

            var xList = packedPermissions.Split(',');

            foreach (var item in xList)
            {
                if (!Enum.TryParse(item, true, out Permission permissionToCheck))
                    throw new InvalidEnumArgumentException($"{item} could not be converted to a {nameof(Permission)}.");

                yield return permissionToCheck;
            }
        }

        public static Permission? FindPermissionViaName(this string permissionName)
        {
            return Enum.TryParse(permissionName, out Permission permission)
                ? (Permission?) permission
                : null;
        }

    }
}