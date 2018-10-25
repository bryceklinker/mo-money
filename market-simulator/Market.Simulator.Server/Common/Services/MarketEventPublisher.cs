using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Market.Simulator.Models.Publishing;
using Market.Simulator.Server.Subscribers.Entities;
using Microsoft.EntityFrameworkCore;

namespace Market.Simulator.Server.Common.Services
{
    public interface IMarketEventPublisher
    {
        Task Publish<T>(T model);
    }

    public class MarketEventPublisher : IMarketEventPublisher
    {
        private readonly IContext _context;
        private readonly IHttpClientFactory _httpClientFactory;

        public MarketEventPublisher(IContext context, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
        }

        public async Task Publish<T>(T model)
        {
            var marketEvent = new MarketEventModel(model);
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
            using (var client = _httpClientFactory.CreateClient())
            {
                await client.PostAsJsonAsync(url, marketEventModel);
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