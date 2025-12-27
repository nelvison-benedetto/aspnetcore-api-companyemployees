using CompanyEmployees.models;
using CompanyEmployees.Service;
using CompanyEmployees.Service.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanyEmployees.Controllers
{
    [Route("api/[controller]")]
    [ApiController]  //questo pero manda mex di errore automatici, x questo serve settare le validazioni w custom mexs
    public class CompaniesController : ControllerBase
    {
        private readonly IServiceManager _service;  //usare sempre le interfaces!!!
        public CompaniesController(IServiceManager service) { //usare sempre le interfaces here!!!
            this._service = service;
        }


        [HttpGet]
        [Authorize(Roles ="admin")]
        public IActionResult GetCompanies() {

                var companies = _service.CompanyService.GetAllCompanies(false);
                return Ok(companies);
        }


        [HttpGet("{id:guid}", Name = "CompanyById")]
        public IActionResult GetCompany(Guid id)
        {
            var company = _service.CompanyService.GetCompany(id, false);
            return Ok(company);
        }

        [HttpPost]
        public IActionResult CreateCompany([FromBody] CompanyForCreationDTO company)
        {
            if (company is null) { return BadRequest("CompanyForCreationDTO is null"); }
            var createdCompany = _service.CompanyService.CreateCompany(company);
            return CreatedAtRoute("CompanyById",
                new { id = createdCompany.id },
                createdCompany);  //restful!! 
        }
        //now appena fai su postman il create, nella response vedi l'header location con il link per fare la get della risorsa creata!!



    }
}
