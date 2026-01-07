using CompanyEmployees.models;
using CompanyEmployees.Repository.Contracts;

namespace CompanyEmployees.Repository
{
    public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository  //Deriva da RepositoryBase<Company> -> eredita i metodi generici (FindAll(), FindByCondition(), Create(), Update(), Delete()) e  implementa ICompanyRepository -> obbliga a scrivere i metodi specifici dell’interfaccia
    {
        public CompanyRepository(RepositoryContext repositoryContext):base(repositoryContext) { 
        }
        //chiama il constrct del base class RepositoryBase

        public IEnumerable<Company> GetAllCompanies(bool trackChanges)  //ienumerable xk devi return all'esterno dei dati, non una query!!
        {
            return 
                FindAll(trackChanges)  //è il findall() custom che ho creato nel mio repositorybase.cs!!(non esistono altri findall), quindi return IQueryable<Company>
                .OrderBy(c => c.Name)  //compone la query
                .ToList();  //ESEGUE SQL!
                //ora i dati vengono materializzati in memoria
            //return dei dati
        }

        public Company GetCompany(Guid companyId, bool trackChanges)  //il prof non usa ? dopo Company
        {
            return 
                FindByCondition(c => c.Id.Equals(companyId), trackChanges)  //findbycondition custom che ho creato in repositorybase.cs  
                .FirstOrDefault();  //return first found or null se non esiste
                //!!better than SingleOrDefault() che controlla che ci sia al massimo un record → più lento. e Id è pk quindi unica (non composta) quindi okok.
        }

        public void CreateCompany(Company company)
        {
            Create(company);
        }

    }
}
