using Spectre.Console.Cli;

namespace FhictPowerTools.Cli.Settings
{
    public class VpnCommandSettings : CommandSettings
    {
        
    }

    public class VpnConnectCommandSettings : VpnCommandSettings
    {
        [CommandArgument(0, "[host]")]
        public string Host { get; set; }
    }
}