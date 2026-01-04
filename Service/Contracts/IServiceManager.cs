namespace CompanyEmployees.Service.Contracts
{
    public interface IServiceManager
    {
        ICompanyService CompanyService { get; }  //read-only
        IEmployeeService EmployeeService { get; }  //read-only
        IAuthenticationService AuthenticationService { get; }

    }
}
