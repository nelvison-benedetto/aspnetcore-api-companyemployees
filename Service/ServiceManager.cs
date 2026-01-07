using AutoMapper;
using CompanyEmployees.Repository.Contracts;
using CompanyEmployees.Service.Contracts;
using Microsoft.AspNetCore.Mvc.Infrastructure;


namespace CompanyEmployees.Service
{
    public sealed class ServiceManager : IServiceManager   //sealed, no children
    {
        private readonly Lazy<ICompanyService> _companyService;  //readonly. dichiarazione obj
        private readonly Lazy<IEmployeeService> _employeeService;
        private readonly Lazy<IAuthenticationService> _authenticationService;
        //remember quando inietti usi sempre le interfaces Ixxx x loose-couplig, easy x tests, easy da sostituire in futuro. e.g. test reale (w concreto sarebbe impossibile)
            //ICompanyService fakeService = new FakeCompanyService();

        public ServiceManager(IRepositoryManager repositoryManager, IMapper mapper, IConfiguration configuration) {
            _companyService =  new Lazy<ICompanyService>( () => new CompanyService( repositoryManager, mapper ) );  //inizializzazione obj. è per questo che ci sono 2 Lazy, è come creare List<string> xx = new List<string>() !
            //registra come creare obj, verrà creata solo alla prima chiamata
            _employeeService =  new Lazy<IEmployeeService>( () => new EmployeeService(repositoryManager, mapper) );
            _authenticationService =  new Lazy<IAuthenticationService>( () => new AuthenticationService(configuration) );
        }

        //properties
        public ICompanyService CompanyService =>  _companyService.Value;  //=> sostituisce gia anche il 'return'
        public IEmployeeService EmployeeService =>  _employeeService.Value;
        public IAuthenticationService AuthenticationService => _authenticationService.Value;
        //PROPERTIES w a get;. usa polymorphysm e.g. ICompanyService service = new CompanyService(); (quindi a compile-time vedy a sx, a run-time vedi type a dx)
        //quando chiami e.g. var service = serviceManager.CompanyService; allora viene controllato _companyService.Value se non esiste allora Lazy crea l’oggetto new CompanyService(repositoryManager, mapper) (here qua sopra usando _companyService), SE ESISTE GIA ALLORA RIUTILIZZERA SEMPRE LO STESSO!!

    }
    //x non dover iniettare nel controller(o dove vuoi utilizzarli) tutti gli IServiceX a mano, invece inietti 1 singolo IServiceManager service. scalabile e performante.

}
