using FhictPowerTools.Cli.Settings;
using FhictPowerTools.Core.VpnClient;
using Spectre.Console.Cli;

namespace FhictPowerTools.Cli.Commands
{
    public class VpnConnectCommand : Command<VpnConnectCommandSettings>
    {
        private readonly IVpnClient _vpnClient;

        public VpnConnectCommand(IVpnClient vpnClient)
        {
            _vpnClient = vpnClient;
        }


        public override int Execute(CommandContext context, VpnConnectCommandSettings settings)
        {
            _vpnClient.Connect(settings.Host);
            return 0;
        }
    }
}