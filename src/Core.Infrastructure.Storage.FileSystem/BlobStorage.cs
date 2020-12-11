using System.IO;
using System.Threading.Tasks;

namespace Core.Infrastructure.Storage.FileSystem
{
    public class BlobStorage : IBlobStorage
    {
        private readonly string _basePath;

        public BlobStorage(string basePath)
        {
            _basePath = basePath;
        }

        public async Task UploadAsync(string containerName, string blobName, Stream stream)
        {
            var containerPath = Path.Combine(_basePath, containerName);
            if( !Directory.Exists(containerPath))
            {
                Directory.CreateDirectory(containerPath);
            }

            var blobPath = Path.Combine(containerPath, blobName);

            using (var fs = new FileStream(blobPath, FileMode.OpenOrCreate))
            {
                await stream.CopyToAsync(fs);
                fs.Flush();
            }
        }
    }
}
