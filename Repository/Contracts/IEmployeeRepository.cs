using CompanyEmployees.models;
namespace CompanyEmployees.Repository.Contracts
{
    public interface IEmployeeRepository
    {
        //IEnumerable<Employee> GetAllEmployees(bool trackChanges);
        IEnumerable<Employee> GetEmployees(Guid companyId, bool trackChanges);
        Employee GetEmployee(Guid companyId, Guid id, bool trackChanges);
        void CreateEmployeeForCompany(Guid companyId, Employee employee);
        void DeleteEmployee(Employee employee);

    } //x more info how check IRepositoryBase.cs
}
