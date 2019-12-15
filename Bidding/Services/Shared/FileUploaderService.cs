using Bidding.Repositories.Shared;
using Bidding.Services.Shared.Permissions;
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
using SixLabors.Primitives;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Bidding.Services.Shared
{
    public class FileUploaderService
    {
        private const int MAX_FILE_SIZE = 10000000; // ~ 10mb

        private readonly string m_azureStorageConnectionString;
        private readonly IConfiguration m_configuration;
        private readonly FileUploaderRepository m_fileUploaderRepository;
        private readonly PermissionService m_permissionService;

        private readonly Dictionary<string, List<byte[]>> m_fileSignature = new Dictionary<string, List<byte[]>>
        {
            { "doc", new List<byte[]> { new byte[] { 0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1 } } },
            { "docx", new List<byte[]> { new byte[] { 0x50, 0x4B, 0x03, 0x04 } } },
            { "pdf", new List<byte[]> { new byte[] { 0x25, 0x50, 0x44, 0x46 } } },
            { "png", new List<byte[]> { new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A } } },
            { "jpg", new List<byte[]> {
                new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
                new byte[] { 0xFF, 0xD8, 0xFF, 0xE1 },
                new byte[] { 0xFF, 0xD8, 0xFF, 0xE8 }
            }},
            { "jpeg", new List<byte[]> {
                new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
                new byte[] { 0xFF, 0xD8, 0xFF, 0xE2 },
                new byte[] { 0xFF, 0xD8, 0xFF, 0xE3 }
            }}
        };

        private readonly Dictionary<string, string> m_contentTypeMap = new Dictionary<string, string>()
        {
            { "png", "image/png" },
            { "jpg", "image/jpg" },
            { "jpeg", "image/jpeg" },
            { "pdf", "application/pdf" }
        };

        public FileUploaderService(IConfiguration configuration, FileUploaderRepository fileUploaderRepository, PermissionService permissionService)
        {
            m_configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            m_azureStorageConnectionString = m_configuration["AzureStorage:ConnectionString"];
            m_fileUploaderRepository = fileUploaderRepository ?? throw new ArgumentNullException(nameof(fileUploaderRepository));
            m_permissionService = permissionService ?? throw new ArgumentNullException(nameof(permissionService));
        }

        public bool ValidateFiles(IFormFileCollection files)
        {
            if (files.IsNotSpecified()) throw new WebApiException(HttpStatusCode.BadRequest, FileUploadErrorMessage.MissingFileInformation);
            if (files.Count > 30) throw new WebApiException(HttpStatusCode.BadRequest, FileUploadErrorMessage.MaxFileLimitReached);

            foreach (IFormFile item in files)
            {
                ValidateFile(item);
            }

            return true;
        }

        public async Task<bool> UploadFilesAsync(IFormFileCollection files, int auctionId)
        {
            if (ValidateFiles(files) == false) throw new WebApiException(HttpStatusCode.BadRequest, FileUploadErrorMessage.GenericUploadErrorMessage);

            try
            {
                CloudBlobContainer cloudBlobContainer = await GetCloudBlobContainer().ConfigureAwait(true);

                foreach (IFormFile file in files)
                {
                    // todo: kke: add back & validate file signature logic!
                    // string fileExtension = GetFileExtension(file);
                    // ValidateFileSignature(fileExtension, imageBytes); // todo: kke: why this fails for jpg?

                    string fileName = CreateSafeFileName(file);
                    byte[] fileBytes = ConvertFileToByteArray(file);

                    await m_fileUploaderRepository.UploadFileAsync(fileBytes, fileName, file.ContentType, cloudBlobContainer).ConfigureAwait(true);
                    //Stream imageStream = file.OpenReadStream();

                    //using (MemoryStream optimiziedImageStream = new MemoryStream())
                    //using (Image<Rgba32> image = Image.Load(imageStream))
                    //{
                    // todo: kke: var imageFormat = Image.DetectFormat(imageStream);	!!!
                    //    Image<Rgba32> clone = image.Clone(context => context
                    //        .Resize(new ResizeOptions
                    //        {
                    //            Mode = ResizeMode.Max,
                    //            Size = new Size(1280, 1280)
                    //        }));

                    //    clone.Save(optimiziedImageStream, new JpegEncoder { Quality = 70 });

                    //    await m_fileUploaderRepository.UploadFileAsync(optimiziedImageStream, fileName, file.ContentType, cloudBlobContainer).ConfigureAwait(true);
                    //}
                }

                int loggedInUserId = m_permissionService.GetUserIdFromClaimsPrincipal();

                return await m_fileUploaderRepository.SaveAuction(cloudBlobContainer.Name, auctionId, loggedInUserId).ConfigureAwait(true);
            }
            catch (Exception ex)
            {
                throw new WebApiException(HttpStatusCode.InternalServerError, FileUploadErrorMessage.GenericUploadErrorMessage, ex);
            }
        }

        private byte[] ConvertFileToByteArray(IFormFile file)
        {
            byte[] result = null;

            try
            {
                var stream1 = file.OpenReadStream();

                //Stream thumbnailStream = new MemoryStream((int)stream1.Length)	
                //{	
                //    Position = 0	
                //};	

                //using (Image<Rgba32> image = Image.Load(imageStream))	
                //{	
                //    var thumbnailRate = GetThumbnailRate(image.Width, 100);	

                //    image.Mutate(x => x	
                //         .Resize(1500, 0, true)); // image.Width / thumbnailRate || image.Height / thumbnailRate	

                //    imageStream.Position = 0;	

                //    var imageFormat = Image.DetectFormat(imageStream);	

                //    if (imageFormat != null)	
                //    {	
                //        image.Save(thumbnailStream, imageFormat);	
                //    }	
                //    else	
                //    {	
                //        image.Save(thumbnailStream, new JpegEncoder());	
                //    }	

                //}	

                stream1.Position = 0;

                using (MemoryStream ms = new MemoryStream())
                {
                    stream1.CopyTo(ms);
                    result = ms.ToArray();
                }
            }
            catch (Exception ex)
            {
                throw new WebApiException(HttpStatusCode.InternalServerError, FileUploadErrorMessage.GenericUploadErrorMessage, ex);
            }

            return result;
        }

        private void ValidateFile(IFormFile file)
        {
            if (!m_contentTypeMap.ContainsValue(file.ContentType)) throw new WebApiException(HttpStatusCode.BadRequest, FileUploadErrorMessage.UnsupportedFileType);
            if (file.Length == 0) throw new WebApiException(HttpStatusCode.BadRequest, FileUploadErrorMessage.FileEmpty);
            if (file.Length > MAX_FILE_SIZE) throw new WebApiException(HttpStatusCode.BadRequest, FileUploadErrorMessage.FileTooLarge);

            string fileExtension = GetFileExtension(file);
            if (!m_contentTypeMap.ContainsKey(fileExtension)) throw new WebApiException(HttpStatusCode.BadRequest, FileUploadErrorMessage.UnsupportedFileExtension);
            if (!m_fileSignature.ContainsKey(fileExtension)) throw new WebApiException(HttpStatusCode.BadRequest, FileUploadErrorMessage.UnsupportedFileExtension);
        }

        private void ValidateFileSignature(string fileExtension, byte[] imageBytes)
        {
            List<byte[]> sig = m_fileSignature[fileExtension];
            foreach (byte[] b in sig)
            {
                var curFileSig = new byte[b.Length];
                Array.Copy(imageBytes, curFileSig, b.Length);
                if (!curFileSig.SequenceEqual(b)) throw new WebApiException(HttpStatusCode.BadRequest, FileUploadErrorMessage.UnsupportedFileType);
            }
        }

        private string GetFileExtension(IFormFile file)
        {
            return Path.GetExtension(file.FileName).Trim().Trim('.').ToLower();
        }

        private string CreateSafeFileName(IFormFile file)
        {
            // note: kke: TEST THIS
            var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName;
            var urlPattern = "[^a-zA-Z0-9-.]";
            var friendlyUrl = Regex.Replace(fileName, @"\s", "-").ToLower();
            fileName = Regex.Replace(friendlyUrl, urlPattern, string.Empty);
            // note: kke: TEST THIS

            return fileName;
        }

        private async Task<CloudBlobContainer> GetCloudBlobContainer()
        {
            if (CloudStorageAccount.TryParse(m_azureStorageConnectionString, out CloudStorageAccount cloudStorageAccount))
            {
                CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
                CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference("auctionfiles-" + Guid.NewGuid().ToString());

                // todo: kke: add more robust try catch using (StorageException e)
                await cloudBlobContainer.CreateIfNotExistsAsync().ConfigureAwait(true);
                await cloudBlobContainer.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob }).ConfigureAwait(true);

                return cloudBlobContainer;
            }
            else
            {
                throw new WebApiException(HttpStatusCode.InternalServerError, FileUploadErrorMessage.GenericUploadErrorMessage);
            }
        }
    }
}
