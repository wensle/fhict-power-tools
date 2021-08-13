namespace FhictPowerTools.Core.Repositories
{
    public interface ICredentialsRepository
    {
        string GetUsername();
        string GetPassword();
        void SetPassword(string password);
        void SetUsername(string username);
    }
}