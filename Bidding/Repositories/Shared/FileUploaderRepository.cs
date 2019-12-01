using Bidding.Models.Contexts;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
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

        public async Task<string> UploadFilesAsync(byte[] imageBytes, string imageName, string contentType, CloudBlobContainer cloudBlobContainer)
        {
            CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(imageName);

            cloudBlockBlob.Properties.ContentType = contentType;

            const int byteArrayStartIndex = 0;

            await cloudBlockBlob.UploadFromByteArrayAsync(
                imageBytes,
                byteArrayStartIndex,
                imageBytes.Length).ConfigureAwait(true);

            var imageFullUrlPath = cloudBlockBlob.Uri.ToString();

            return imageFullUrlPath;
        }
    }
}
