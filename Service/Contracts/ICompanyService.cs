using CompanyEmployees.models;

namespace CompanyEmployees.Service.Contracts
{
    public interface ICompanyService
    {
        //IEnumerable<Company> GetAllCompanies(bool trackChanges);

        IEnumerable<CompanyDTO> GetAllCompanies(bool tracChanges); //voluto da exercise1 ok
        CompanyDTO GetCompany(Guid companyId, bool trackChanges);  

        CompanyDTO CreateCompany(CompanyForCreationDTO company);

    }
}
