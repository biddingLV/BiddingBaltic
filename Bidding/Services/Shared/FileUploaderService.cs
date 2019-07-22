using Bidding.Models.ViewModels.Bidding.Shared.FileUpload;
using Bidding.Repositories.Shared;
using Bidding.Shared.ErrorHandling.Errors;
using Bidding.Shared.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Bidding.Services.Shared
{
    public class FileUploaderService
    {
        private readonly string m_azureStorageConnectionString;

        private readonly IConfiguration m_configuration;
        public readonly FileUploaderRepository m_fileUploaderRepository;

        public FileUploaderService(IConfiguration configuration, FileUploaderRepository fileUploaderRepository)
        {
            m_configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            m_azureStorageConnectionString = m_configuration["AzureStorage:ConnectionString"];
            m_fileUploaderRepository = fileUploaderRepository ?? throw new ArgumentNullException(nameof(fileUploaderRepository));
        }

        public async Task<CloudBlobContainer> GetCloudBlobContainer2()
        {
            // CHECK THIS OUT - https://forums.asp.net/t/2142260.aspx?Blob+Container+rename 

            if (CloudStorageAccount.TryParse(m_azureStorageConnectionString, out CloudStorageAccount cloudStorageAccount))
            {
                CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
                CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference("bidauctionimages-67107ed8-d71f-4130-8787-91c202e8434a");

                List<Uri> allBlobs = new List<Uri>();

                BlobContinuationToken token = null;
                do
                {
                    BlobResultSegment resultSegment = await cloudBlobContainer.ListBlobsSegmentedAsync(token);
                    token = resultSegment.ContinuationToken;
                    foreach (IListBlobItem blob in resultSegment.Results)
                    {
                        if (blob.GetType() == typeof(CloudBlockBlob))
                            allBlobs.Add(blob.Uri);
                    }
                }
                while (token != null);

                return cloudBlobContainer;
            }
            else
            {
                // todo: kke: improve this!
                throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.IncorrectAuction);
            }
        }

        public async Task<bool> Upload(IFormFileCollection images)
        {
            bool status = false;

            CloudBlobContainer cloudBlobContainer = await GetCloudBlobContainer();

            foreach (var image in images)
            {
                if (image.Length > 0)
                {
                    var fileName = GetFileName(image);

                    var imageName = GenerateImageName(fileName);

                    var imageBytes = ConvertImageToByteArray(image);

                    var imageUrl = await UploadImageByteArray(imageBytes, imageName, image.ContentType, cloudBlobContainer);
                }

                status = true;
            }

            return status;
        }

        private string GetFileName(IFormFile image)
        {
            return ContentDispositionHeaderValue.Parse(image.ContentDisposition).FileName.Trim('"');
        }

        private string GenerateImageName(string fileName)
        {
            return $"{Guid.NewGuid().ToString()}{Path.GetExtension(fileName)}";
        }

        private byte[] ConvertImageToByteArray(IFormFile image)
        {
            byte[] result = null;

            using (var fileStream = image.OpenReadStream())
            using (var memoryStream = new MemoryStream())
            {
                fileStream.CopyTo(memoryStream);
                result = memoryStream.ToArray();
            }

            return result;
        }

        private async Task<string> UploadImageByteArray(byte[] imageBytes, string imageName, string contentType, CloudBlobContainer cloudBlobContainer)
        {
            if (imageBytes == null || imageBytes.Length == 0)
            {
                return null;
            }

            var cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(imageName);

            cloudBlockBlob.Properties.ContentType = contentType;

            const int byteArrayStartIndex = 0;

            await cloudBlockBlob.UploadFromByteArrayAsync(
                imageBytes,
                byteArrayStartIndex,
                imageBytes.Length);

            var imageFullUrlPath = cloudBlockBlob.Uri.ToString();

            return imageFullUrlPath;
        }

        private async Task<CloudBlobContainer> GetCloudBlobContainer()
        {
            if (CloudStorageAccount.TryParse(m_azureStorageConnectionString, out CloudStorageAccount cloudStorageAccount))
            {
                CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
                CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference("bidauctionimages-" + Guid.NewGuid().ToString());
                await cloudBlobContainer.CreateIfNotExistsAsync();

                await cloudBlobContainer.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

                return cloudBlobContainer;
            }
            else
            {
                // todo: kke: improve this!
                throw new WebApiException(HttpStatusCode.BadRequest, AuctionErrorMessages.IncorrectAuction);
            }
        }
    }
}
