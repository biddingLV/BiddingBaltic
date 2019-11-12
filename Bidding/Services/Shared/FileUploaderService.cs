using Bidding.Repositories.Shared;
using Bidding.Shared.ErrorHandling.Errors;
using Bidding.Shared.Exceptions;
using Bidding.Shared.Utility.Validation.Comparers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Bidding.Services.Shared
{
    public class FileUploaderService
    {
        private const int MAX_FILE_SIZE = 10000000; // ~ 10mb

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
            if (images.IsNotSpecified()) throw new WebApiException(HttpStatusCode.BadRequest, FileUploadErrorMessages.MissingFileInformation);
            if (images.Count > 30) throw new WebApiException(HttpStatusCode.BadRequest, FileUploadErrorMessages.MaxImageLimitReached);

            bool status = false;

            try
            {
                CloudBlobContainer cloudBlobContainer = await GetCloudBlobContainer();

                foreach (var image in images)
                {
                    ValidateImage(image);

                    var fileName = GetFileName(image);
                    var imageName = GenerateImageName(fileName);
                    var imageBytes = ConvertImageToByteArray(image);
                    var imageUrl = await UploadImageByteArray(imageBytes, imageName, image.ContentType, cloudBlobContainer);

                    status = true;
                }
            }
            catch (Exception ex)
            {
                throw new WebApiException(HttpStatusCode.BadRequest, FileUploadErrorMessages.MissingFileInformation, ex);
            }

            return status;
        }

        private void ValidateImage(IFormFile image)
        {
            if (!m_contentTypeMap.ContainsValue(image.ContentType)) throw new WebApiException(HttpStatusCode.BadRequest, "Could not upload image, unsupported file type.");
            if (image.Length > MAX_FILE_SIZE) throw new WebApiException(HttpStatusCode.BadRequest, "Could not upload image, file too large.");
            if (image.Length == 0) throw new WebApiException(HttpStatusCode.BadRequest, "Could not upload image, file empty.");
            string fileExtension = Path.GetExtension(image.FileName).Trim().Trim('.').ToLower();
            if (!m_contentTypeMap.ContainsKey(fileExtension)) throw new WebApiException(HttpStatusCode.BadRequest, "Could not upload image, unsupported file extension.");
        }

        private string GetFileName(IFormFile image)
        {
            return ContentDispositionHeaderValue.Parse(image.ContentDisposition).FileName.Trim('"');
        }

        private string GenerateImageName(string fileName)
        {
            return $"{Guid.NewGuid().ToString()}{Path.GetExtension(fileName)}";
        }

        private byte[] ConvertImageToByteArray(IFormFile inputImage)
        {
            byte[] result = null;

            try
            {
                var imageStream = inputImage.OpenReadStream();

                Stream thumbnailStream = new MemoryStream((int)imageStream.Length);
                thumbnailStream.Position = 0;

                using (Image<Rgba32> image = Image.Load(imageStream))
                {
                    var thumbnailRate = GetThumbnailRate(image.Width, 100);

                    image.Mutate(x => x
                         .Resize(1500, 0, true)); // image.Width / thumbnailRate || image.Height / thumbnailRate

                    imageStream.Position = 0;
                    var imageFormat = Image.DetectFormat(imageStream);
                    if (imageFormat != null)
                    {
                        image.Save(thumbnailStream, imageFormat);
                    }
                    else
                    {
                        image.Save(thumbnailStream, new JpegEncoder());
                    }

                }

                thumbnailStream.Position = 0;

                using (MemoryStream ms = new MemoryStream())
                {
                    thumbnailStream.CopyTo(ms);
                    result = ms.ToArray();
                }
            }
            catch (Exception ex)
            {

            }

            return result;
        }

        private int GetThumbnailRate(int originailWidth, int estimatedThumbnailWith)
        {
            var thumbnailRate = originailWidth / estimatedThumbnailWith;
            if (thumbnailRate == 0)
            {
                thumbnailRate = 1;
            }

            return thumbnailRate;
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

        private Dictionary<string, string> m_contentTypeMap = new Dictionary<string, string>()
        {
            { "png", "image/png" },
            { "jpg", "image/jpg" },
            { "jpeg", "image/jpeg" }
        };

        //private List<ImageFormat> m_supportedImageFormats = new List<ImageFormat>()
        //{
        //    ImageFormat.Jpeg,
        //    ImageFormat.Png,
        //    ImageFormat.Gif
        //};
    }
}
