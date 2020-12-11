using Microsoft.Extensions.DependencyInjection;

namespace Core.Infrastructure.Storage.AzureBlob
{
    public static class ServiceConfigurationExtensions
    {
        public static void AddAzureBlobStorage(this IServiceCollection services, string connectionString)
        {
            services.AddSingleton<IBlobStorage>(sp => new BlobStorage(connectionString));
        }
    }
}
