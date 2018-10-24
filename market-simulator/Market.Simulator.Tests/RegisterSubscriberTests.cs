using System;
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
            _client = new MarketSimulatorClient(fixture.BaseUrl);
            ClearSubscribers().Wait();
        }

        [Fact]
        public async Task ShouldSubscribeToSimulator()
        {
            var id = await _client.SubscribeAsync(new SubscriberModel
            {
                Name = "New Listener",
                Url = "https://localhost:4000/listening"
            });

            var listeners = await _client.GetSubscribersAsync();
            Assert.Single(listeners);
            Assert.Equal(id, listeners[0].Id);
            Assert.Equal("New Listener", listeners[0].Name);
            Assert.Equal("https://localhost:4000/listening", listeners[0].Url);
        }

        [Fact]
        public async Task ShouldHaveTwoSubscribers()
        {
            await _client.SubscribeAsync(new SubscriberModel {Name = "Bob", Url = "https://localhost/"});
            var id = await _client.SubscribeAsync(new SubscriberModel
            {
                Name = "Jack",
                Url = "https://localhost:3200/bob"
            });
           
            var listeners = await _client.GetSubscribersAsync();
            Assert.Equal(2, listeners.Length);
            Assert.Equal(id, listeners[1].Id);
            Assert.Equal("Jack", listeners[1].Name);
            Assert.Equal("https://localhost:3200/bob", listeners[1].Url);
        }

        [Fact]
        public async Task ShouldUpdateExistingSubscriber()
        {
            var id = await _client.SubscribeAsync(new SubscriberModel
            {
                Name = "Bill",
                Url = "https://localhost:5421/receiver"
            });

            await _client.UpdateSubscriberAsync(id, new SubscriberModel
            {
                Name = "IDK",
                Url = "https://localhost:5894/receiving"
            });

            var subscriber = await _client.GetSubscriberAsync(id);
            Assert.Equal("IDK", subscriber.Name);
            Assert.Equal("https://localhost:5894/receiving", subscriber.Url);
        }

        private async Task ClearSubscribers()
        {
            var models = await _client.GetSubscribersAsync();
            foreach (var model in models)
            {
                await _client.DeleteSubscriberAsync(model.Id);
            }
        }
    }
}