using System;
using System.Threading.Tasks;
using Market.Simulator.Client;
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
            await _marketSimulatorClient.AddSubscriberAsync("Testing", _fakeMarketSubscriber.SubscriberUrl.AbsoluteUri);
            await Task.Delay(1000);
            Assert.True(_fakeMarketSubscriber.MarketEvents.Length > 1);
        }

        public void Dispose()
        {
            _fakeMarketSubscriber?.Dispose();
        }
    }
}