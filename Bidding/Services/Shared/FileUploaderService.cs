using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Bidding.Repositories.Shared;
using Bidding.Services.Shared.Permissions;
using Bidding.Shared.ErrorHandling.Errors;
using Bidding.Shared.Exceptions;
using Bidding.Shared.Utility.Validation.Comparers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
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

        private readonly string _connectionString;
        private readonly IConfiguration _configuration;
        private readonly FileUploaderRepository _fileUploaderRepository;
        private readonly PermissionService _permissionService;

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
            { "pdf", "application/pdf" },
            { "doc", "application/msword"},
            { "docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document"}
        };

        public FileUploaderService(IConfiguration configuration, FileUploaderRepository fileUploaderRepository, PermissionService permissionService)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _connectionString = _configuration["AzureStorage:ConnectionString"];
            _fileUploaderRepository = fileUploaderRepository ?? throw new ArgumentNullException(nameof(fileUploaderRepository));
            _permissionService = permissionService ?? throw new ArgumentNullException(nameof(permissionService));
        }

        public bool ValidateFiles(IFormFileCollection files)
        {
            if (files.IsNotSpecified()) throw new WebApiException(HttpStatusCode.BadRequest, FileUploadErrorMessage.MissingFileInformation);
            if (files.Count > 30) throw new WebApiException(HttpStatusCode.BadRequest, FileUploadErrorMessage.MaxFileLimitReached);

            foreach (IFormFile file in files)
            {
                ValidateFile(file);
            }

            return true;
        }

        public async Task<bool> UploadFilesAsync(IFormFileCollection files, int auctionId)
        {
            if (ValidateFiles(files) == false) throw new WebApiException(HttpStatusCode.BadRequest, FileUploadErrorMessage.GenericUploadErrorMessage);

            try
            {
                BlobServiceClient blobServiceClient = new BlobServiceClient(_connectionString);
                string containerName = "auctionfiles-" + Guid.NewGuid().ToString();
                BlobContainerClient blobContainerClient = await blobServiceClient.CreateBlobContainerAsync(containerName, PublicAccessType.BlobContainer);

                foreach (IFormFile file in files)
                {
                    Stream fileStream = file.OpenReadStream();
                    string fileName = CreateSafeFileName(file);

                    await blobContainerClient.UploadBlobAsync(fileName, fileStream);
                }

                int loggedInUserId = _permissionService.GetAndValidateUserId();

                return await _fileUploaderRepository.SaveAuction(containerName, auctionId, loggedInUserId).ConfigureAwait(true);
            }
            catch (Exception ex)
            {
                throw new WebApiException(HttpStatusCode.InternalServerError, FileUploadErrorMessage.GenericUploadErrorMessage, ex);
            }
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
            var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName;
            var urlPattern = "[^a-zA-Z0-9-.]";
            var friendlyUrl = Regex.Replace(fileName, @"\s", "-").ToLower();
            fileName = Regex.Replace(friendlyUrl, urlPattern, string.Empty);

            return fileName;
        }
    }
}
