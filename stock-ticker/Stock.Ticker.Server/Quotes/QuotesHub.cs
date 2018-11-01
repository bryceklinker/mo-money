using System.Threading.Tasks;
using Market.Simulator.Client.Quotes;
using Microsoft.AspNetCore.SignalR;

namespace Stock.Ticker.Server.Quotes
{
    public interface IQuotesClient
    {
        Task ReceiveQuote(QuoteModel model);
    }
    
    public class QuotesHub : Hub<IQuotesClient>
    {
        public async Task SendQuote(QuoteModel model)
        {
            await Clients.All.ReceiveQuote(model);
        }
    }
}