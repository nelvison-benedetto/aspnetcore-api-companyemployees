using CompanyEmployees.models;
using CompanyEmployees.Repository.Contracts;

namespace CompanyEmployees.Repository
{
    public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
    {
        public CompanyRepository(RepositoryContext repositoryContext):base(repositoryContext) { 
            

        }

        public IEnumerable<Company> GetAllCompanies(bool trackChanges)
        {
            return FindAll(trackChanges)
                .OrderBy(c => c.Name)
                .ToList();
        }

        public Company GetCompany(Guid companyId, bool trackChanges)  //il prof non usa ? dopo Company
        {
            return FindByCondition(c => c.Id.Equals(companyId), trackChanges)
                .FirstOrDefault();  //piu veloce e little better performance than .SingleOrDefault()
        }

        public void CreateCompany(Company company)
        {
            Create(company);
        }

    }
}
