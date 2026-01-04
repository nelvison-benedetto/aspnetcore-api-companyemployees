using CompanyEmployees.models;
using CompanyEmployees.Service.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CompanyEmployees.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/companies/{companyId}/employees")]
    [ApiController]  //attiva comportamenti auto x le apis, ect binding/validazione auto/ect
    public class EmployeesController : ControllerBase
    {
        private readonly IServiceManager _service;
        public EmployeesController(IServiceManager service)
        {
            this._service = service;  //DI
        }

        //[HttpGet]
        //public IActionResult GetEmployees()
        //{
        //    var employees = _service.EmployeeService.GetAllEmployees(false);
        //    return Ok(employees);
        //}

        [HttpGet]
        public IActionResult GetEmployeesForCompany(Guid companyId)
        {
            var employees = _service.EmployeeService.GetEmployees(companyId, false);
            return Ok(employees);
        }

        [HttpGet("{id:guid}", Name ="GetEmployeeForCompany")]
        public IActionResult GetEmployeeForCompany(Guid companyId, Guid id) {
            var employee = _service.EmployeeService.GetEmployee(companyId, id, false);
            return Ok(employee);
        }

        //[HttpPost]
        //public IActionResult CreateEmployeeForCompany(Guid companyId, [FromBody] models.EmployeeForCreationDTO employee)
        //{
        //    if (employee is null)  //da scrivere xk in program.cs ho disabilitato il automatic model state validation dell'api controller!!
        //    {
        //        return BadRequest("EmployeeForCreationDTO is null");
        //    }
        //    var employeeToReturn = _service.EmployeeService.CreateEmployeeForCompany(companyId, employee, trackChanges: false);
        //    return CreatedAtRoute("GetEmployeeForCompany",
        //        new { companyId, id = employeeToReturn.id },
        //        employeeToReturn);
        //}

        [HttpPost]
        public IActionResult CreateEmployeeForCompany(Guid companyId, [FromBody] EmployeeForCreationDTO employee)
        {
            if (employee is null)  //!!DA VALIDARE A MANO in program.css ho disabilitato il automatic model state validation di [ApiController]!!
            {
                return BadRequest("EmployeeForCreationDTO is null");
            }
            if (!ModelState.IsValid)  //!!DA VALIDARE A MANO in program.css ho disabilitato il automatic model state validation di [ApiController]!!
            {
                return UnprocessableEntity(ModelState);
            }
            var employeeToReturn = _service.EmployeeService.CreateEmployeeForCompany(companyId, employee, false);
            return CreatedAtRoute("GetEmployeeForCompany",
                new { companyId, id = employeeToReturn.id },
                employeeToReturn); //restful!! nella respose sul client in section header, vedrai il link per fare il GET alla risorsa appena creata!!
        }

        [HttpDelete("{id:guid}")]
        public IActionResult DeleteEmployeeForCompany(Guid companyId, Guid id)
        {
            _service.EmployeeService.DeleteEmployeeForCompany(companyId, id, false);
            return NoContent();
        }

    }
}
