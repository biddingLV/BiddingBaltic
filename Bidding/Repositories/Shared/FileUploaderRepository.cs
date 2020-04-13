using Bidding.Models.Contexts;
using Bidding.Models.DatabaseModels.Auctions;
using Bidding.Shared.ErrorHandling.Errors;
using Bidding.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Bidding.Repositories.Shared
{
    public class FileUploaderRepository
    {
        private readonly BiddingContext _context;

        public FileUploaderRepository(BiddingContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> SaveAuction(string containerName, int auctionId, int loggedInUserId)
        {
            try
            {
                Auction auction = await _context.Auctions.Where(auct => auct.AuctionId == auctionId).SingleOrDefaultAsync().ConfigureAwait(true);

                if (auction != null)
                {
                    auction.AuctionImageContainer = containerName;
                    auction.LastUpdatedAt = DateTime.UtcNow;
                    auction.LastUpdatedBy = loggedInUserId;

                    _context.SaveChanges();
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new WebApiException(HttpStatusCode.InternalServerError, FileUploadErrorMessage.GenericUploadErrorMessage, ex);
            }

            return true;
        }
    }
}
