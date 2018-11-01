using System.Threading.Tasks;
using Identity.Management.Client;
using Identity.Management.Client.ApiResources;
using Identity.Management.Tests.Common;
using Xunit;

namespace Identity.Management.Tests
{
    [Collection(IdentityManagementCollection.Name)]
    public class ApiResourcesTests
    {
        private readonly IdentityManagementClient _client;

        public ApiResourcesTests(IdentityManagementFixture fixture)
        {
            fixture.ResetData();

            _client = fixture.CreateClient();
        }

        [Fact]
        public async Task ShouldAllowTheCreationOfApiResources()
        {
            await _client.AddApiResourceAsync(new ApiResourceModel
            {
                Name = "the.new.api",
                DisplayName = "The New Api"
            });

            var resources = await _client.GetApiResourcesAsync();
            Assert.Equal(2, resources.Length);
        }

        [Fact]
        public async Task ShouldGetNewlyCreatedApiResource()
        {
            var id = await _client.AddApiResourceAsync(new ApiResourceModel
            {
                Name = "bob.bob",
                DisplayName = "jack"
            });

            var resource = await _client.GetApiResourceAsync(id);
            Assert.Equal("bob.bob", resource.Name);
            Assert.Equal("jack", resource.DisplayName);
        }
    }
}