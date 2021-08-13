using System.Text.RegularExpressions;
using Spectre.Console;
using Spectre.Console.Cli;

namespace FhictPowerTools.Cli.Settings
{
    public class FtpFilesDeleteSettings : FtpSettings
    {
        [CommandArgument(0, "<directory>")]
        public string Path { get; init; }
        
        public override ValidationResult Validate()
        {
            const string pattern = @"^\/[^\0]+";
            return Regex.Match(Path, pattern).Success 
                ? ValidationResult.Success() 
                : ValidationResult.Error("Path to directory is not valid.");
        }
        
    }
}