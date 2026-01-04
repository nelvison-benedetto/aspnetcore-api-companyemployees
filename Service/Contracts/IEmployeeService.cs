using CompanyEmployees.models;

namespace CompanyEmployees.Service.Contracts
{
    public interface IEmployeeService
    {
        IEnumerable<EmployeeDTO> GetEmployees(Guid companyId, bool trackChanges);

        EmployeeDTO GetEmployee(Guid companyId, Guid id, bool trackChanges);

        EmployeeDTO CreateEmployeeForCompany(Guid companyId, EmployeeForCreationDTO employeeForCreation, bool trackChanges);

        void DeleteEmployeeForCompany(Guid companyId, Guid id, bool trackChanges);

        //usiamo i DTO x return xk di convention(e anche regola) le Entities NON DEVONO MAI USCIRE DAL BACKEND!!
    }
}
