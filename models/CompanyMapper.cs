namespace CompanyEmployees.models
{
    public static class CompanyMapper
    {
        public static CompanyDTO MapToDto(Company company)
        {
            return new CompanyDTO(
                company.Id,
                company.Name!,  //“So che non è null a runtime, fidati”, xk dopotutto il field originale era [Required]
                $"{company.Address}, {company.Country}"
            );
        }
    }
}
//now use w  var dto = CompanyMapper.MapToDto(company);
//here faccio MAPPER MANUALE: good only x small prjs, costruisco a mano il DTO.
//see AUTOMAPPER in Mapping/MappingProfile.cs (perfect x big prjs)