namespace CompanyEmployees.models
{
    public record CompanyForCreationDTO(string Name, string Address, string Country)
    {
    }
    //è un DTO di input: cioe con i dati che ti invia l'utente tramite un POST/PUT
    //ma per questo tipo di DTO aggiungere validazioni ect , come in EmployeeForCreationDTO.cs!!

}
