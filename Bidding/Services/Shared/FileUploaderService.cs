using Bidding.Shared.ErrorHandling.Errors;
using Bidding.Shared.Exceptions;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Bidding.Services.Shared
{
    public class FileUploaderService
    {
        private readonly string _containerName = "auctionpictures"; // todo: kke: improve naming here?
        private readonly string _connectionString;

        private readonly IConfiguration m_configuration;

        public FileUploaderService(IConfiguration configuration)
        {
            m_configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _connectionString = configuration.GetConnectionString("StorageAccount");
        }

        public async Task UploadImage()
        {

        }

        public async Task UploadImages()
        {

        }

        private async Task<CloudBlockBlob> UploadImageAsync(byte[] imageByteArray, string blobName)
        {
            CloudBlobContainer cloudBlobContainer = await GetAuctionImageContainerAsync();

            var cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(blobName);

            // await cloudBlockBlob.UploadFromByteArrayAsync()

            return cloudBlockBlob;

        }

        private async Task<CloudBlobContainer> GetAuctionImageContainerAsync()
        {
            if (CloudStorageAccount.TryParse(_connectionString, out CloudStorageAccount cloudStorageAccount))
            {
                CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();

                CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(_containerName);

                // todo: kke: is this right?
                await cloudBlobContainer.CreateIfNotExistsAsync(
                    BlobContainerPublicAccessType.Blob, null, null
                );

                return cloudBlobContainer;
            }
            else
            {
                // todo: kke: improve this!
                throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.IncorrectAuction);
            }
        }

        // todo: kke: when to call this?
        private async Task<bool> CheckIfBlobExistsAsync(string blobName)
        {
            CloudBlobContainer cloudBlobContainer = await GetAuctionImageContainerAsync();

            var cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(blobName);

            return await cloudBlockBlob.ExistsAsync();
        }
    }
}
