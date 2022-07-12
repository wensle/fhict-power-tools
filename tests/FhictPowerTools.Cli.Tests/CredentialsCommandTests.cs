using FhictPowerTools.Cli.Settings;
using Spectre.Console;
using Spectre.Console.Cli;
using Xunit;

namespace FhictPowerTools.Cli.Tests
{
    public class CredentialsCommandTests
    {
        [Fact]
        public void ShouldShowDescription() // TODO Don't do this. It just tests the Spectre.Console.Cli library
        {
            CommandApp app = new();
            app.Configure(config =>
            {
                config.AddBranch<CredentialsSettings>("credentials", credentials =>
                {
                    credentials.AddCommand<Commands.CredentialsSaveCommand>("save");
                });
            });
            
            AnsiConsole.Record();

            app.Run(new[] {"credentials"});

            AnsiConsole.ExportText();
        }
    }
}