using System.IO;

namespace FhictPowerTools.Cli
{
    public class UserSettingsFileHelper
    {
        private const string FilePath = "usersetings.json";

        internal static string GetAbsoulteFilePath()
        {
            return Path.GetFullPath(FilePath);
        }
    }
}