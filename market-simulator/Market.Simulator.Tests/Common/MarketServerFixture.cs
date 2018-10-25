using System;
using System.Net.Http;
using System.Threading.Tasks;
using Market.Simulator.Client;
using Market.Simulator.Models.Common;
using Market.Simulator.Models.Companies;
using Market.Simulator.Server;
using Market.Simulator.Server.Companies.Entities;
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

        public void ResetData()
        {
            ClearSubscribers().Wait();
        }
        
        public void Dispose()
        {
            _server?.Dispose();
        }
        
        private async Task ClearSubscribers()
        {
            var client = new MarketSimulatorClient(BaseUrl);
            await DeleteAsync(client.GetCompaniesAsync, client.DeleteCompanyAsync);
            await DeleteAsync(client.GetSubscribersAsync, client.DeleteSubscriberAsync);
        }

        private async Task DeleteAsync<T>(Func<Task<T[]>> getMethod, Func<long, Task> deleteMethod)
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