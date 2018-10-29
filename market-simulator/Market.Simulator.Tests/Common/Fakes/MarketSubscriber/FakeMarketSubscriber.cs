using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Market.Simulator.Models.Publishing;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Mo.Money.Common;

namespace Market.Simulator.Tests.Common.Fakes.MarketSubscriber
{
    public class FakeMarketSubscriber : IDisposable
    {
        public const int DefaultPort = 6100;
        private readonly int _port;
        private readonly IWebHost _host;
        private readonly ConcurrentBag<MarketEventModel> _marketEvents;

        public Uri BaseUrl => new Uri($"https://localhost:{_port}");
        public Uri SubscriberUrl => new Uri($"{BaseUrl.AbsoluteUri}events/incoming");
        
        public FakeMarketSubscriber(int port = DefaultPort)
        {
            _marketEvents = new ConcurrentBag<MarketEventModel>();
            _port = port;
            _host = WebHost.CreateDefaultBuilder<FakeMarketSubscriberStartup>(Array.Empty<string>())
                .ConfigureServices(services => services.AddSingleton(this))
                .UseUrls(BaseUrl.AbsoluteUri)
                .ConfigureForMoMoney()
                .Build();

            _host.StartAsync();
        }

        public void Dispose()
        {
            _host.Dispose();
        }

        public MarketEventModel[] GetMarketEvents(MarketEventType eventType)
        {
            return _marketEvents.Where(m => m.Metadata.EventType == eventType)
                .ToArray();
        }
        
        public void AddMarketEvent(MarketEventModel marketEvent)
        {
            _marketEvents.Add(marketEvent);
        }
    }
}