using System;
using FhictPowerTools.Cli.Settings;
using FhictPowerTools.Core.FtpClient;
using Spectre.Console;
using Spectre.Console.Cli;

namespace FhictPowerTools.Cli.Commands
{
    public class FtpFilesDeleteCommand : Command<FtpFilesDeleteSettings>
    {
        private readonly IFtpClient _ftpClient;

        public FtpFilesDeleteCommand(IFtpClient ftpClient)
        {
            _ftpClient = ftpClient;
        }

        public override int Execute(CommandContext context, FtpFilesDeleteSettings settings)
        {
            try
            {
                _ftpClient.DeleteFilesInRemoteDirectory(settings.Path);
                return 0;
            }
            catch (InvalidOperationException)
            {
                AnsiConsole.MarkupLine($"[red]✗[/] Cannot delete files in '{settings.Path}' because it does not exist.");
                return 1;
            }
        }
    }
}