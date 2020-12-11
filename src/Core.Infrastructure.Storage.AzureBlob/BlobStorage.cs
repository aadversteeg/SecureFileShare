using Azure.Storage.Blobs;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Core.Infrastructure.Storage.AzureBlob
{
    public class BlobStorage : IBlobStorage
    {
        private readonly string _connectionString;

        public BlobStorage(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task UploadAsync(string containerName, string blobName, Stream stream)
        {
            var blobServiceClient = new BlobServiceClient(_connectionString);

            var containerClient = await blobServiceClient.CreateBlobContainerAsync(containerName);

            var blobClient = containerClient.Value.GetBlobClient(blobName);

            await blobClient.UploadAsync(stream);
        }
    }
}
