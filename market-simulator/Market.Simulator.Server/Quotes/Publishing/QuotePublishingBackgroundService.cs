using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Market.Simulator.Server.Quotes.Publishing
{
    public class QuotePublishingBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public QuotePublishingBackgroundService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var publisher = scope.ServiceProvider.GetRequiredService<IQuotesPublisher>();
                    await publisher.GenerateAndPublishQuotes();
                }
                await Task.Delay(500, stoppingToken);
            }
            
        }
    }
}