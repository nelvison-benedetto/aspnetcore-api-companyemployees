using CompanyEmployees.models;
using CompanyEmployees.Service;
using CompanyEmployees.Service.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanyEmployees.Controllers
{
    [Route("api/[controller]")]  //[controller] è un placeholder, aspnetcore lo sostituisce con il nome del controller, ma senza str 'Controller', quindi here endpoint diventa  api/companies
    [ApiController]  //attiva comportamenti auto x le apis, ect binding/validazione auto/ect
    //questo pero manda mex di errore automatici, x questo serve settare le validazioni w custom mexs!!
    public class CompaniesController : ControllerBase
    {
        private readonly IServiceManager _service;  //iniettare sempre le interfaces!! x loose-coupling, facile da testare, facile da sostituire
        public CompaniesController(IServiceManager service) { //usare le interfaces
            this._service = service;  //DI
        }

        [HttpGet]
        [Authorize(Roles ="admin")]  //JWT valido + claim Role = admin
        public IActionResult GetCompanies() {
                var companies = _service.CompanyService.GetAllCompanies(false);
                return Ok(companies);
        }

        [HttpGet("{id:guid}", Name="CompanyById")]  //endpoint  api/companies/{id} + constraint id deve essere type guid (altrimenti error 404)
        public IActionResult GetCompany(Guid id)
        {
            var company = _service.CompanyService.GetCompany(id, false);
            return Ok(company);
        }

        [HttpPost]
        public IActionResult CreateCompany([FromBody] CompanyForCreationDTO company)
        {
            if (company is null) { return BadRequest("CompanyForCreationDTO is null"); }
            //BadRequest ritorna una risposta HTTP 400 (w custom mex)
            var createdCompany = _service.CompanyService.CreateCompany(company);
            return CreatedAtRoute("CompanyById",
                new { id = createdCompany.id },
                createdCompany);  //restful!! nella respose sul client in section header, vedrai il link per fare il GET alla risorsa appena creata!!
        }
        
    }
}
