using System;
using System.IO;
using FhictPowerTools.Core.Repositories;
using FhictPowerTools.Infrastructure.DataStores;
using Microsoft.AspNetCore.DataProtection;
using Newtonsoft.Json.Linq;

namespace FhictPowerTools.Infrastructure.Repositories
{
    public class CredentialsRepository : ICredentialsRepository
    {
        private readonly ICredentialsDataStore _credentialsDataStore;

        private readonly IDataProtector _dataProtector;

        public CredentialsRepository(ICredentialsDataStore credentialsDataStore, IDataProtectionProvider dataProtector)
        {
            _credentialsDataStore = credentialsDataStore;
            _dataProtector = dataProtector.CreateProtector("FhictAccount");
        }

        private void CreateCredentialDataStoreIfNotExistent()
        {
            if (!_credentialsDataStore.Exists) _credentialsDataStore.Create();
        }

        public string GetUsername()
        {
            CreateCredentialDataStoreIfNotExistent();
            JObject credentialsDataStore = _credentialsDataStore.Get();
            return credentialsDataStore["credentials"]?["username"]?.Value<string>();
        }

        public string GetPassword()
        {
            CreateCredentialDataStoreIfNotExistent();
            JObject credentialsDataStore = _credentialsDataStore.Get();
            string protectedPassword = credentialsDataStore["credentials"]?["protectedPassword"]?.Value<string>() ?? throw new NullReferenceException();
            return _dataProtector.Unprotect(protectedPassword);
        }

        public void SetPassword(string password)
        {
            CreateCredentialDataStoreIfNotExistent();
            JObject credentialsDataStore = _credentialsDataStore.Get();
            string protectedPassword = _dataProtector.Protect(password);
            if (credentialsDataStore["credentials"]?["protectedPassword"] is null) throw new NullReferenceException();
            credentialsDataStore["credentials"]["protectedPassword"] = JToken.FromObject(protectedPassword);
            _credentialsDataStore.Write(credentialsDataStore);
        }

        public void SetUsername(string username)
        {
            CreateCredentialDataStoreIfNotExistent();
            JObject credentialsDataStore = _credentialsDataStore.Get();
            if (credentialsDataStore["credentials"]?["protectedPassword"] is null) throw new NullReferenceException();
            credentialsDataStore["credentials"]["username"] = JToken.FromObject(username);
            _credentialsDataStore.Write(credentialsDataStore);
        }

        public void DeleteCredentials()
        {
            if (!_credentialsDataStore.Exists) throw new FileNotFoundException("usersettings.json file does not exist");
            JObject credentialsDataStore = _credentialsDataStore.Get();
            if (credentialsDataStore["credentials"]?["protectedPassword"] is null) throw new NullReferenceException();
            credentialsDataStore["credentials"]["username"] = JToken.FromObject("");
            credentialsDataStore["credentials"]["password"] = JToken.FromObject("");
            _credentialsDataStore.Write(credentialsDataStore);
        }
    }
}