using AutoMapper;
using CompanyEmployees.models;
namespace CompanyEmployees.Mapping

{
    //le mappature le fai tutte qua!!
    public class MappingProfile : Profile   //Profile è classe di AutoMapper, serve x registrare tutte le mappature è una configurazione centrale
    {
        public MappingProfile()
        {
            CreateMap<Company, CompanyDTO>()
                .ForCtorParam("FullAddress", opt => opt.MapFrom(x => string.Join(' ', new object?[] { x.Address, x.Country } ) ) );
            //vede che ComapnyDTO è un record->usa il constrct->mappa i params x nome. but FullAddress(esistente in CompanyDTO) non esiste in Company, quindi lo devi mappare manualmente con ForCtorParam (quindi quando chiama il constrct di CompanyDTO, il param FullAddress proviene da questa expression! works only w records/classes w constr parametrico)

            CreateMap<Employee, EmployeeDTO>();  //mappatura dati Employee->EmployeeDTO(solo quelli specificati come param nel constrct di EmployeeDTO)
            CreateMap<CompanyForCreationDTO, Company>();
            CreateMap<EmployeeForCreationDTO, Employee>();

        }
        //AUTOMAPPER si usa il 90% delle volte, MAPPING MANUALE(see Models/CompanyMapper.cs) il 10% delle volte (quando hai logica complessa)

    }
}
