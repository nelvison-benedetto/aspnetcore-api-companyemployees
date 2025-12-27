using AutoMapper;
using CompanyEmployees.models;
namespace CompanyEmployees.Mapping

{
    //le mappature le fai tutte qua!!
    public class MappingProfile : Profile  
    {
        public MappingProfile()
        {
            CreateMap<Company, CompanyDTO>()
                .ForCtorParam("FullAddress", opt => opt.MapFrom(x => string.Join(' ', new object?[] { x.Address, x.Country } ) ) );
            CreateMap<Employee, EmployeeDTO>();
            CreateMap<CompanyForCreationDTO, Company>();
            CreateMap<EmployeeForCreationDTO, Employee>();


        }

    }
}
