using CompanyEmployees.Repository.Contracts;

namespace CompanyEmployees.Repository
{
    public sealed class RepositoryManager : IRepositoryManager  //sealed, no childrens
    {
        private readonly RepositoryContext _repositoryContext;  //DependencyInjection di RepositoryContext

        private readonly Lazy<ICompanyRepository> _companyRepository;  //Lazy<T> non crea subito Xobj quando viene istanziato this obj, ma lo crea solo quando Xobj viene realmente richiesto 
        private readonly Lazy<IEmployeeRepository>  _employeeRepository;
        public RepositoryManager(RepositoryContext repositoryContext) {
            _repositoryContext = repositoryContext;

            _companyRepository =  new Lazy<ICompanyRepository>( () => new CompanyRepository(_repositoryContext) );  //create new 
            _employeeRepository = new Lazy<IEmployeeRepository>( () => new EmployeeRepository(_repositoryContext) );
            //cosi sei sicuro che sia _companyRepository sia _employeeRepository usano lo stesso RepositoryContext -> garantisce Unit of Work
        }

        //properties
        public ICompanyRepository Company => _companyRepository.Value; //=> sostituisce gia anche il 'return'
        public IEmployeeRepository Employee => _employeeRepository.Value;
        //PROPERTIES w a get;. usa polymorphysm e.g. ICompanyRepository service = new CompanyRepository(); (quindi a compile-time vedy a sx, a run-time vedi type a dx)
        //quando chiami e.g. var service = repositoryManager.CompanyRepository; allora viene controllato _companyRepository.Value se non esiste allora Lazy crea l’oggetto new CompanyService(repositoryManager, mapper) (here qua sopra usando _companyRepository), SE ESISTE GIA ALLORA RIUTILIZZERA SEMPRE LO STESSO!!

        //methods
        public void Save() => _repositoryContext.SaveChanges();
        //tutte le edits fatte ai repos (CompanyRepository e/o EmployeeRepository) vengono persistite nel DB. garantisce che le modifiche siano fatte in un’unica unità di lavoro (Unit of Work)

    }
}
