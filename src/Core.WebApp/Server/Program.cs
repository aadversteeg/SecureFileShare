using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Core.WebApp.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {

                    webBuilder.ConfigureAppConfiguration((context, config) =>
                    {
                        var keyVaultName = config.Build()["KeyVault:Application"];
                        Console.WriteLine($"Using key vault: \"{keyVaultName}\".");
                        config.AddAzureKeyVault($"https://{keyVaultName}.vault.azure.net");
                    });

                    webBuilder.UseStartup<Startup>();
                });
    }
}
