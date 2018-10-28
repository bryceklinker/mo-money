using System.Threading.Tasks;
using Market.Simulator.Models.Companies;
using Market.Simulator.Models.Publishing;
using Market.Simulator.Models.Quotes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Stock.Ticker.Server.Quotes;

namespace Stock.Ticker.Server.Market
{
    [Route("[controller]")]
    public class MarketController : Controller
    {
        private readonly IQuotesService _quotesService;

        public MarketController(IQuotesService quotesService)
        {
            _quotesService = quotesService;
        }

        [HttpPost("incoming-events")]
        public async Task<IActionResult> IncomingEvents([FromBody] MarketEventModel model)
        {
            await _quotesService.SendQuote(model);
            return Ok();
        }
    }
}