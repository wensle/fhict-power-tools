using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FhictPowerTools.Infrastructure.DataStores
{
    public class CredentialsDataStore : ICredentialsDataStore
    {
        private const string DataStoreFileName = "usersettings.json";

        public bool Exists => File.Exists(DataStoreFileName);

        public void Create()
        {
            File.WriteAllText(DataStoreFileName, JsonConvert.SerializeObject(new UserDataModel()));
        }

        public JObject Get()
        {
            return JObject.Parse(File.ReadAllText(DataStoreFileName));
        }

        public void Write(JObject userDataStore)
        {
            File.WriteAllText(DataStoreFileName, JsonConvert.SerializeObject(userDataStore));
        }

        public void Delete()
        {
            if (!File.Exists(DataStoreFileName)) return;
            File.Delete(DataStoreFileName);
        }
    }
}