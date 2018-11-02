using System;
using System.Linq;
using System.Threading.Tasks;
using Identity.Management.Client;
using Identity.Management.Server;
using Identity.Management.Server.ApiResources;
using Identity.Management.Server.Clients;
using Identity.Management.Server.Users;
using Microsoft.AspNetCore.Hosting;
using Xunit;

namespace Identity.Management.Tests.Common
{
    [CollectionDefinition(Name)]
    public class IdentityManagementCollection : ICollectionFixture<IdentityManagementFixture>
    {
        public const string Name = "IdentityManagement";
    }

    public class IdentityManagementFixture : IDisposable
    {
        private const string IdentityClientSecret = "Mo.Money.Identity.Secret";
        private const string ServerBaseUrl = "https://localhost:6400";
        private readonly IWebHost _identityManagementServer;

        public Uri IdentityBaseUrl => new Uri(ServerBaseUrl);
        
        public IdentityManagementFixture()
        {
            _identityManagementServer = Program.CreateWebHostBuilder(Array.Empty<string>())
                .UseUrls(ServerBaseUrl)
                .Build();

            _identityManagementServer.StartAsync();
        }

        public IdentityManagementClient CreateClient()
        {
            var clientId = DefaultClientsConfig.IdentityClient.ClientId;
            return new IdentityManagementClient(IdentityBaseUrl, clientId, IdentityClientSecret);
        }

        public void ResetData()
        {
            DeleteClients().Wait();
            DeleteApiResources().Wait();
            DeleteUsers().Wait();
        }

        public void Dispose()
        {
            _identityManagementServer?.Dispose();
        }

        private async Task DeleteClients()
        {
            var client = CreateClient();
            var models = await client.GetClientsAsync();
            foreach (var model in models.Where(c => c.ClientId != DefaultClientsConfig.IdentityClient.ClientId))
                await client.DeleteClientAsync(model.ClientId);
        }

        private async Task DeleteApiResources()
        {
            var client = CreateClient();
            var models = await client.GetApiResourcesAsync();
            foreach (var model in models.Where(c => c.Name != DefaultApiResourcesConfig.IdentityApiResource.Name))
                await client.DeleteApiResourceAsync(model.Name);
        }

        private async Task DeleteUsers()
        {
            var client = CreateClient();
            var models = await client.GetUsersAsync();
            foreach (var model in models.Where(u => u.UserName != DefaultUsersConfig.AdminUser.UserName))
                await client.DeleteUserAsync(model.Id);
        }
    }
}