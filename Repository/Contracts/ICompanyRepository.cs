using CompanyEmployees.models;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace CompanyEmployees.Repository.Contracts
{
    public interface ICompanyRepository
    {
        IEnumerable<Company> GetAllCompanies(bool trackChanges);
        Company GetCompany(Guid companyId, bool trackChanges);
        void CreateCompany(Company company);
    }
}
