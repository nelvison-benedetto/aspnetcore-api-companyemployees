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
            //tutti i repository condividono lo stesso RepositoryContext, questo permette di coordinare le operazioni in un'unica Unit of Works (è garantita dal DbContext,see more in repositorycontext.cs)
        }

        //properties
        public ICompanyRepository Company => _companyRepository.Value; //=> sostituisce gia anche il 'return'
        public IEmployeeRepository Employee => _employeeRepository.Value;
        //PROPERTIES w a get;. usa polymorphysm e.g. ICompanyRepository service = new CompanyRepository(); (quindi a compile-time vedy a sx, a run-time vedi type a dx)
        //quando chiami e.g. var service = repositoryManager.CompanyRepository; allora viene controllato _companyRepository.Value se non esiste allora Lazy crea l’oggetto new CompanyService(repositoryManager, mapper) (see here qua sopra usando _companyRepository), SE ESISTE GIA ALLORA RIUTILIZZERA SEMPRE LO STESSO!!

        //methods
        public void Save() => _repositoryContext.SaveChanges();
        //Chiude la Unit of Work: persiste(salva) tutte le modifiche(fatte ai repos CompanyRepository e/o EmployeeRepository) tracciate dal DbContext.
        //quindi nei services usi questo method, invece di original SaveChanges() (perche cosi in 1 colpo salvi tutte le modifiche di TUTTE le repos)

    }
}
