using AutoMapper;
using CompanyEmployees.Exceptions;
using CompanyEmployees.models;
using CompanyEmployees.Repository.Contracts;
using CompanyEmployees.Service.Contracts;

namespace CompanyEmployees.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        public EmployeeService(IRepositoryManager repository, IMapper mapper) { 
            this._repository = repository;  //DI
            this._mapper = mapper;  //DI
        }

        //public IEnumerable<models.Employee> GetAllEmployees(bool trackChanges)
        //{
        //    var employees = _repository.Employee.GetAllEmployees(trackChanges);
        //    return employees;
        //}

        public IEnumerable<EmployeeDTO> GetEmployees(Guid companyId, bool trackChanges)
        {
            var company = _repository.Company.GetCompany(companyId, trackChanges);
            if (company is null) { throw new CompanyNotFoundException(companyId); }
            var employeesFromDb = _repository.Employee.GetEmployees(companyId, trackChanges);
            var employeesDTO = _mapper.Map<IEnumerable<EmployeeDTO>>(employeesFromDb);
            return employeesDTO;
        }

        public EmployeeDTO GetEmployee(Guid companyId, Guid id, bool trackChanges)
        {
            var company = _repository.Company.GetCompany(companyId, false);
            if (company is null) { throw new CompanyNotFoundException(companyId); }

            var employeeFromDb = _repository.Employee.GetEmployee(companyId, id, false);
            if (employeeFromDb is null) { throw new EmployeeNotFoundException(id); }
            
            var employeeDTO = _mapper.Map<EmployeeDTO>(employeeFromDb);
            return employeeDTO;
        }

        public EmployeeDTO CreateEmployeeForCompany(Guid companyId, EmployeeForCreationDTO employeeForCreation, bool trackChanges)
        {
            var company = _repository.Company.GetCompany(companyId, trackChanges);
            if (company is null) { throw new CompanyNotFoundException(companyId); }
            var employeeEntity = _mapper.Map<Employee>(employeeForCreation);
            _repository.Employee.CreateEmployeeForCompany(companyId, employeeEntity);
            _repository.Save();
            var employeeToReturn = _mapper.Map<EmployeeDTO>(employeeEntity);
            return employeeToReturn;
        }

        public void DeleteEmployeeForCompany(Guid companyId, Guid id, bool trackChanges)
        {
            var company = _repository.Company.GetCompany(companyId, trackChanges);
            if (company is null) { throw new CompanyNotFoundException(companyId); }
            var employeeForCompany = _repository.Employee.GetEmployee(companyId, id, false);
            if (employeeForCompany is null) { throw new EmployeeNotFoundException(id); }
            _repository.Employee.DeleteEmployee(employeeForCompany);
            _repository.Save();
        }

    }
}
