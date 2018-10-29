using System;
using Identity.Management.Server;
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

        public void Dispose()
        {
            _identityManagementServer?.Dispose();
        }
    }
}