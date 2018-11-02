using System.Threading.Tasks;
using Identity.Management.Client;
using Identity.Management.Client.Users;
using Identity.Management.Tests.Common;
using Xunit;

namespace Identity.Management.Tests
{
    [Collection(IdentityManagementCollection.Name)]
    public class UsersTests
    {
        private readonly IdentityManagementClient _client;

        public UsersTests(IdentityManagementFixture fixture)
        {
            fixture.ResetData();

            _client = fixture.CreateClient();
        }

        [Fact]
        public async Task ShouldAddUser()
        {
            await _client.AddUserAsync(new UserModel
            {
                Email = "bill@identity.com",
                Password = "0nly-Valid-on-add",
                FirstName = "Bob",
                LastName = "Identity",
                UserName = "bob.identity"
            });

            var users = await _client.GetUsersAsync();
            Assert.Equal(2, users.Length);
        }

        [Fact]
        public async Task ShouldGetUserUsingId()
        {
            var id = await _client.AddUserAsync(new UserModel
            {
                Email = "bboppin@b-bops.com",
                Password = "$strongPassw0rd",
                FirstName = "bob",
                LastName = "Poppin",
                UserName = "bboppin"
            });

            var user = await _client.GetUserByIdAsync(id);
            Assert.Equal("bboppin@b-bops.com", user.Email);
            Assert.Null(user.Password);
            Assert.Equal("bob", user.FirstName);
            Assert.Equal("Poppin", user.LastName);
            Assert.Equal("bboppin", user.UserName);
        }
    }
}