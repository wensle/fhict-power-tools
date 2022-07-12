using System;
using System.Collections.Generic;
using FhictPowerTools.Core.Repositories;
using FhictPowerTools.Infrastructure.DataStores;
using Newtonsoft.Json.Linq;

namespace FhictPowerTools.Infrastructure.Repositories
{
    public class VpnHostRepository : IVpnHostRepository
    {
        private readonly ICredentialsDataStore _credentialsDataStore;

        public VpnHostRepository(ICredentialsDataStore credentialsDataStore)
        {
            _credentialsDataStore = credentialsDataStore;
        }

        public void AddHost(string host)
        {
            JObject userDataStore = _credentialsDataStore.Get();
            List<string> hosts = userDataStore["vpnHosts"]?.ToObject<List<string>>() ?? throw new NullReferenceException();
            if (hosts.Exists(s => s == host)) return;
            hosts.Add(host);
            userDataStore["vpnHosts"] = JToken.FromObject(hosts);
            _credentialsDataStore.Write(userDataStore);
        }

        public void RemoveHost(string host)
        {
            JObject userDataStore = _credentialsDataStore.Get();
            List<string> hosts = userDataStore["vpnHosts"]?.ToObject<List<string>>() ?? throw new NullReferenceException();
            hosts.Remove(host);
            userDataStore["vpnHosts"] = JToken.FromObject(hosts);
            _credentialsDataStore.Write(userDataStore);
        }

        public List<string> GetHosts()
        {
            if (!_credentialsDataStore.Exists) return new List<string>();
            JObject userDataStore = _credentialsDataStore.Get();
            return userDataStore["vpnHosts"]?.Value<List<string>>();
        }
    }
}