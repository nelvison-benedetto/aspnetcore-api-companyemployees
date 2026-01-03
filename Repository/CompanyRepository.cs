using CompanyEmployees.models;
using CompanyEmployees.Repository.Contracts;

namespace CompanyEmployees.Repository
{
    public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository  //Deriva da RepositoryBase<Company> → eredita i metodi generici (FindAll(), FindByCondition(), Create(), Update(), Delete()) + Implementa ICompanyRepository → obbliga a scrivere i metodi specifici dell’interfaccia
    {
        public CompanyRepository(RepositoryContext repositoryContext):base(repositoryContext) { 
        }
        //chiama il constrct del base class RepositoryBase

        public IEnumerable<Company> GetAllCompanies(bool trackChanges)
        {
            return FindAll(trackChanges)
                .OrderBy(c => c.Name)
                .ToList();  //convert result in List, qui viene generato realmente l'sql
        }

        public Company GetCompany(Guid companyId, bool trackChanges)  //il prof non usa ? dopo Company
        {
            return FindByCondition(c => c.Id.Equals(companyId), trackChanges)
                .FirstOrDefault();  //return first found or null se non esiste
                //!!better than SingleOrDefault() che controlla che ci sia al massimo un record → più lento. e Id è pk quindi unica (non composta) quindi okok.
        }

        public void CreateCompany(Company company)
        {
            Create(company);
        }

    }
}
