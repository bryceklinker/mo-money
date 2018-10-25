using Market.Simulator.Models.Publishing;
using Microsoft.AspNetCore.Mvc;

namespace Market.Simulator.Tests.Common.Fakes.MarketSubscriber
{
    [Route("[controller]")]
    public class EventsController : Controller
    {
        private readonly FakeMarketSubscriber _subscriber;

        public EventsController(FakeMarketSubscriber subscriber)
        {
            _subscriber = subscriber;
        }

        [HttpPost("incoming")]
        public IActionResult Receive([FromBody] MarketEventModel marketEvent)
        {
            _subscriber.AddMarketEvent(marketEvent);
            return Accepted();
        } 
    }
}