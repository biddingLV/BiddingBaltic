using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Bidding.Controllers.Shared
{
    [Produces("application/json")]
    [Route("api/[Controller]/[action]")]
    public class FileUploaderController : ControllerBase
    {
        private readonly string _containerName = "";
        private readonly string _connectionString;

        private readonly IConfiguration m_configuration;

        public FileUploaderController(IConfiguration configuration)
        {
            m_configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _connectionString = configuration["StorageInfo"];
        }

        [HttpPost]
        public async Task<IActionResult> UploadImages()
        {
            IFormFileCollection files = Request.Form.Files;
            //// USE THIS - https://wakeupandcode.com/azure-blob-storage-from-asp-net-core-file-upload/

            await UploadToBlob(files);

            return Ok();
        }

        private async Task<bool> UploadToBlob(IFormFileCollection files)
        {
            // Check whether the connection string can be parsed.
            if (CloudStorageAccount.TryParse(_connectionString, out CloudStorageAccount storageAccount))
            {
                try
                {
                    // Create the CloudBlobClient that represents the Blob storage endpoint for the storage account.
                    CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();

                    // Create a container called 'uploadblob' and append a GUID value to it to make the name unique. 
                    CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference("uploadblob" + Guid.NewGuid().ToString());
                    await cloudBlobContainer.CreateIfNotExistsAsync();

                    // Set the permissions so the blobs are public. 
                    BlobContainerPermissions permissions = new BlobContainerPermissions
                    {
                        PublicAccess = BlobContainerPublicAccessType.Blob
                    };
                    await cloudBlobContainer.SetPermissionsAsync(permissions);

                    foreach (var formFile in files)
                    {
                        // Get a reference to the blob address, then upload the file to the blob.
                        CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(formFile.FileName);

                        if (formFile.Length <= 0)
                        {
                            continue;
                        }

                        // OPTION B: read directly from stream for blob upload      
                        using (var stream = formFile.OpenReadStream())
                        {
                            if (stream != null)
                            {
                                //// OPTION B: pass in memory stream directly
                                await cloudBlockBlob.UploadFromStreamAsync(stream);
                            }
                            else
                            {
                                return false;
                            }
                        }

                    }

                    return true;
                }
                catch (StorageException ex)
                {
                    return false;
                }
                finally
                {
                    // OPTIONAL: Clean up resources, e.g. blob container
                    //if (cloudBlobContainer != null)
                    //{
                    //    await cloudBlobContainer.DeleteIfExistsAsync();
                    //}
                }
            }
            else
            {
                return false;
            }

        }

        [HttpGet]
        public async Task<IActionResult> GetImages()
        {
            string strorageconn = _connectionString;
            CloudStorageAccount storageacc = CloudStorageAccount.Parse(strorageconn);

            //Create Reference to Azure Blob
            CloudBlobClient blobClient = storageacc.CreateCloudBlobClient();

            //The next 2 lines create if not exists a container named "democontainer"
            CloudBlobContainer container = blobClient.GetContainerReference("uploadblobcec43629-42ca-4f17-a2f2-14a9dce93c96");

            CloudBlockBlob blockBlob = container.GetBlockBlobReference("DemoBlob");



            // todo: jrb: this fails!
            // todo: jrb: how to get the files?
            using (var filestream = System.IO.File.OpenWrite(@""))
            {
                await blockBlob.DownloadToStreamAsync(filestream);
            }

            return Ok();
        }
    }
}
