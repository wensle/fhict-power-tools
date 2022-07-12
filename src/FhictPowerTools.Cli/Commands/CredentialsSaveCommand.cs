using FhictPowerTools.Cli.Settings;
using FhictPowerTools.Core.Repositories;
using Spectre.Console;
using Spectre.Console.Cli;

namespace FhictPowerTools.Cli.Commands
{
    public class CredentialsSaveCommand : Command<CredentialsSaveSettings>
    {
        private readonly ICredentialsRepository _credentialsRepository;

        public CredentialsSaveCommand(ICredentialsRepository credentialsRepository)
        {
            _credentialsRepository = credentialsRepository;
        }
        
        public override int Execute(CommandContext context, CredentialsSaveSettings settings)
        {
            if (_credentialsRepository.GetPassword() != null || _credentialsRepository.GetPassword() != null)
            {
                if (!AnsiConsole.Confirm("You’ve already saved your some or both fhict credentials. Do you want to replace them?"))
                {
                    return 0;
                }
            }
            
            string username = AnsiConsole.Ask<string>("[green]?[/] Username:");
            string password = AnsiConsole.Prompt(
                new TextPrompt<string>("[green]?[/] Password:")
                    .PromptStyle("red")
                    .Secret());
            _credentialsRepository.SetUsername(username);
            _credentialsRepository.SetPassword(password);
            AnsiConsole.Markup("[green]✓[/] Saved your fhict credentials");
            return 0;
        }
    }
}