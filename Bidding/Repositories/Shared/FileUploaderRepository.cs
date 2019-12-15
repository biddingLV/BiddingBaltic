using Bidding.Models.Contexts;
using Bidding.Models.DatabaseModels.Auctions;
using Bidding.Shared.ErrorHandling.Errors;
using Bidding.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Bidding.Repositories.Shared
{
    public class FileUploaderRepository
    {
        private readonly BiddingContext m_context;

        public FileUploaderRepository(BiddingContext context)
        {
            m_context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task UploadFileAsync(MemoryStream fileStream, string fileName, string fileContentType, CloudBlobContainer cloudBlobContainer)
        {
            // todo: kke: if file upload fails we need to delete auction from database!!! data corroption problem!
            try
            {
                CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(fileName);

                cloudBlockBlob.Properties.ContentType = fileContentType;

                fileStream.Position = 0;

                using (var stream = fileStream)
                {
                    await cloudBlockBlob.UploadFromStreamAsync(stream).ConfigureAwait(true);
                }
            }
            catch (Exception ex)
            {
                throw new WebApiException(HttpStatusCode.InternalServerError, FileUploadErrorMessage.GenericUploadErrorMessage, ex);
            }
        }

        public async Task<bool> SaveAuction(string containerName, int auctionId, int loggedInUserId)
        {
            try
            {
                Auction auction = await m_context.Auctions.Where(auct => auct.AuctionId == auctionId).SingleOrDefaultAsync().ConfigureAwait(true);

                auction.AuctionImageContainer = containerName;
                auction.LastUpdatedAt = DateTime.UtcNow;
                auction.LastUpdatedBy = loggedInUserId;

                m_context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new WebApiException(HttpStatusCode.InternalServerError, FileUploadErrorMessage.GenericUploadErrorMessage, ex);
            }

            return true;
        }
    }
}
