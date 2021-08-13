using FhictPowerTools.Core.Repositories;
using FhictPowerTools.FakeImplementations;
using Xunit;

namespace FhictPowerTools.Core.Tests
{
    public class CredentialsRepositoryTests
    {
        private readonly ICredentialsRepository _credentialsRepository;

        public CredentialsRepositoryTests()
        {
            _credentialsRepository = new CredentialsRepositoryStub();
        }
        [Fact]
        public void ShouldSetPassword()
        {
            const string password = "sultry-overhung-pox4-nimble";
            _credentialsRepository.SetPassword(password);
            Assert.Equal(password, _credentialsRepository.GetPassword());
        }
        [Fact]
        public void ShouldFailSettingUsername()
        {
            const string username = "i453297";
            _credentialsRepository.SetPassword(username);
            Assert.NotEqual(username, _credentialsRepository.GetUsername());
        }
        [Fact]
        public void ShouldSetUsername()
        {
            const string username = "I453297";
            _credentialsRepository.SetUsername(username);
            Assert.Equal(username, _credentialsRepository.GetUsername());
        }
    }
}