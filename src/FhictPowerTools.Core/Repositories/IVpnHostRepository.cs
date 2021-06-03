using System.Collections.Generic;

namespace FhictPowerTools.Core.Repositories
{
    public interface IVpnHostRepository
    {
        public void AddHost(string host);
        public void RemoveHost(string host);
        public List<string> GetHosts();
    }
}