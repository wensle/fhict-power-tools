using Newtonsoft.Json.Linq;

namespace FhictPowerTools.Infrastructure.DataStores
{
    public interface IUserDataStore
    {
        bool Exists();
        void Create();
        JObject Get();
        void Write(JObject userDataStore);
        void Delete();
    }
}