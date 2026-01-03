using CompanyEmployees.models;
using CompanyEmployees.Repository.Contracts;

namespace CompanyEmployees.Repository
{
    //x more info how works check CompanyRepository.cs

    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public Employee GetEmployee(Guid companyId, Guid id, bool trackChanges)
        {
            return FindByCondition(e => e.CompanyId.Equals(companyId) && e.Id.Equals(id), trackChanges)
                   .SingleOrDefault();
        }

        //public IEnumerable<Employee> GetAllEmployees(bool trackChanges)
        //{
        //    return FindAll(trackChanges)
        //        .OrderBy(e => e.Name)
        //        .ToList();
        //}

        public IEnumerable<Employee> GetEmployees(Guid companyId, bool trackChanges)
        {
            return FindByCondition(e => e.CompanyId.Equals(companyId), trackChanges)
                   .OrderBy(e => e.Name)
                   .ToList();
        }

        public void CreateEmployeeForCompany(Guid companyId, Employee employee)
        {
            employee.CompanyId = companyId;
            Create(employee);
        }

        public void DeleteEmployee(Employee employee)
        {
            Delete(employee);
        }

    }
}
