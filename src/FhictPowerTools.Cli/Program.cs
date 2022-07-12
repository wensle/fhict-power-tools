using System;
using System.Collections.Generic;
using FhictPowerTools.Cli.Commands;
using FhictPowerTools.Cli.Settings;
using FhictPowerTools.Core.FtpClient;
using FhictPowerTools.Core.Repositories;
using FhictPowerTools.Core.VpnClient;
using FhictPowerTools.Infrastructure.DataStores;
using FhictPowerTools.Infrastructure.FhictFtp;
using FhictPowerTools.Infrastructure.FhictVpn;
using FhictPowerTools.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Spectre.Console.Cli;

namespace FhictPowerTools.Cli
{
    internal static class Program
    {
        private static int Main(string[] args)
        {
            CreateConfigurationBuilder().Build();
            ConfigureJsonSerializerSettings();
            IServiceCollection serviceCollection = CreateServiceCollection();
            return SpectreCliApp(serviceCollection, args);
        }

        private static int SpectreCliApp(IServiceCollection serviceCollection, IEnumerable<string> args)
        {
            TypeRegistrar registrar = new(serviceCollection);
            CommandApp app = new(registrar);

            app.Configure(config =>
            {
                config.AddBranch<CredentialsSettings>("credentials", credentials =>
                {
                    credentials.AddCommand<CredentialsSaveCommand>("save");
                    credentials.AddCommand<CredentialsDeleteCommand>("delete");
                    credentials.AddCommand<CredentialsVerifyCommand>("verify");
                });
            });
            return app.Run(args);
        }

        private static void ConfigureJsonSerializerSettings()
        {
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
        }

        private static IConfigurationBuilder CreateConfigurationBuilder()
        {
            Environment.SetEnvironmentVariable("DOTNET_ENVIRONMENT", "Development");
            string env = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ??
                         throw new NullReferenceException("Environment variable NETCORE_ENVIRONMENT is not set");

            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env}.json", true, true)
                .AddEnvironmentVariables();
                
            return configurationBuilder;
        }
        
        private static IServiceCollection CreateServiceCollection()
        {
            IServiceCollection serviceCollection = new ServiceCollection()
                .AddOptions()
                .AddScoped<IVpnClient, VpnClient>()
                .AddScoped<ICredentialsDataStore, CredentialsDataStore>()
                .AddScoped<ICredentialsRepository, CredentialsRepository>()
                .AddScoped<IVpnHostRepository, VpnHostRepository>()
                .AddScoped<IFtpClient, FtpClient>();
            serviceCollection.AddDataProtection();
                
            return serviceCollection;
        }
    }
}
