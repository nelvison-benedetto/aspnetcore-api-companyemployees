namespace CompanyEmployees.models
{
    public record EmployeeDTO(Guid id, string Name, int Age, string Position)
    {

    }
    //record x è immutabile, constr compatto, perfetto x dati di output (solo lettura), piu sicuro di classes normali.
    //here seleziona nel constrct solo i campi che vuoi esporre (qua selezioni id,Name,Age,Position)
}
