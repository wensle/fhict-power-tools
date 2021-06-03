namespace FhictPowerTools.Core.Repositories
{
    public interface IUserRepository
    {
        string GetUsername();
        string GetPassword();
        void SetPassword(string password);
        void SetUsername(string username);
    }
}