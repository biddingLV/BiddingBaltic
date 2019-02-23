using System;
using System.Collections.Generic;
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
    public class ImageStorageController : ControllerBase
    {
        private readonly string storageInfo;

        private readonly IConfiguration m_configuration;

        public ImageStorageController(IConfiguration configuration)
        {
            m_configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

            // Configuration
            storageInfo = configuration["StorageInfo"];
        }

        [AllowAnonymous] // todo: kke: remove after testing
        [HttpGet]
        public async Task<IActionResult> Upload()
        {
            // based on this - https://docs.microsoft.com/en-us/azure/storage/blobs/storage-upload-process-images?tabs=dotnet
            // https://docs.microsoft.com/en-us/azure/storage/blobs/storage-quickstart-blobs-dotnet?tabs=windows
            // old -> https://social.technet.microsoft.com/wiki/contents/articles/51791.azure-storage-c-create-container-upload-and-download-blobs.aspx
            // example - https://github.com/Azure-Samples/storage-blobs-dotnet-webapp/blob/master/WebApp-Storage-DotNet/Controllers/HomeController.cs
            // Create Reference to Azure Storage Account
            String strorageconn = storageInfo;
            CloudStorageAccount storageacc = CloudStorageAccount.Parse(strorageconn);

            //Create Reference to Azure Blob
            CloudBlobClient blobClient = storageacc.CreateCloudBlobClient();

            //The next 2 lines create if not exists a container named "democontainer"
            CloudBlobContainer container = blobClient.GetContainerReference("auctionpictures");

            // todo: jrb: do we need this?
            await container.CreateIfNotExistsAsync();

            //The next 7 lines upload the file test.txt with the name DemoBlob on the container "democontainer"
            CloudBlockBlob blockBlob = container.GetBlockBlobReference("DemoBlob");
            using (var filestream = System.IO.File.OpenRead(@""))
            {

                await blockBlob.UploadFromStreamAsync(filestream);

            }

            return Ok();
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
