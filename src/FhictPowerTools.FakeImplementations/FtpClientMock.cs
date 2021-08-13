using System;
using System.Collections.Generic;
using FhictPowerTools.Core.FtpClient;

namespace FhictPowerTools.Cli.Tests.FakeImplementations
{
    public class FtpClientMock : IFtpClient
    {
        private readonly List<string> _files = new()
        {
            "ClientApp",
            "runtimes",
            "Ananas.Api.deps.json",
            "Ananas.Api.dll",
            "Ananas.Api.exe",
            "Ananas.Api.pdb",
            "Ananas.Api.runtimeconfig.json",
            "Ananas.Client.deps.json",
            "Ananas.Client.dll",
            "Ananas.Client.exe",
            "Ananas.Client.pdb",
            "Ananas.Client.runtimeconfig.json",
            "Ananas.Client.Views.dll",
            "Ananas.Client.Views.pdb",
            "Ananas.Core.dll",
            "Ananas.Core.pdb",
            "Ananas.Infrastructure.dll",
            "Ananas.Infrastructure.pdb",
            "appsettings.Development.json",
            "appsettings.json",
            "AutoMapper.dll",
            "AutoMapper.Extensions.Microsoft.DependencyInjection.dll",
            "BCrypt.Net-Next.dll",
            "BouncyCastle.Crypto.dll",
            "Dapper.dll",
            "Google.Protobuf.dll",
            "Hashids.net.dll",
            "K4os.Compression.LZ4.dll",
            "K4os.Compression.LZ4.Streams.dll",
            "K4os.Hash.xxHash.dll",
            "Microsoft.AspNetCore.Cryptography.Internal.dll",
            "Microsoft.AspNetCore.Cryptography.KeyDerivation.dll",
            "Microsoft.AspNetCore.SpaServices.Extensions.dll",
            "Microsoft.Extensions.Identity.Core.dll",
            "Microsoft.Extensions.Identity.Stores.dll",
            "Microsoft.OpenApi.dll",
            "MySql.Data.dll",
            "Newtonsoft.Json.dll",
            "Swashbuckle.AspNetCore.Swagger.dll",
            "Swashbuckle.AspNetCore.SwaggerGen.dll",
            "Swashbuckle.AspNetCore.SwaggerUI.dll",
            "System.Configuration.ConfigurationManager.dll",
            "System.Security.Cryptography.ProtectedData.dll",
            "Ubiety.Dns.Core.dll",
            "web.config",
            "Zstandard.Net.dll"
        };
        public void DeleteFilesInRemoteDirectory(string remotePath)
        {
            // Mocking the behavior of not finding a path
            if (remotePath != "/domains/this-path-exists")
            {
                throw new InvalidOperationException(
                    @$"Cannot find path '{remotePath}' because it does not exist.");
            }

            _files.Clear();
        }

        public IEnumerable<string> ListRemoteFiles()
        {
            return _files;
        }
    }
}