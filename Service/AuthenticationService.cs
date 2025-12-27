using CompanyEmployees.models;
using CompanyEmployees.Service.Contracts;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CompanyEmployees.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IConfiguration _configuration;
        public AuthenticationService(IConfiguration configuration) {
            _configuration = configuration;
        }
        public async Task<bool> ValidateUser(UserForAuthenticationDTO userForAuthenticationDTO)
        {
            //works here sqlconnection + sqlcommand in x exercise che usa .DOTNET
            
            if (userForAuthenticationDTO.UserName.Equals("user1") && userForAuthenticationDTO.Password.Equals("pwd1")) {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<string> CreateToken()
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims();
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        private SigningCredentials GetSigningCredentials() {
            var key = "BusinessSecretKeyBusinessSecretKeyBusinessSecretKeyBusinessSecretKey";
            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims() { 
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, "user1"),
            };
            claims.Add(new Claim(ClaimTypes.Role, "admin"));
            return claims;
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims) { 
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var tokenOptions = new JwtSecurityToken(
                issuer: jwtSettings["ValidIssuer"],
                audience: jwtSettings["ValidAudience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(10),
                //expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expires"])),
                signingCredentials: signingCredentials
                );
            return tokenOptions;
        }

    }
}
