using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Stock.Ticker.Server.Market
{
    public class MarketSubscriptionBackgroundService : BackgroundService
    {
        private readonly IMarketClientFactory _marketClientFactory;
        private readonly IConfiguration _configuration;

        private string StockTickerBaseUrl => _configuration["urls"];
        
        public MarketSubscriptionBackgroundService(IMarketClientFactory marketClientFactory, IConfiguration configuration)
        {
            _marketClientFactory = marketClientFactory;
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var client = _marketClientFactory.Create();
            var subscriptionUrl = $"{StockTickerBaseUrl}/market/incoming-events";
            await client.AddSubscriberAsync("StockTicker", subscriptionUrl);
        }
    }
}