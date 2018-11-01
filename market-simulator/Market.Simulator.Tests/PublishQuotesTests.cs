using System;
using System.Threading.Tasks;
using Market.Simulator.Client;
using Market.Simulator.Client.Publishing;
using Market.Simulator.Client.Quotes;
using Market.Simulator.Tests.Common;
using Market.Simulator.Tests.Common.Fakes.MarketSubscriber;
using Xunit;

namespace Market.Simulator.Tests
{
    [Collection(MarketServerCollection.Name)]
    public class PublishQuotesTests : IDisposable
    {
        private readonly FakeMarketSubscriber _fakeMarketSubscriber;
        private readonly MarketSimulatorClient _marketSimulatorClient;

        public PublishQuotesTests(MarketServerFixture fixture)
        {
            fixture.Reset();

            _marketSimulatorClient = fixture.CreateClient();
            _fakeMarketSubscriber = fixture.AddFakeSubscriber();
        }

        [Fact]
        public async Task ShouldPublishQuotesToSubscribers()
        {
            await _marketSimulatorClient.AddCompanyAsync("Microsoft");
            await _marketSimulatorClient.AddSubscriberAsync("Testing", _fakeMarketSubscriber.SubscriberUrl.AbsoluteUri);
            await Task.Delay(1500);

            var quotes = _fakeMarketSubscriber.GetMarketEvents(MarketEventType.NewQuote);
            Assert.All(quotes, m => Assert.Equal("Microsoft", m.PayloadAs<QuoteModel>().CompanyName));
        }

        public void Dispose()
        {
            _fakeMarketSubscriber?.Dispose();
        }
    }
}