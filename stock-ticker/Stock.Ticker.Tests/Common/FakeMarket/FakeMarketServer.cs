using System;
using System.Collections.Concurrent;
using Market.Simulator.Models.Quotes;
using Market.Simulator.Models.Subscribers;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Mo.Money.Common;
using Serilog;

namespace Stock.Ticker.Tests.Common.FakeMarket
{
    public class FakeMarketServer : IDisposable
    {
        private readonly int _port;
        public const int DefaultPort = 4600;
        private readonly IWebHost _server;
        private readonly ConcurrentBag<SubscriberModel> _subscribers;

        public Uri BaseUri => new Uri($"https://localhost:{_port}");
        
        public FakeMarketServer(int port = DefaultPort)
        {
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

        public void PublishQuote(QuoteModel model)
        {
            foreach (var subscriber in _subscribers)
            {
                
            }
        }
        
        public void Dispose()
        {
            _server?.Dispose();
        }
    }
}