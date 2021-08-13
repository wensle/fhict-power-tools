using System.Collections.Generic;

namespace FhictPowerTools.Core.FtpClient
{
    public interface IFtpClient
    {
        public void DeleteFilesInRemoteDirectory(string remotePath);
        public IEnumerable<string> ListRemoteFiles();
    }
}