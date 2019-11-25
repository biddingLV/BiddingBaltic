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
    }
}
