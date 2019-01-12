using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bidding.Shared.Authorization
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public int PermissionId { get; }

        public PermissionRequirement(int permissionId) => PermissionId = permissionId;
    }
}
