using System;
using Market.Simulator.Client;
using Microsoft.Extensions.Configuration;

namespace Stock.Ticker.Server.Market
{
    public interface IMarketClientFactory
    {
        MarketSimulatorClient Create();
    }

    public class MarketClientFactory : IMarketClientFactory
    {
        private readonly IConfiguration _configuration;

        private Uri MarketUrl => new Uri(_configuration["Market:BaseUrl"]);

        public MarketClientFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public MarketSimulatorClient Create()
        {
            return new MarketSimulatorClient(MarketUrl);
        }
    }
}