using System.Collections.Generic;
using FhictPowerTools.Core.Repositories;

namespace FhictPowerTools.FakeImplementations
{
    public class CredentialsRepositoryStub : ICredentialsRepository
    {
        private readonly Dictionary<string, string> _dictionary;

        public CredentialsRepositoryStub()
        {
            _dictionary = new Dictionary<string, string>
            {
                {"password", ""},
                {"username", ""}
            };
        }

        public string GetUsername()
        {
            return _dictionary["username"];
        }

        public string GetPassword()
        {
            return _dictionary["password"];
        }

        public void SetPassword(string password)
        {
            _dictionary["password"] = password;
        }

        public void SetUsername(string username)
        {
            _dictionary["username"] = username;
        }
    }
}