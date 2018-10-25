using System;
using System.Threading.Tasks;
using Market.Simulator.Models.Companies;
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
        public Task<IActionResult> GetById(long id)
        {
            throw new NotImplementedException();
        }
        
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CompanyModel model)
        {
            var newModel = await _service.Add(model);
            return CreatedAtRoute("GetCompanyById", new {id = newModel.Id}, newModel);
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _service.Delete(id);
            return NoContent();
        }
    }
}