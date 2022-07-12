using System.Threading.Tasks;
using FhictPowerTools.Cli.Settings;
using FhictPowerTools.Core.VpnClient;
using Spectre.Console;
using Spectre.Console.Cli;

namespace FhictPowerTools.Cli.Commands
{
    public class CredentialsVerifyCommand : AsyncCommand<CredentialVerifySettings>
    {
        private readonly IVpnClient _vpnClient;

        public CredentialsVerifyCommand(IVpnClient vpnClient)
        {
            _vpnClient = vpnClient;
        }

        public override async Task<int> ExecuteAsync(CommandContext context, CredentialVerifySettings settings)
        {
            _vpnClient.Connect("vdi.fhict.nl");
            if (_vpnClient.IsConnected())
            {
                AnsiConsole.Markup($"[green]✓[/] Verification complete [grey]({UserSettingsFileHelper.GetAbsoulteFilePath()})[/]");
                return await Task.FromResult(0);
            }
            AnsiConsole.Markup("[red]✓[/] Invalid username or password. Please save them again.");
            return await Task.FromResult(1);

        }
    }
}