using CompanyEmployees.models;
using CompanyEmployees.Service.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CompanyEmployees.Controllers
{
    [Route("api/[controller]")]  //[controller] è un placeholder, aspnetcore lo sostituisce con il nome del controller, ma senza str 'Controller', quindi here endpoint diventa  api/authentications
    [ApiController]  //attiva comportamenti auto x le apis, ect binding/validazione auto/ect
    public class AuthenticationController : ControllerBase
    {
        private readonly IServiceManager _service;
        public AuthenticationController(IServiceManager service)
        {
            this._service = service;  //DI
        }

        [HttpPost("login")]  //endpoint apposta solo x POST
        public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDTO user) {
            //estrai model from the input, name it 'user'
            if (!await _service.AuthenticationService.ValidateUser(user))
            { //ValidateUser l'ho definito nel mio IAuthenticationService, se username & psw wrong allora...
                return Unauthorized();
            }
            return Ok(new { Token = _service.AuthenticationService.CreateToken() });
            //e.g. {"token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."}
        }
        //ora test su potsman fai un POST a api/authentication/login e in body row json: "username":"user1", "password": "pwd1" e sempre header : Content-Type: application/json
        //header in postman  Authorization : Beare <MyToken>

    }
}
