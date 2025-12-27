using AutoMapper;
using CompanyEmployees.Exceptions;
using CompanyEmployees.models;
using CompanyEmployees.Repository.Contracts;
using CompanyEmployees.Service.Contracts;

namespace CompanyEmployees.Service
{
    public class CompanyService : ICompanyService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        public CompanyService(IRepositoryManager repository, IMapper mapper) { 
            this._repository = repository;
            this._mapper = mapper;
        }

        //original, no DTO
        //public IEnumerable<Company> GetAllCompanies(bool trackChanges)
        //{
        //    try  //basic x gestire exceptions but bad
        //    {
        //        var companies = _repository.Company.GetAllCompanies(trackChanges);
        //        return companies;
        //    }
        //    catch (Exception ex) { 
        //        Console.WriteLine($"something is wrong {ex}");
        //        throw;
        //    }
        //}


        //voluto da exercise 1
        //public IEnumerable<CompanyDTO> GetAllCompanies(bool trackChanges)
        //{
        //    var companies = _repository.Company.GetAllCompanies(trackChanges);

        //    //by prof
        //    var companiesDTO = companies.Select(c => new CompanyDTO(c.Id, c.Name ?? "", string.Join(' ', c.Address, c.Country))).ToList();
        //    //--

        //    return companies.Select(company =>
        //        CompanyMapper.MapToDto(company)
        //    );
        //}
        //---

        //public IEnumerable<CompanyDTO> GetAllCompanies(bool trackChanges)
        //{
        //    try  //basic x gestire exceptions but bad
        //    {
        //        var companies = _repository.Company.GetAllCompanies(trackChanges);
        //        var companiesDTO = _mapper.Map<IEnumerable<CompanyDTO>>(companies);
        //        return companiesDTO;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"something is wrong {ex}");
        //        throw;
        //    }
        //}

        public IEnumerable<CompanyDTO> GetAllCompanies(bool trackChanges)
        {

            //throw new Exception("Test exception");  x test se lancia error by errorhandler globale mio ConfigureExceptionHandler()
            var companies = _repository.Company.GetAllCompanies(trackChanges);
            var companiesDTO = _mapper.Map<IEnumerable<CompanyDTO>>(companies);
            return companiesDTO;


        }
        public CompanyDTO GetCompany(Guid companyId, bool trackChanges)  //ok la dto okok
        {
            var company = _repository.Company.GetCompany(companyId, trackChanges);

            if (company is null)
                //throw new Exception($"Company with id {companyId} not found");
                throw new CompanyNotFoundException(companyId);
            
            var companyDTO = _mapper.Map<CompanyDTO>(company);
            return companyDTO;
        }

        public CompanyDTO CreateCompany(CompanyForCreationDTO company)
        {
            var companyEntity = _mapper.Map<Company>(company);
            _repository.Company.CreateCompany(companyEntity);
            _repository.Save();  //here Save runna SaveCanges in repobase
            var companyToReturn = _mapper.Map<CompanyDTO>(companyEntity);
            return companyToReturn;
        }

    }
}
