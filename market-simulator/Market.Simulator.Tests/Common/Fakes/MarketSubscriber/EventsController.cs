using Market.Simulator.Models.Publishing;
using Microsoft.AspNetCore.Mvc;

namespace Market.Simulator.Tests.Common.Fakes.MarketSubscriber
{
    [Route("events")]
    public class EventsController : Controller
    {
        private readonly FakeMarketSubscriber _subscriber;

        public EventsController(FakeMarketSubscriber subscriber)
        {
            _subscriber = subscriber;
        }

        [HttpPost("incoming")]
        public IActionResult Incoming([FromBody] MarketEventModel marketEvent)
        {
            _subscriber.AddMarketEvent(marketEvent);
            return Accepted();
        } 
    }
}