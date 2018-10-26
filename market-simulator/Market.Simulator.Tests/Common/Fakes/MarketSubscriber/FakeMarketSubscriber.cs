using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Market.Simulator.Models.Publishing;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Market.Simulator.Tests.Common.Fakes.MarketSubscriber
{
    public class FakeMarketSubscriber : IDisposable
    {
        private readonly int _port;
        private readonly IWebHost _host;
        private readonly ConcurrentBag<MarketEventModel> _marketEvents;

        public MarketEventModel[] MarketEvents => _marketEvents.ToArray();
        
        public Uri BaseUrl => new Uri($"https://localhost:{_port}");
        public Uri SubscriberUrl => new Uri($"{BaseUrl.AbsoluteUri}events/incoming");
        
        public FakeMarketSubscriber(int port = 4100)
        {
            _marketEvents = new ConcurrentBag<MarketEventModel>();
            _port = port;
            _host = WebHost.CreateDefaultBuilder<FakeMarketSubscriberStartup>(Array.Empty<string>())
                .ConfigureServices(services => services.AddSingleton(this))
                .UseUrls(BaseUrl.AbsoluteUri)
                .Build();

            _host.StartAsync();
        }

        public void Dispose()
        {
            _host.Dispose();
        }

        public void AddMarketEvent(MarketEventModel marketEvent)
        {
            _marketEvents.Add(marketEvent);
        }
    }
}