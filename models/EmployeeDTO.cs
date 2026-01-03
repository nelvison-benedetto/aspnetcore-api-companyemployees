namespace CompanyEmployees.models
{
    public record EmployeeDTO(Guid id, string Name, int Age, string Position)
    {

    }
    //record x è immutabile, perfetto x dati di output (solo lettura), piu sicuro di classes normali.
    //qua select solo i campi che vuoi esporre
}
