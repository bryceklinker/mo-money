using System.Threading.Tasks;
using Identity.Management.Client.Clients;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Management.Server.Clients
{
    [Authorize]
    [Route("[controller]")]
    public class ClientsController : Controller
    {
        private readonly IClientsService _clientsService;

        public ClientsController(IClientsService clientsService)
        {
            _clientsService = clientsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var models = await _clientsService.GetAllAsync();
            return Ok(models);
        }

        [HttpGet("{id}", Name = "GetClientById")]
        public async Task<IActionResult> GetById(string id)
        {
            var model = await _clientsService.GetByIdAsync(id);
            return Ok(model);
        }
        
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ClientModel model)
        {
            var newModel = await _clientsService.AddAsync(model);
            return CreatedAtRoute("GetClientById", new {id = newModel.ClientId}, newModel);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _clientsService.DeleteAsync(id);
            return NoContent();
        }
    }
}