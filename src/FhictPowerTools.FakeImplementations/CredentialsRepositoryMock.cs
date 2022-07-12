using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using FhictPowerTools.Core.Repositories;

namespace FhictPowerTools.FakeImplementations
{
    public class  CredentialsRepositoryMock : ICredentialsRepository
    {
        public Dictionary<string, string> Dictionary { get; set; }
        public bool UserSettingsFileExists { get; set; }
        
        public CredentialsRepositoryMock()
        {
            Dictionary = new Dictionary<string, string>
            {
                {"password", ""},
                {"username", ""}
            };
            UserSettingsFileExists = false;
        }

        public CredentialsRepositoryMock(string username, string password, bool userSettingFileExists)
        {
            Dictionary = new Dictionary<string, string>
            {
                {"password", username},
                {"username", password}
            };
            UserSettingsFileExists = userSettingFileExists;
        }

        public CredentialsRepositoryMock WithPassword(string password)
        {
            Dictionary["password"] = password;
            return this;
        }
        
        public CredentialsRepositoryMock WithUsername(string username)
        {
            Dictionary["username"] = username;
            return this;
        }

        public CredentialsRepositoryMock WithUserSettingsFile()
        {
            UserSettingsFileExists = true;
            return this;
        }

        public string GetUsername()
        {
            return Dictionary["username"];
        }

        public string GetPassword()
        {
            return Dictionary["password"];
        }

        public void SetPassword(string password)
        {
            Dictionary["password"] = password;
        }

        public void SetUsername(string username)
        {
            Dictionary["username"] = username;
        }

        public void DeleteCredentials()
        {
            if (!UserSettingsFileExists)
            {
                throw new FileNotFoundException("usersettings.json does not exist");
            }
            Dictionary["username"] = "";
            Dictionary["password"] = "";
        }
    }
}