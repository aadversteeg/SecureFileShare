using System.IO;
using System.Threading.Tasks;

namespace Core.Infrastructure.Storage
{
    public interface IBlobStorage
    {
        Task UploadAsync(string containerName, string blobName, Stream stream);
    }
}
