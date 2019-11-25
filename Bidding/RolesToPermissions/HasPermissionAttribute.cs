using Microsoft.AspNetCore.Authorization;
using PermissionParts;
using System;

namespace FeatureAuthorize.PolicyCode
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = false)]
    public class HasPermissionAttribute : AuthorizeAttribute
    {
        public HasPermissionAttribute(Permission permission) : base(permission.ToString())
        { }
    }
}