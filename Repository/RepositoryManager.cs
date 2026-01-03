using CompanyEmployees.Repository.Contracts;

namespace CompanyEmployees.Repository
{
    public sealed class RepositoryManager : IRepositoryManager  //sealed! no childrens!
    {
        private readonly RepositoryContext _repositoryContext;  //DependencyInjection di RepositoryContext

        private readonly Lazy<ICompanyRepository> _companyRepository;  //Lazy<T> non crea subito Xobj quando viene istanziato this obj, ma lo crea solo quando viene realmente richiesto Xobj
        private readonly Lazy<IEmployeeRepository>  _employeeRepository;
        public RepositoryManager(RepositoryContext repositoryContext) {
            _repositoryContext = repositoryContext;

            _companyRepository = new Lazy<ICompanyRepository>(() => new CompanyRepository(_repositoryContext));  //create new 
            _employeeRepository = new Lazy<IEmployeeRepository>(() => new EmployeeRepository(_repositoryContext));
            //cosi sei sicuro che sia _companyRepository sia _employeeRepository usano lo stesso RepositoryContext → garantisce Unit of Work
        }

        public ICompanyRepository Company => _companyRepository.Value;
        public IEmployeeRepository Employee => _employeeRepository.Value;
        //restituiscono il repository concreto (CompanyRepository o EmployeeRepository). se _companyRepository.Value non è ancora stato creato, viene istanziato now.

        public void Save()=> _repositoryContext.SaveChanges();
        //tutte le edits fatte ai repos (CompanyRepository e/o EmployeeRepository) vengono persistite nel DB. garantisce che le modifiche siano fatte in un’unica unità di lavoro (Unit of Work)

    }
}
