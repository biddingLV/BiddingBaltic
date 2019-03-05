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
        private readonly string storageInfo;

        private readonly IConfiguration m_configuration;

        public FileUploaderController(IConfiguration configuration)
        {
            m_configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

            // Configuration
            storageInfo = configuration["StorageInfo"];
        }

        [HttpPost]
        public async Task<IActionResult> Upload([FromBody] List<IFormFile> files)
        {
            //// based on this - https://docs.microsoft.com/en-us/azure/storage/blobs/storage-upload-process-images?tabs=dotnet
            //// https://docs.microsoft.com/en-us/azure/storage/blobs/storage-quickstart-blobs-dotnet?tabs=windows
            //// old -> https://social.technet.microsoft.com/wiki/contents/articles/51791.azure-storage-c-create-container-upload-and-download-blobs.aspx
            //// example - https://github.com/Azure-Samples/storage-blobs-dotnet-webapp/blob/master/WebApp-Storage-DotNet/Controllers/HomeController.cs
            //// Create Reference to Azure Storage Account

            //// USER THIS - https://wakeupandcode.com/azure-blob-storage-from-asp-net-core-file-upload/
            //String strorageconn = storageInfo;
            //CloudStorageAccount storageacc = CloudStorageAccount.Parse(strorageconn);

            ////Create Reference to Azure Blob
            //CloudBlobClient blobClient = storageacc.CreateCloudBlobClient();

            ////The next 2 lines create if not exists a container named "democontainer"
            //CloudBlobContainer container = blobClient.GetContainerReference("auctionpictures");

            //// todo: jrb: do we need this?
            //await container.CreateIfNotExistsAsync();

            ////The next 7 lines upload the file test.txt with the name DemoBlob on the container "democontainer"
            //CloudBlockBlob blockBlob = container.GetBlockBlobReference("DemoBlob");
            //using (var filestream = System.IO.File.OpenRead(@""))
            //{

            //    await blockBlob.UploadFromStreamAsync(filestream);

            //}

            //return Ok();

            var uploadSuccess = false;

            foreach (var formFile in files)
            {
                if (formFile.Length <= 0)
                {
                    continue;
                }

                // OPTION B: read directly from stream for blob upload      
                using (var stream = formFile.OpenReadStream())
                {
                    uploadSuccess = await UploadToBlob(formFile.FileName, stream);
                }

            }

            //if (uploadSuccess)
            //    return View("UploadSuccess");
            //else
            //    return View("UploadError");
            return Ok();
        }

        private async Task<bool> UploadToBlob(string filename, Stream stream = null)
        {
            // Check whether the connection string can be parsed.
            if (CloudStorageAccount.TryParse(storageInfo, out CloudStorageAccount storageAccount))
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

                    // Get a reference to the blob address, then upload the file to the blob.
                    CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(filename);

                    if (stream != null)
                    {
                        // OPTION B: pass in memory stream directly
                        await cloudBlockBlob.UploadFromStreamAsync(stream);
                    }
                    else
                    {
                        return false;
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

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetImages()
        {
            String strorageconn = storageInfo;
            CloudStorageAccount storageacc = CloudStorageAccount.Parse(strorageconn);

            //Create Reference to Azure Blob
            CloudBlobClient blobClient = storageacc.CreateCloudBlobClient();

            //The next 2 lines create if not exists a container named "democontainer"
            CloudBlobContainer container = blobClient.GetContainerReference("auctionpictures");

            await container.CreateIfNotExistsAsync();

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
