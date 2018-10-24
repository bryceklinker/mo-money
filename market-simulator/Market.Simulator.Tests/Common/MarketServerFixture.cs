using System;
using System.Net.Http;
using Market.Simulator.Server;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace Market.Simulator.Tests.Common
{
    public class MarketServerFixture : IDisposable
    {
        private const string ServerBaseUrl = "https://localhost:4000";
        private readonly IWebHost _server;

        public Uri BaseUrl => new Uri(ServerBaseUrl);
        
        public MarketServerFixture()
        {
            _server = WebHost
                .CreateDefaultBuilder<Startup>(Array.Empty<string>())
                .UseUrls(ServerBaseUrl)
                .Build();

            _server.StartAsync();
        }
        
        public void Dispose()
        {
            _server?.Dispose();
        }
    }

    [CollectionDefinition(Name)]
    public class MarketServerCollection : ICollectionFixture<MarketServerFixture>
    {
        public const string Name = "MarketServer";
    }
}