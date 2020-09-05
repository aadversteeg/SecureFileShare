using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Core.FunctionApp
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var currentDirectory = "/home/site/wwwroot";

            bool isLocal = string.IsNullOrEmpty(System.Environment.GetEnvironmentVariable("WEBSITE_INSTANCE_ID"));
            if (isLocal)
            {
                currentDirectory = System.Environment.CurrentDirectory;
            }

            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(currentDirectory)
                .AddJsonFile("appsettings.json");

            var serviceProvider = builder.Services.BuildServiceProvider();
            var configurationRoot = serviceProvider.GetService<IConfiguration>();
            configurationBuilder
                .AddConfiguration(configurationRoot)
                .AddEnvironmentVariables();

            // build the config in order to access the appsettings for getting the key vault connection settings
            var config = configurationBuilder.Build();
            var keyVaultName = config["KeyVault:Application"];
            System.Console.WriteLine($"Using key vault: \"{keyVaultName}\".");

            configurationBuilder.AddAzureKeyVault($"https://{keyVaultName}.vault.azure.net");


            // build the config again so it has the key vault provider
            config = configurationBuilder.Build();

            // replace the existing config with the new one
            builder.Services.Replace(ServiceDescriptor.Singleton(typeof(IConfiguration), config));
        }
    }
}
