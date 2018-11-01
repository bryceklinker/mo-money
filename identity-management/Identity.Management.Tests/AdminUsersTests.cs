using System.Net;
using System.Threading.Tasks;
using Identity.Management.Client;
using Identity.Management.Server.Users;
using Identity.Management.Tests.Common;
using Xunit;

namespace Identity.Management.Tests
{
    [Collection(IdentityManagementCollection.Name)]
    public class AdminUserTests
    {
        private const string AdminPassword = DefaultUsersConfig.AdminPassword;
        private static readonly string AdminUserName = DefaultUsersConfig.AdminUser.UserName;
        private readonly string _identityBaseUrl;
        private readonly IdentityManagementClient _client;

        public AdminUserTests(IdentityManagementFixture fixture)
        {
            fixture.ResetData();
            
            _identityBaseUrl = fixture.IdentityBaseUrl.AbsoluteUri;
            _client = fixture.CreateClient();
        }
        
        [Fact]
        public async Task ShouldAllowLoginUsingDefaultAdminUser()
        {
            var response = await _client.GetUserAccessTokenAsync(AdminUserName, AdminPassword);
            
            Assert.Equal(HttpStatusCode.OK, response.HttpStatusCode);
            await JwtTokenAssert.IsValidAsync(response.AccessToken, _identityBaseUrl);
        }
    }
}