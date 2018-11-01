using System.Threading.Tasks;
using Identity.Management.Client.ApiResources;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Management.Server.ApiResources
{
    [Route("api-resources")]
    public class ApiResourcesController : Controller
    {
        private readonly IApiResourcesService _service;

        public ApiResourcesController(IApiResourcesService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ApiResourceModel model)
        {
            var newModel = await _service.AddAsync(model);
            return CreatedAtRoute("GetApiResourceById", new {id = newModel.Name}, newModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var models = await _service.GetAllAsync();
            return Ok(models);
        }

        [HttpGet("{id}", Name = "GetApiResourceById")]
        public async Task<IActionResult> GetById(string id)
        {
            var model = await _service.GetByIdAsync(id);
            return Ok(model);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}