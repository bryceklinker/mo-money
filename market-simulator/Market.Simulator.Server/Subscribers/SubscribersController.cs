using System;
using System.Threading.Tasks;
using Market.Simulator.Models.Subscribers;
using Microsoft.AspNetCore.Mvc;

namespace Market.Simulator.Server.Subscribers
{
    [Route("[controller]")]
    public class SubscribersController : Controller
    {
        private readonly ISubscribersService _service;

        public SubscribersController(ISubscribersService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var models = await _service.GetAll();
            return Ok(models);
        }

        [HttpGet("{id:long}", Name = "GetSubscriberById")]
        public async Task<IActionResult> GetById(long id)
        {
            var model = await _service.GetById(id);
            return Ok(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] SubscriberModel model)
        {
            var newModel = await _service.Add(model);
            return CreatedAtRoute("GetSubscriberById", new {id = newModel.Id}, newModel);
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> Update(long id, [FromBody] SubscriberModel model)
        {
            await _service.Update(id, model);
            return NoContent();
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}