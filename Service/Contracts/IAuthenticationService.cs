using CompanyEmployees.models;

namespace CompanyEmployees.Service.Contracts
{
    public interface IAuthenticationService
    {
        //public async Task<bool> ValidateUser(UserForAuthenticationDTO userForAuthenticationDTO);
        //public Task<string> CreateToken();

        Task<bool> ValidateUser(UserForAuthenticationDTO userForAuthenticationDTO);

        Task<string> CreateToken();

    }
}
