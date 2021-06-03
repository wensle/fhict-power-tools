using System;
using System.Collections.Generic;
using FhictPowerTools.Core.Repositories;
using FhictPowerTools.Infrastructure.DataStores;
using Newtonsoft.Json.Linq;

namespace FhictPowerTools.Infrastructure.Repositories
{
    public class VpnHostRepository : IVpnHostRepository
    {
        private readonly IUserDataStore _userDataStore;

        public VpnHostRepository(IUserDataStore userDataStore)
        {
            _userDataStore = userDataStore;
        }

        public void AddHost(string host)
        {
            JObject userDataStore = _userDataStore.Get();
            List<string> hosts = userDataStore["vpnHosts"]?.ToObject<List<string>>() ?? throw new NullReferenceException();
            if (hosts.Exists(s => s == host)) return;
            hosts.Add(host);
            userDataStore["vpnHosts"] = JToken.FromObject(hosts);
            _userDataStore.Write(userDataStore);
        }

        public void RemoveHost(string host)
        {
            JObject userDataStore = _userDataStore.Get();
            List<string> hosts = userDataStore["vpnHosts"]?.ToObject<List<string>>() ?? throw new NullReferenceException();
            hosts.Remove(host);
            userDataStore["vpnHosts"] = JToken.FromObject(hosts);
            _userDataStore.Write(userDataStore);
        }

        public List<string> GetHosts()
        {
            if (!_userDataStore.Exists()) return new List<string>();
            JObject userDataStore = _userDataStore.Get();
            return userDataStore["vpnHosts"]?.Value<List<string>>();
        }
    }
}