using System.Threading.Tasks;
using Market.Simulator.Client.Companies;
using Microsoft.AspNetCore.Mvc;

namespace Market.Simulator.Server.Companies
{
    [Route("[controller]")]
    public class CompaniesController : Controller
    {
        private readonly ICompaniesService _service;

        public CompaniesController(ICompaniesService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var models = await _service.GetAll();
            return Ok(models);
        }

        [HttpGet("{id:long}", Name = "GetCompanyById")]
        public async Task<IActionResult> GetById(long id)
        {
            var model = await _service.GetById(id);
            return Ok(model);
        }
        
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CompanyModel model)
        {
            var newModel = await _service.Add(model);
            return CreatedAtRoute("GetCompanyById", new {id = newModel.Id}, newModel);
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> Update(long id, [FromBody] CompanyModel model)
        {
            await _service.Update(id, model);
            return NoContent();
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _service.Delete(id);
            return NoContent();
        }
    }
}