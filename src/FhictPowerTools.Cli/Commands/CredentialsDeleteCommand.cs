using System;
using System.IO;
using FhictPowerTools.Cli.Settings;
using FhictPowerTools.Core.Repositories;
using Spectre.Console;
using Spectre.Console.Cli;

namespace FhictPowerTools.Cli.Commands
{
    public class CredentialsDeleteCommand : Command<CredentialsDeleteSettings>
    {
        private readonly ICredentialsRepository _repository;

        public CredentialsDeleteCommand(ICredentialsRepository repository)
        {
            _repository = repository;
        }
        public override int Execute(CommandContext context, CredentialsDeleteSettings settings)
        {
            try
            {
                _repository.DeleteCredentials();
                AnsiConsole.MarkupLine("[green]✓[/] Deleted user credentials");
                return 0;
            }
            catch (FileNotFoundException)
            {
                AnsiConsole.MarkupLine("[red]![/] Credentials cannot be deleted because usersettings.json file was not found");
                return 1;
            }
        }
    }
}