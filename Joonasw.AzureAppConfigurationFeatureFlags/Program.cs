using Azure.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;

namespace Joonasw.AzureAppConfigurationFeatureFlags
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
                    webBuilder.ConfigureAppConfiguration((hostingContext, configBuilder) =>
                    {
                        var config = configBuilder.Build();
                        var appSettings = config.GetSection("App").Get<AppSettings>();
                        configBuilder.AddAzureAppConfiguration(o =>
                        {
                            var credentialOptions = new DefaultAzureCredentialOptions();
                            if (!string.IsNullOrEmpty(appSettings.SharedTokenCacheTenantId))
                            {
                                // AAD tenant id to use in local environment
                                credentialOptions.SharedTokenCacheTenantId = appSettings.SharedTokenCacheTenantId;
                                //You can also specify the user to use locally
                                //credentialOptions.SharedTokenCacheUsername = "";
                            }

                            var credential = new DefaultAzureCredential(credentialOptions);

                            // Connect to app configuration with either the user or Managed Identity
                            o.Connect(new Uri(appSettings.AppConfigEndpoint), credential);
                            // Get feature flags and add them to config in format
                            // expected by the FeatureManagement library
                            o.UseFeatureFlags();
                            // Configure refresh interval for settings (default is 30 seconds if you leave it out)
                            o.ConfigureRefresh(r => r.SetCacheExpiration(TimeSpan.FromMinutes(30)));
                        });
                    });
                    webBuilder.UseStartup<Startup>();
                });
    }
}
