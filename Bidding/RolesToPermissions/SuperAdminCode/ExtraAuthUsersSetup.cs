using Bidding.Database.Contexts;
using DataLayer.ExtraAuthClasses;
using PermissionParts;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Test")]

namespace ServiceLayer.UserServices.Internal
{
    /// <summary>
    /// These contain the individual methods to add/update the database, BUT you should call SaveChanges to update the database after using
    /// (This is different to AspNetUserExtension, where the userManger updates the database immediately)
    /// </summary>
    internal class ExtraAuthUsersSetup
    {
        private readonly BiddingContext _context;

        public ExtraAuthUsersSetup(BiddingContext context)
        {
            _context = context;
        }

        /// <summary>
        /// This adds a role if not present, or updates a role if is present.
        /// </summary>
        /// <param name="roleName"></param>
        /// <param name="description"></param>
        /// <param name="permissions"></param>
        public void AddUpdateRoleToPermissions(string roleName, string description, ICollection<Permission> permissions)
        {
            var status = RoleToPermissions.CreateRoleWithPermissions(roleName, description, permissions, _context);
            if (status.IsValid)
                //Note that CreateRoleWithPermissions will return a invalid status if the role is already present.
                _context.Add(status.Result);
            else
            {
                UpdateRole(roleName, description, permissions);
            }
        }

        /// <summary>
        /// This will update a role
        /// </summary>
        /// <param name="roleName"></param>
        /// <param name="description"></param>
        /// <param name="permissions"></param>
        public void UpdateRole(string roleName, string description, ICollection<Permission> permissions)
        {
            var existingRole = _context.Find<RoleToPermissions>(roleName);
            if (existingRole == null)
                throw new KeyNotFoundException($"Could not find the role {roleName} to update.");
            existingRole.Update(description, permissions);
        }
    }
}