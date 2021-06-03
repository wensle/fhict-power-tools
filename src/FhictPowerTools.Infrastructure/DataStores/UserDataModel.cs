using System.Collections.Generic;
using FhictPowerTools.Core.Entities;

namespace FhictPowerTools.Infrastructure.DataStores
{
    public class UserDataModel
    {
        public User User { get; } = new();
        public List<string> VpnHosts { get; } = new();
        
    }
}