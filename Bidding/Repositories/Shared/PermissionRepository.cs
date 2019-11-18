using Bidding.Models.Contexts;
using System;

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
    }
}
