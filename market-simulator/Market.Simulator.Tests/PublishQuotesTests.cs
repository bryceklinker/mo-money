using System;
using System.Threading.Tasks;
using Market.Simulator.Client;
using Market.Simulator.Models.Quotes;
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
            fixture.ResetData();

            _marketSimulatorClient = new MarketSimulatorClient(fixture.BaseUrl);
            _fakeMarketSubscriber = new FakeMarketSubscriber();
        }

        [Fact]
        public async Task ShouldPublishQuotesToSubscribers()
        {
            await _marketSimulatorClient.AddCompanyAsync("Microsoft");
            await _marketSimulatorClient.AddSubscriberAsync("Testing", _fakeMarketSubscriber.SubscriberUrl.AbsoluteUri);
            await Task.Delay(1500);
            Assert.NotEmpty(_fakeMarketSubscriber.MarketEvents);
            Assert.All(_fakeMarketSubscriber.MarketEvents, m => Assert.Equal("Microsoft", m.PayloadAs<QuoteModel>().CompanyName));
        }

        public void Dispose()
        {
            _fakeMarketSubscriber?.Dispose();
        }
    }
}