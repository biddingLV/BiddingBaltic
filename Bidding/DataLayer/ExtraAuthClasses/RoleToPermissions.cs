﻿using PermissionParts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DataLayer.ExtraAuthClasses
{
    /// <summary>
    /// This holds each Roles, which are mapped to Permissions
    /// </summary>
    public class RoleToPermissions
    {
        [Required(AllowEmptyStrings = false)] //A role must have at least one role in it
        private string _permissionsInRole;

        private RoleToPermissions() { }

        /// <summary>
        /// This creates the Role with its permissions
        /// </summary>
        /// <param name="roleName"></param>
        /// <param name="description"></param>
        /// <param name="permissions"></param>
        public RoleToPermissions(string roleName, string description, ICollection<Permission> permissions)
        {
            RoleName = roleName;
            Update(description, permissions);
        }

        /// <summary>
        /// ShortName of the role
        /// </summary>
        [Key]
        [Required(AllowEmptyStrings = false)]
        [MaxLength(ExtraAuthConstants.RoleNameSize)]
        public string RoleName { get; private set; }

        /// <summary>
        /// A human-friendly description of what the Role does
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public string Description { get; private set; }

        /// <summary>
        /// This returns the list of permissions in this role
        /// </summary>
        public string PermissionsInRole => _permissionsInRole;

        public void Update(string description, ICollection<Permission> permissions)
        {
            if (permissions == null || !permissions.Any())
                throw new InvalidOperationException("There should be at least one permission associated with a role.");

            _permissionsInRole = string.Join(",", permissions);
            Description = description;
        }
    }
}