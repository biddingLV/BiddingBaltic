using Bidding.Models.Contexts;
using Bidding.Models.ViewModels.Shared;
using System;
using System.Collections.Generic;

namespace Bidding.Repositories.Shared
{
    public class PermissionRepository
    {
        private readonly BiddingContext m_context;

        public PermissionRepository(BiddingContext context)
        {
            m_context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Check if the logged in user is still active
        /// </summary>
        /// <param name="loggedInUserId"></param>
        /// <returns></returns>
        public bool IsUserActive(int loggedInUserId)
        {
            // @Permissions: WIP!
            return true; // m_context.Users.Where(usr => usr.UserId == loggedInUserId && usr.Deleted == false).Any();
        }

        /// <summary>
        /// Returns logged in users role
        /// </summary>
        /// <param name="loggedInUserId"></param>
        /// <returns></returns>
        public IEnumerable<UserRoleResponseModel> GetUserRole(int loggedInUserId)
        {
            yield return new UserRoleResponseModel()
            {
                RoleId = 1,
                RoleName = "Admin"
            };

            // @Permissions: WIP!
            //return from usr in m_context.Users
            //       join rol in m_context.Roles on usr.RoleId equals rol.RoleId
            //       where usr.UserId == loggedInUserId && usr.Deleted == false
            //       select new UserRoleResponseModel()
            //       {
            //           RoleId = rol.RoleId,
            //           RoleName = rol.Name
            //       };
        }
    }
}
