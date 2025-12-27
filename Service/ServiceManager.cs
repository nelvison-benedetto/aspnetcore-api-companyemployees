using AutoMapper;
using CompanyEmployees.Repository.Contracts;
using CompanyEmployees.Service.Contracts;
using Microsoft.AspNetCore.Mvc.Infrastructure;


namespace CompanyEmployees.Service
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<ICompanyService> _companyService;
        private readonly Lazy<IEmployeeService> _employeeService;
        private readonly Lazy<IAuthenticationService> _authenticationService;

        public ServiceManager(IRepositoryManager repositoryManager, IMapper mapper, IConfiguration configuration) {
            _companyService = new Lazy<ICompanyService>(() => new CompanyService(repositoryManager, mapper));
            _employeeService = new Lazy<IEmployeeService>(() => new EmployeeService(repositoryManager, mapper));
            _authenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(configuration));
        }

        public ICompanyService CompanyService => _companyService.Value;

        public IEmployeeService EmployeeService => _employeeService.Value;
    
        public IAuthenticationService AuthenticationService => _authenticationService.Value;

    }
}
