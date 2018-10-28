using System.Threading.Tasks;
using Market.Simulator.Models.Publishing;
using Market.Simulator.Models.Quotes;
using Microsoft.AspNetCore.SignalR;

namespace Stock.Ticker.Server.Quotes
{
    public interface IQuotesService
    {
        Task SendQuote(MarketEventModel model);
    }

    public class QuotesService : IQuotesService
    {
        private readonly IHubContext<QuotesHub> _hubContext;

        public QuotesService(IHubContext<QuotesHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SendQuote(MarketEventModel model)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveQuote", model.PayloadAs<QuoteModel>());
        }
    }
}