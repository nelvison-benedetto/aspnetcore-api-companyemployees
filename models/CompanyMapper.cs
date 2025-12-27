namespace CompanyEmployees.models
{
    public static class CompanyMapper
    {
        public static CompanyDTO MapToDto(Company company)
        {
            return new CompanyDTO(
                company.Id,
                company.Name!,
                $"{company.Address}, {company.Country}"
            );
        }
    }
}
//now use w  var dto = CompanyMapper.MapToDto(company);

