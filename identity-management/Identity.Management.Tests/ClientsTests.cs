using System;
using System.Threading.Tasks;
using Identity.Management.Client;
using Identity.Management.Client.Clients;
using Identity.Management.Tests.Common;
using Xunit;

namespace Identity.Management.Tests
{
    [Collection(IdentityManagementCollection.Name)]
    public class ClientsTests
    {
        private readonly IdentityManagementClient _client;

        public ClientsTests(IdentityManagementFixture fixture)
        {
            fixture.ResetData();
            
            _client = fixture.CreateClient();
        }
        
        [Fact]
        public async Task ShouldAllowAdminToCreateNewClients()
        {
            await _client.AddClientAsync(new ClientModel
            {
                ClientId = "Mo.Money.IdentityTesting",
                ClientSecrets = new[]
                {
                    new ClientSecretModel
                    {
                        Value = "Mo.Money.IdentityTesting.Secret" 
                    }
                },
                GrantTypes = SupportedGrantTypes.All,
                ClientName = "IdentityTesting",
                Scopes = Array.Empty<string>()
            });

            var clients = await _client.GetClientsAsync();
            Assert.Equal(2, clients.Length);
        }

        [Fact]
        public async Task ShouldReturnNewlyCreatedClient()
        {
            var id = await _client.AddClientAsync(new ClientModel
            {
                ClientId = "That.Client",
                ClientName = "That Client",
                GrantTypes = new [] {SupportedGrantTypes.Password},
                ClientSecrets = new []
                {
                    new ClientSecretModel
                    {
                        Value = "This.Data"
                    }
                },
                Scopes = new[]{"bob"}
            });

            var client = await _client.GetClientAsync(id);
            Assert.Equal(id, client.ClientId);
            Assert.Equal("That Client", client.ClientName);
            Assert.Contains(SupportedGrantTypes.Password, client.GrantTypes);
            Assert.Contains("bob", client.Scopes);
        }
    }
}