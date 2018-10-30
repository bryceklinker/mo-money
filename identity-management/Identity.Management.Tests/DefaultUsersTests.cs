using System.Net;
using System.Threading.Tasks;
using Identity.Management.Server.Clients;
using Identity.Management.Server.Users;
using Identity.Management.Tests.Common;
using IdentityModel.Client;
using Xunit;

namespace Identity.Management.Tests
{
    [Collection(IdentityManagementCollection.Name)]
    public class DefaultUsersTests
    {
        private readonly TokenClient _client;
        private readonly string _identityBaseUrl;

        public DefaultUsersTests(IdentityManagementFixture fixture)
        {
            var clientId = DefaultClientsConfig.IdentityClient.ClientId;
            _identityBaseUrl = fixture.IdentityBaseUrl.AbsoluteUri;
            _client = new TokenClient($"{_identityBaseUrl}connect/token", clientId, "Mo.Money.Identity.Secret");
        }
        
        [Fact]
        public async Task ShouldAllowLoginUsingDefaultAdminUser()
        {
            var username = DefaultUsersConfig.AdminUser.UserName;
            var password = DefaultUsersConfig.AdminPassword;
            var response = await _client.RequestResourceOwnerPasswordAsync(username, password);
            
            Assert.Equal(HttpStatusCode.OK, response.HttpStatusCode);
            await JwtTokenAssert.IsValidAsync(response.AccessToken, _identityBaseUrl);
        }
    }
}