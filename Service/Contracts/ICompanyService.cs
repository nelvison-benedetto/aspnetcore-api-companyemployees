using CompanyEmployees.models;

namespace CompanyEmployees.Service.Contracts
{
    public interface ICompanyService
    {
        //IEnumerable<Company> GetAllCompanies(bool trackChanges);

        IEnumerable<CompanyDTO> GetAllCompanies(bool trackChanges); //voluto da exercise1 ok
        CompanyDTO GetCompany(Guid companyId, bool trackChanges);  

        CompanyDTO CreateCompany(CompanyForCreationDTO company);

        //usiamo i DTO x return xk di convention(e anche regola) le Entities NON DEVONO MAI USCIRE DAL BACKEND!!
    }
}
