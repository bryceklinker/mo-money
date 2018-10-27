using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Market.Simulator.Models.Publishing;
using Market.Simulator.Server.Subscribers.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Market.Simulator.Server.Common.Services
{
    public interface IMarketEventPublisher
    {
        Task Publish<T>(T model, MarketEventType? marketEventType = null);
    }

    public class MarketEventPublisher : IMarketEventPublisher
    {
        public const string HttpClientName = "MarketEventPublisher";
        private readonly IContext _context;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger _logger;

        public MarketEventPublisher(IContext context, IHttpClientFactory httpClientFactory, ILoggerFactory loggerFactory)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
            _logger = loggerFactory.CreateLogger<MarketEventPublisher>();
        }

        public async Task Publish<T>(T model, MarketEventType? marketEventType = null)
        {
            var marketEvent = new MarketEventModel(model, marketEventType);
            await PublishToSubscribers(marketEvent);
        }

        private async Task PublishToSubscribers(MarketEventModel marketEvent)
        {
            var subscribers = await GetSubscribers();
            var publishTasks = subscribers.Select(url => PublishToSubscriber(url, marketEvent));
            await Task.WhenAll(publishTasks);
        }
        
        private async Task PublishToSubscriber(string url, MarketEventModel marketEventModel)
        {
            using (var client = _httpClientFactory.CreateClient(HttpClientName))
            {
                try
                {
                    client.Timeout = TimeSpan.FromSeconds(1);
                    await client.PostAsJsonAsync(url, marketEventModel);
                }
                catch (Exception e)
                {
                    _logger.LogWarning("Failed to publish event to {subscriber}\r\n {exception}", url, e);
                }
                
            }
        }

        private Task<string[]> GetSubscribers()
        {
            return _context.GetAll<Subscriber>()
                .Select(s => s.Url)
                .ToArrayAsync();
        }
    }
}