﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Bidding.Shared.Permissions
{
    // Thanks to https://korzh.com/blogs/net-tricks/aspnet-identity-store-user-data-in-claims
    public class AddPermissionsToUserClaims : UserClaimsPrincipalFactory<IdentityUser>
    {
        private readonly ExtraAuthorizeDbContext _extraAuthDbContext;

        public AddPermissionsToUserClaims(UserManager<IdentityUser> userManager, IOptions<IdentityOptions> optionsAccessor,
            ExtraAuthorizeDbContext extraAuthDbContext)
            : base(userManager, optionsAccessor)
        {
            _extraAuthDbContext = extraAuthDbContext;
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(IdentityUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            var userId = identity.Claims.GetUserIdFromClaims();
            var rtoPCalcer = new CalcAllowedPermissions(_extraAuthDbContext);
            identity.AddClaim(new Claim(PermissionConstants.PackedPermissionClaimType, await rtoPCalcer.CalcPermissionsForUserAsync(userId)));
            var dataKeyCalcer = new CalcDataKey(_extraAuthDbContext);
            identity.AddClaim(new Claim(DataAuthConstants.HierarchicalKeyClaimName, dataKeyCalcer.CalcDataKeyForUser(userId)));
            return identity;
        }
    }
}
