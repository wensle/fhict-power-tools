#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using FhictPowerTools.Cli.Commands;
using FhictPowerTools.Cli.Settings;
using FhictPowerTools.Cli.Tests.FakeImplementations;
using FhictPowerTools.Core.FtpClient;
using Moq;
using Spectre.Console;
using Spectre.Console.Cli;
using Xunit;

namespace FhictPowerTools.Cli.Tests
{
    public class FtpFilesDeleteCommandTests
    {
        
        private readonly IRemainingArguments _remainingArgs = new Mock<IRemainingArguments>().Object;
        
        [Fact]
        public void ShouldDeleteContentInRemoteDirectory()
        {
            IFtpClient ftpClient = new FtpClientMock();
            FtpFilesDeleteCommand command = new(ftpClient);
            CommandContext context = new(_remainingArgs, "delete", null);
            FtpFilesDeleteSettings settings = new(){ Path = @"/domains/this-path-exists"};
            AnsiConsole.Record();
            
            int result = command.Execute(context, settings);
            
            Assert.Equal(0, result);
            Assert.Empty(ftpClient.ListRemoteFiles());
        }

        [Fact]
        public void ShouldReportOnNotFindingRemoteDirectory()
        {
            IFtpClient ftpClient = new FtpClientMock();
            FtpFilesDeleteCommand command = new(ftpClient);
            CommandContext context = new(_remainingArgs, "delete", null);
            const string remotePath = @"/domains/i-am-random-path-fjdhasjkfhdsa/";
            FtpFilesDeleteSettings settings = new() { Path = remotePath};
            AnsiConsole.Record();
            
            int result = command.Execute(context, settings);

            // assert
            Assert.Equal(1, result);
            Assert.Contains(@$"Cannot delete files in '{remotePath}'", AnsiConsole.ExportText());
        }

        [Fact]
        public void ShouldValidateRemotePath()
        {
            // Given
            CommandApp app = new();
            app.Configure(config =>
            {
                config.PropagateExceptions();
                config.Settings.Registrar.Register<IFtpClient, FtpClientMock>();
                config.AddBranch<FtpSettings>("ftp", ftp =>
                {
                    ftp.AddBranch<FtpFilesSettings>("files", files =>
                    {
                        files.AddCommand<FtpFilesDeleteCommand>("delete");
                    });
                });
            });
            
            // When
            Exception result = Record.Exception(() => app.Run(new[] { "ftp", "files", "delete", "/"}))!;
            
            // Then
            Assert.IsType<CommandRuntimeException>(result);
            Assert.Equal("Path to directory is not valid.", result.Message);
        }
    }
}