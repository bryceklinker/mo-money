using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Market.Simulator.Models.Publishing;
using Market.Simulator.Models.Quotes;
using Market.Simulator.Models.Subscribers;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Mo.Money.Common;
using Mo.Money.Common.Http;
using Serilog;

namespace Stock.Ticker.Tests.Common.FakeMarket
{
    public class FakeMarketServer : IDisposable
    {
        private readonly int _port;
        public const int DefaultPort = 6300;
        private readonly IWebHost _server;
        private readonly ConcurrentBag<SubscriberModel> _subscribers;
        private readonly IHttpClientFactory _httpClientFactory;

        public Uri BaseUri => new Uri($"https://localhost:{_port}");
        
        public FakeMarketServer(int port = DefaultPort)
        {
            _httpClientFactory = HttpClientFactoryCreator.Create();
            _subscribers = new ConcurrentBag<SubscriberModel>();
            _port = port;
            _server = WebHost.CreateDefaultBuilder<FakeMarketStartup>(Array.Empty<string>())
                .ConfigureServices(s => s.AddSingleton(this))
                .UseUrls(BaseUri.AbsoluteUri)
                .ConfigureForMoMoney()
                .Build();

            _server.StartAsync();
        }

        public SubscriberModel[] GetSubscribers()
        {
            return _subscribers.ToArray();
        }

        public void AddSubscriber(SubscriberModel model)
        {
            _subscribers.Add(model);
        }

        public async Task PublishQuote(QuoteModel model)
        {
            var marketEvent = new MarketEventModel(model);
            var tasks = _subscribers.Select(s => PublishToSubscriber(s, marketEvent));
            await Task.WhenAll(tasks);
        }
        
        public void Dispose()
        {
            _server?.Dispose();
        }

        private async Task PublishToSubscriber(SubscriberModel subscriber, MarketEventModel model)
        {
            using (var client = _httpClientFactory.CreateClient())
            {
                await client.PostAsync(subscriber.Url, new JsonContent<MarketEventModel>(model));
            }
        }
    }
}