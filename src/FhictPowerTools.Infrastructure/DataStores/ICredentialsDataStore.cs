using Newtonsoft.Json.Linq;

namespace FhictPowerTools.Infrastructure.DataStores
{
    public interface ICredentialsDataStore
    {
        bool Exists { get; }
        void Create();
        JObject Get();
        void Write(JObject userDataStore);
        void Delete();
    }
}