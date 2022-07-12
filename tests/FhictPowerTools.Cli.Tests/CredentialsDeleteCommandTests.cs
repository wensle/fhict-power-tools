using System;
using System.Collections.Generic;
using FhictPowerTools.Cli.Commands;
using FhictPowerTools.Core.Repositories;
using FhictPowerTools.FakeImplementations;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;
using Spectre.Console.Cli;
using Spectre.Console.Testing;
using Xunit;

namespace FhictPowerTools.Cli.Tests
{
    public class CredentialsDeleteCommandTests
    {
        [Fact]
        public void ShouldDeleteCredentials()
        {
            CredentialsRepositoryMock mock = new CredentialsRepositoryMockBuilder()
                .AddPassword("hi-i-am-a-password")
                .AddUsername("hi-i-am-a-username")
                .AddUserSettingsFile()
                .Build();
            CommandApp app = new();
            app.Configure(configurator =>
            {
                configurator.Settings.Registrar.RegisterInstance<ICredentialsRepository>(mock);
                configurator.AddBranch("credentials", credentials =>
                {
                    credentials.AddCommand<CredentialsDeleteCommand>("delete");
                });
            });
            
            app.Run(new[] {"credentials", "delete"});
            Assert.Empty(mock.Dictionary["username"]);
            Assert.Empty(mock.Dictionary["password"]);
        }

        [Fact]
        public void ShouldReportOnSuccess()
        {
            CommandApp app = new();
            CredentialsRepositoryMock mock = new()
            {
                Dictionary = new Dictionary<string, string>
                {
                    {"password", "hi-i-am-a-password"}, {"username", "hi-i-am-a-username"}
                },
                UserSettingsFileExists = true
            };
            app.Configure(configurator =>
            {
                configurator.Settings.Registrar.RegisterInstance<ICredentialsRepository>(mock);
                configurator.AddBranch("credentials", credentials =>
                {
                    credentials.AddCommand<CredentialsDeleteCommand>("delete");
                });
            });
            AnsiConsole.Record();
            app.Run(new[] {"credentials", "delete"});
            
            string actual = AnsiConsole.ExportText();
            Assert.Contains("Deleted user credentials", actual);
        }

        [Fact]
        public void ShouldReportOnNotFindingUserSettingsFile()
        {
            CommandApp app = new();
            CredentialsRepositoryMock mock = new()
            {
                Dictionary = new Dictionary<string, string>
                {
                    {"password", "hi-i-am-a-password"}, {"username", "hi-i-am-a-username"}
                },
                UserSettingsFileExists = false
            };
            app.Configure(configurator =>
            {
                configurator.Settings.Registrar.RegisterInstance<ICredentialsRepository>(mock);
                configurator.AddBranch("credentials", credentials =>
                {
                    credentials.AddCommand<CredentialsDeleteCommand>("delete");
                });
            });
            AnsiConsole.Record();
            app.Run(new[] {"credentials", "delete"});
            
            string actual = AnsiConsole.ExportText();
            Assert.Contains("Credentials cannot be deleted because", actual);
        }
    }
}