using Microsoft.Extensions.DependencyInjection;

namespace Core.Infrastructure.Storage.FileSystem
{
    public static class ServiceConfigurationExtensions
    {
        public static void AddFileSystemBlobStorage(this IServiceCollection services, string basePath)
        {
            services.AddSingleton<IBlobStorage>(sp => new BlobStorage(basePath));
        }
    }
}
