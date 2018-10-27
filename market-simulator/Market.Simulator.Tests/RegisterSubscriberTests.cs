using System.Linq;
using System.Threading.Tasks;
using Market.Simulator.Client;
using Market.Simulator.Models.Subscribers;
using Market.Simulator.Tests.Common;
using Xunit;

namespace Market.Simulator.Tests
{
    [Collection(MarketServerCollection.Name)]
    public class RegisterSubscriberTests
    {
        private readonly MarketSimulatorClient _client;

        public RegisterSubscriberTests(MarketServerFixture fixture)
        {
            fixture.Reset();

            _client = fixture.CreateClient();
        }

        [Fact]
        public async Task ShouldSubscribeToSimulator()
        {
            var id = await _client.AddSubscriberAsync("New Listener", "https://localhost:4000/listening");

            var listeners = await _client.GetSubscribersAsync();
            Assert.Single(listeners);
            Assert.Equal(id, listeners[0].Id);
            Assert.Equal("New Listener", listeners[0].Name);
            Assert.Equal("https://localhost:4000/listening", listeners[0].Url);
        }

        [Fact]
        public async Task ShouldHaveTwoSubscribers()
        {
            await _client.AddSubscriberAsync("Bob", "https://localhost/");
            var id = await _client.AddSubscriberAsync("Jack", "https://localhost:3200/bob");
           
            var subscribers = await _client.GetSubscribersAsync();
            Assert.Equal(2, subscribers.Length);
            var subscriber = subscribers.Single(s => s.Id == id);
            Assert.Equal(id, subscriber.Id);
            Assert.Equal("Jack", subscriber.Name);
            Assert.Equal("https://localhost:3200/bob", subscriber.Url);
        }

        [Fact]
        public async Task ShouldUpdateExistingSubscriber()
        {
            var id = await _client.AddSubscriberAsync("Bill", "https://localhost:5421/receiver");

            await _client.UpdateSubscriberAsync(id, new SubscriberModel
            {
                Name = "IDK",
                Url = "https://localhost:5894/receiving"
            });

            var subscriber = await _client.GetSubscriberAsync(id);
            Assert.Equal("IDK", subscriber.Name);
            Assert.Equal("https://localhost:5894/receiving", subscriber.Url);
        }
    }
}