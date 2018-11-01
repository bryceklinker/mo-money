using System;
using System.Linq;
using System.Threading.Tasks;
using Identity.Management.Client;
using Identity.Management.Server;
using Identity.Management.Server.Clients;
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
            var client = CreateClient();
            var clients = client.GetClientsAsync().Result;
            foreach (var clientModel in clients.Where(c => c.ClientId != DefaultClientsConfig.IdentityClient.ClientId))
            {
                client.DeleteClientAsync(clientModel.ClientId).Wait();
            }
        }
        
        public void Dispose()
        {
            _identityManagementServer?.Dispose();
        }
    }
}