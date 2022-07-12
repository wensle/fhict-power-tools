namespace FhictPowerTools.FakeImplementations
{
    public class CredentialsRepositoryMockBuilder
    {
        private string _username = "";
        
        public CredentialsRepositoryMockBuilder AddUsername(string username)
        {
            _username = username;
            return this;
        }

        private string _password = "";

        public CredentialsRepositoryMockBuilder AddPassword(string password)
        {
            _password = password;
            return this;
        }

        private bool _userSettingsFileExists;

        public CredentialsRepositoryMockBuilder AddUserSettingsFile()
        {
            _userSettingsFileExists = true;
            return this;
        }

        public CredentialsRepositoryMock Build()
        {
            return new CredentialsRepositoryMock(_username, _password, _userSettingsFileExists);
        }
    }
}