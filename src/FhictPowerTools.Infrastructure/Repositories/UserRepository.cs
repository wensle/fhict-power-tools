using System;
using FhictPowerTools.Core.Repositories;
using FhictPowerTools.Infrastructure.DataStores;
using Microsoft.AspNetCore.DataProtection;
using Newtonsoft.Json.Linq;

namespace FhictPowerTools.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IUserDataStore _userDataStore;

        private readonly IDataProtector _dataProtector;

        public UserRepository(IUserDataStore userDataStore, IDataProtectionProvider dataProtector)
        {
            _userDataStore = userDataStore;
            _dataProtector = dataProtector.CreateProtector("FhictAccount");
        }

        public string GetUsername()
        {
            if (!_userDataStore.Exists()) return "";
            JObject userDataStore = _userDataStore.Get();
            return userDataStore["user"]?["username"]?.Value<string>();
        }

        public string GetPassword()
        {
            JObject userDataStore = _userDataStore.Get();
            string protectedPassword = userDataStore["user"]?["protectedPassword"]?.Value<string>() ?? throw new NullReferenceException();
            return _dataProtector.Unprotect(protectedPassword);
        }

        public void SetPassword(string password)
        {
            JObject userDataStore = _userDataStore.Get();
            string protectedPassword = _dataProtector.Protect(password);
            if (userDataStore["user"]?["protectedPassword"] is null) throw new NullReferenceException();
            userDataStore["user"]["protectedPassword"] = JToken.FromObject(protectedPassword);
            _userDataStore.Write(userDataStore);
        }

        public void SetUsername(string username)
        {
            JObject userDataStore = _userDataStore.Get();
            if (userDataStore["user"]?["protectedPassword"] is null) throw new NullReferenceException();
            userDataStore["user"]["username"] = JToken.FromObject(username);
            _userDataStore.Write(userDataStore);
        }
    }
}