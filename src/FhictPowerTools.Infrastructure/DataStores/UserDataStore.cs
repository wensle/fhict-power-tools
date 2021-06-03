using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FhictPowerTools.Infrastructure.DataStores
{
    public class UserDataStore : IUserDataStore
    {
        private const string DefaultFilename = "userdata.json";

        public bool Exists()
        {
            return File.Exists(DefaultFilename);
        }

        public void Create()
        {
            File.WriteAllText(DefaultFilename, JsonConvert.SerializeObject(new UserDataModel()));
        }

        public JObject Get()
        {
            return JObject.Parse(File.ReadAllText(DefaultFilename));
        }

        public void Write(JObject userDataStore)
        {
            File.WriteAllText(DefaultFilename, JsonConvert.SerializeObject(userDataStore));
        }

        public void Delete()
        {
            if (!File.Exists(DefaultFilename)) return;
            File.Delete(DefaultFilename);
        }
    }
}