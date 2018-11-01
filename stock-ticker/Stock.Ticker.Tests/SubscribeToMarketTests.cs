using System;
using System.Threading.Tasks;
using Market.Simulator.Client.Quotes;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Stock.Ticker.Tests.Common;
using Stock.Ticker.Tests.Common.FakeMarket;
using Xunit;
using Xunit.Sdk;

namespace Stock.Ticker.Tests
{
    [Collection(StockTickerCollection.Name)]
    public class SubscribeToMarketTests
    {
        private readonly StockTickerFixture _fixture;
        private readonly FakeMarketServer _fakeMarketServer;

        public SubscribeToMarketTests(StockTickerFixture fixture)
        {
            _fixture = fixture;
            _fakeMarketServer = fixture.MarketServer;
        }
        
        [Fact]
        public async Task ShouldSubscribeToMarketEvents()
        {
            await Task.Delay(1000);
            Assert.Single(_fakeMarketServer.GetSubscribers());
        }

        [Fact]
        public async Task ShouldPublishQuotesWhenTheyAreReceived()
        {
            var hubConnection = new HubConnectionBuilder()
                .WithUrl($"{_fixture.StockTickerBaseUrl}quotes")
                .AddJsonProtocol()
                .Build();

            QuoteModel quote = null;
            hubConnection.On<QuoteModel>("ReceiveQuote", q => quote = q);
            await hubConnection.StartAsync();

            var expected = new QuoteModel { Id = 123, Price = 23.12m, Timestamp = DateTimeOffset.UtcNow };
            await _fakeMarketServer.PublishQuote(expected);

            await Task.Delay(1000);
            Assert.Equal(expected.Id, quote.Id);
            Assert.Equal(expected.Price, quote.Price);
            Assert.Equal(expected.Timestamp, quote.Timestamp);
        }
    }
}