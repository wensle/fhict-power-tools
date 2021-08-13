using System;
using System.Collections.Generic;
using System.Linq;
using FhictPowerTools.Core.FtpClient;
using FhictPowerTools.Core.Repositories;
using WinSCP;

namespace FhictPowerTools.Infrastructure.FhictFtp
{
    public class FtpClient : IFtpClient
    {
        private readonly ICredentialsRepository _credentialsRepository;

        public FtpClient(ICredentialsRepository credentialsRepository)
        {
            _credentialsRepository = credentialsRepository;
        }

        public void DeleteFilesInRemoteDirectory(string remotePath)
        {
            try
            {
                SessionOptions sessionOptions = new()
                {
                    Protocol = Protocol.Ftp,
                    HostName = "venus.fhict.nl",
                    UserName = _credentialsRepository.GetUsername(),
                    Password = _credentialsRepository.GetPassword(),
                    FtpSecure = FtpSecure.Explicit
                };
                using Session session = new();
                session.Open(sessionOptions);
                List<RemoteFileInfo> remoteFileInfos = session
                    .EnumerateRemoteFiles(remotePath, "*", EnumerationOptions.AllDirectories).ToList();
                while (remoteFileInfos.Any())
                {
                    for (int i = remoteFileInfos.Count - 1; i >= 0; i--)
                    {
                        try
                        {
                            RemovalEventArgs removalEventArgs = session.RemoveFile(remoteFileInfos[i].FullName);
                            Console.WriteLine(removalEventArgs.FileName);
                            remoteFileInfos.RemoveAt(i);
                        }
                        catch (SessionRemoteException)
                        {
                            Console.WriteLine($"Failed to delete {remoteFileInfos[i].FullName}");
                        }
                    }
                }
                Console.WriteLine("Done deleting files");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public IEnumerable<string> ListRemoteFiles()
        {
            throw new NotImplementedException();
        }
    }
}