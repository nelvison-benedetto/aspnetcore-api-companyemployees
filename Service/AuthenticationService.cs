using CompanyEmployees.models;
using CompanyEmployees.Service.Contracts;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CompanyEmployees.Service
{
    //JWT + OAuth2/OIDC è lo standard moderno today!!
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IConfiguration _configuration;
        public AuthenticationService(IConfiguration configuration) {
            _configuration = configuration;  //DependencyInjection
        }

        public async Task<bool> ValidateUser(UserForAuthenticationDTO userForAuthenticationDTO) //usi 'async' anche se here non usi 'await' xk in futuro puo sempre servire
        {
            //works here sqlconnection + sqlcommand in x exercise che usa .DOTNET
            
            if (userForAuthenticationDTO.UserName.Equals("user1") && userForAuthenticationDTO.Password.Equals("pwd1")) {
                return true;
            }
            else
            {
                return false;
            }
        } //è un trial auth. in prj real usi MICROSOFT IDENDITY/db/aspnet idendity/oauth/azure ad

        public async Task<string> CreateToken() 
        {
            var signingCredentials = GetSigningCredentials();  //see method below
            var claims = await GetClaims();  //**
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);  //**
            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        } //crea un JWT firmato e lo return
        //GetSigningCredentials() -> GetClaims() -> GenerateTokenOptions() -> return

        private SigningCredentials GetSigningCredentials() {  //private
            var key = "BusinessSecretKeyBusinessSecretKeyBusinessSecretKeyBusinessSecretKey";
            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)); 
             //trasforma key in byte[] chiave crittografica
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
             //return info about chiave(now crittografata) e algoritmo di firma
        }

        private async Task<List<Claim>> GetClaims() { 
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, "user1"),
            };
             //inserisce nome utente
            claims.Add(new Claim(ClaimTypes.Role, "admin"));
             //inserisce ruolo
            return claims;
            //ora potrai usare e.g.[Authorize(Roles = "admin")]
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims) { 
            var jwtSettings = _configuration.GetSection("JwtSettings"); //legge section JwtSettings in appsettings.json
            var tokenOptions = new JwtSecurityToken(
                issuer: jwtSettings["ValidIssuer"],  //chi emette il token
                audience: jwtSettings["ValidAudience"],  //chi lo puo usare
                claims: claims,  //identita
                expires: DateTime.Now.AddMinutes(10),  //tempo scadenza
                //expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expires"])),
                signingCredentials: signingCredentials  //firma
                );
            return tokenOptions;
        }
        //costruisce il JWT vero e proprio e lo restituisce

        /*
         ValidateUser	        verifica credenziali
         GetSigningCredentials	firma JWT
         GetClaims	            identità utente
         GenerateTokenOptions	costruzione token
         CreateToken	        ritorna JWT
         */

    }
}
