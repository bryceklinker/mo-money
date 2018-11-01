using Market.Simulator.Client.Subscribers;
using Microsoft.AspNetCore.Mvc;

namespace Stock.Ticker.Tests.Common.FakeMarket
{
    [Route("subscribers")]
    public class FakeSubscribersController : Controller
    {
        private readonly FakeMarketServer _server;

        public FakeSubscribersController(FakeMarketServer server)
        {
            _server = server;
        }

        [HttpPost]
        public IActionResult Add([FromBody] SubscriberModel model)
        {
            _server.AddSubscriber(model);
            return Ok();
        }
    }
}