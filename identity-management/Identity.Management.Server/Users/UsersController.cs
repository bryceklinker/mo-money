using System.Threading.Tasks;
using Identity.Management.Client.Users;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Management.Server.Users
{
    [Route("[controller]")]
    public class UsersController : Controller
    {
        private readonly IUsersService _service;

        public UsersController(IUsersService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] UserModel model)
        {
            var newModel = await _service.AddAsync(model);
            return CreatedAtRoute("GetUserById", new {id = newModel.Id}, newModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var models = await _service.GetAllAsync();
            return Ok(models);
        }

        [HttpGet("{id}", Name = "GetUserById")]
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