using System;
using System.Collections.Generic;
using System.Linq;
using FhictPowerTools.Core.FhictFtp;
using FhictPowerTools.Core.Repositories;
using WinSCP;

namespace FhictPowerTools.Infrastructure.FhictFtp
{
    public class FhictFtp : IFhictFtp
    {
        private readonly IUserRepository _userRepository;

        public FhictFtp(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void RemoveRemoteFiles()
        {
            try
            {
                SessionOptions sessionOptions = new()
                {
                    Protocol = Protocol.Ftp,
                    HostName = "venus.fhict.nl",
                    UserName = _userRepository.GetUsername(),
                    Password = _userRepository.GetPassword(),
                    FtpSecure = FtpSecure.Explicit
                };
                using Session session = new();
                session.Open(sessionOptions);
                const string remotePath = "/domains/i453297.venus.fhict.nl/";
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
    }
}