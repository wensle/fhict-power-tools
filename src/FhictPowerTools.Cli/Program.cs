using System;
using FhictPowerTools.Core.FhictFtp;
using FhictPowerTools.Core.FhictVpn;
using FhictPowerTools.Core.Repositories;
using FhictPowerTools.Infrastructure.DataStores;
using FhictPowerTools.Infrastructure.FhictFtp;
using FhictPowerTools.Infrastructure.FhictVpn;
using FhictPowerTools.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace FhictPowerTools.Cli
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            IConfigurationRoot configurationRoot = CreateConfigurationBuilder().Build();
            ServiceProvider serviceProvider = CreateServiceCollection(configurationRoot).BuildServiceProvider();
            ConfigureJsonSerializerSettings();
            IUserDataStore userDataStore = serviceProvider.GetService<IUserDataStore>() ?? throw new NullReferenceException();
            userDataStore.Delete();
            if (!userDataStore.Exists()) userDataStore.Create();
            IUserRepository userRepository =
                serviceProvider.GetService<IUserRepository>() ?? throw new NullReferenceException();
            userRepository.SetPassword("sultry-overhung-pox4-nimble");
            Console.WriteLine(userRepository.GetPassword());
            userRepository.SetUsername("I453297");
            IVpnHostRepository vpnHostRepository = serviceProvider.GetService<IVpnHostRepository>() ?? throw new NullReferenceException();
            vpnHostRepository.AddHost("vdi.fhict.nl");
            vpnHostRepository.AddHost("seclab.fhict.nl");
            IFhictVpn fhictVpn = serviceProvider.GetService<IFhictVpn>() ?? 
                                               throw new NullReferenceException(
                                                   "Could not find {nameof(IFhictVpn)}");
            fhictVpn.Connect("vdi.fhict.nl");
            Console.WriteLine($"Is VPN connected? {fhictVpn.IsConnected()}");
            fhictVpn.Disconnect();
            Console.WriteLine($"Is VPN connected? {fhictVpn.IsConnected()}");
            IFhictFtp fhictFtp = serviceProvider.GetService<IFhictFtp>() ?? throw new NullReferenceException();
            fhictFtp.RemoveRemoteFiles();
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
        
        private static IServiceCollection CreateServiceCollection(IConfiguration configurationRoot)
        {
            IServiceCollection serviceCollection = new ServiceCollection()
                .AddOptions()
                .AddScoped<IFhictVpn, FhictVpn>()
                .AddScoped<IUserDataStore, UserDataStore>()
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IVpnHostRepository, VpnHostRepository>()
                .AddScoped<IFhictFtp, FhictFtp>();
            serviceCollection.AddDataProtection();
                
            return serviceCollection;
        }
    }
}
