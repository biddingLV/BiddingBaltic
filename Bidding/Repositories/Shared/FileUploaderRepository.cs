using Bidding.Models.Contexts;
using System;

namespace Bidding.Repositories.Shared
{
    public class FileUploaderRepository
    {
        private readonly BiddingContext m_context;

        public FileUploaderRepository(BiddingContext context)
        {
            m_context = context ?? throw new ArgumentNullException(nameof(context));
        }
    }
}
