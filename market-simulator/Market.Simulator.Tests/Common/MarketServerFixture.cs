using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Market.Simulator.Client;
using Market.Simulator.Models.Common;
using Market.Simulator.Models.Companies;
using Market.Simulator.Server;
using Market.Simulator.Server.Companies.Entities;
using Market.Simulator.Tests.Common.Fakes.MarketSubscriber;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace Market.Simulator.Tests.Common
{
    public class MarketServerFixture : IDisposable
    {
        private const string ServerBaseUrl = "https://localhost:4000";
        private readonly IWebHost _simulatorServer;
        private readonly List<FakeMarketSubscriber> _fakeSubscribers;

        public Uri SimulatorBaseUrl => new Uri(ServerBaseUrl);
        
        public MarketServerFixture()
        {
            _fakeSubscribers = new List<FakeMarketSubscriber>();
            _simulatorServer = Program.CreateWebHostBuilder(Array.Empty<string>())
                .UseUrls(ServerBaseUrl)
                .Build();

            _simulatorServer.StartAsync();
        }

        public void Reset()
        {
            ClearSubscriberData().Wait();
            StopFakeSubscribers();
        }
        
        public void Dispose()
        {
            _simulatorServer?.Dispose();
        }

        public MarketSimulatorClient CreateClient()
        {
            return new MarketSimulatorClient(SimulatorBaseUrl);
        }
        
        public FakeMarketSubscriber AddFakeSubscriber(int port = FakeMarketSubscriber.DefaultPort)
        {
            var subscriber = new FakeMarketSubscriber(port);
            var client = CreateClient();
            _fakeSubscribers.Add(subscriber);
            client.AddSubscriberAsync($"Fake Subscriber {_fakeSubscribers.Count}", subscriber.SubscriberUrl.AbsoluteUri)
                .Wait();
            return subscriber;
        }
        
        private async Task ClearSubscriberData()
        {
            var client = new MarketSimulatorClient(SimulatorBaseUrl);
            await DeleteAsync(client.GetCompaniesAsync, client.DeleteCompanyAsync);
            await DeleteAsync(client.GetSubscribersAsync, client.DeleteSubscriberAsync);
        }

        private void StopFakeSubscribers()
        {
            _fakeSubscribers.ForEach(s => s.Dispose());
            _fakeSubscribers.Clear();
        }

        private static async Task DeleteAsync<T>(Func<Task<T[]>> getMethod, Func<long, Task> deleteMethod)
            where T : IModel
        {
            var models = await getMethod();
            foreach (var model in models)
                await deleteMethod(model.Id);
        }
    }

    [CollectionDefinition(Name)]
    public class MarketServerCollection : ICollectionFixture<MarketServerFixture>
    {
        public const string Name = "MarketServer";
    }
}