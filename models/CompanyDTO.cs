namespace CompanyEmployees.models
{
    public record CompanyDTO(Guid id, string Name, string FullAddress)
    {

    }
    //record x è immutabile, perfetto x dati di output (solo lettura), piu sicuro di classes normali.
    //qua select solo i campi che vuoi esporre


}
