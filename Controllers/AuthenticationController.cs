using CompanyEmployees.models;
using CompanyEmployees.Service.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CompanyEmployees.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IServiceManager _service;
        public AuthenticationController(IServiceManager service)
        {
            this._service = service;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDTO user) {
            if (!await _service.AuthenticationService.ValidateUser(user)) { 
                return Unauthorized();
            }
            return Ok(new { Token = _service.AuthenticationService.CreateToken() });
        }
        //ora test su potsman fai un POST a api/authentication/login e in body row json: "username":"user1", "password": "pwd1" e sempre header : Content-Type: application/json
        //header in postman  Authorization : Beare <MyToken>

    }
}
