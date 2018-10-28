using System;
using System.Threading.Tasks;
using Stock.Ticker.Tests.Common;
using Stock.Ticker.Tests.Common.FakeMarket;
using Xunit;

namespace Stock.Ticker.Tests
{
    [Collection(StockTickerCollection.Name)]
    public class SubscribeToMarketTests : IDisposable
    {
        private readonly FakeMarketServer _fakeMarketServer;

        public SubscribeToMarketTests(StockTickerFixture fixture)
        {
            _fakeMarketServer = fixture.MarketServer;
        }
        
        [Fact]
        public async Task ShouldSubscribeToMarketEvents()
        {
            await Task.Delay(1000);
            Assert.Single(_fakeMarketServer.GetSubscribers());
        }

        public void Dispose()
        {
            _fakeMarketServer?.Dispose();
        }
    }
}