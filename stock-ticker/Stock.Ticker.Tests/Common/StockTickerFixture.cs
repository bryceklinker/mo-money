using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Stock.Ticker.Server;
using Stock.Ticker.Tests.Common.FakeMarket;
using Xunit;

namespace Stock.Ticker.Tests.Common
{
    [CollectionDefinition(Name)]
    public class StockTickerCollection : ICollectionFixture<StockTickerFixture>
    {
        public const string Name = "StockTicker";
    }
    
    public class StockTickerFixture : IDisposable
    {
        private const string ServerBaseUrl = "https://localhost:4500";
        private readonly IWebHost _stockTickerServer;

        public FakeMarketServer MarketServer { get; }
        public Uri StockTickerBaseUrl => new Uri(ServerBaseUrl);

        public StockTickerFixture()
        {
            MarketServer = new FakeMarketServer();
            _stockTickerServer = Program.CreateWebHostBuilder(Array.Empty<string>())
                .UseUrls(ServerBaseUrl)
                .Build();

            _stockTickerServer.StartAsync();
        }
        
        public void Dispose()
        {
            MarketServer.Dispose();
            _stockTickerServer?.Dispose();
        }
    }
}