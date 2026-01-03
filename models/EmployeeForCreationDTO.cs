using System.ComponentModel.DataAnnotations;

namespace CompanyEmployees.models
{
    //public record EmployeeForCreationDTO(string Name, int Age, string Position)
    //{
    //} records compatti non supportano bene DataAnnotations sui parametri!meglio farlo bene dettagliato

    public record EmployeeForCreationDTO
    {
        [Required(ErrorMessage = "employee name is required")]
        [MaxLength(30, ErrorMessage = "employee name max length 30 chars")]
        public string? Name { get; init; }  //usa 'init' x immutabilita (assegnabile solo in creazione, dopo solo readonly)!

        [Required(ErrorMessage = "employee age is required")]
        public int Age { get; set; }

        [Required(ErrorMessage = "employee position is required")]
        [MaxLength(20, ErrorMessage = "employee name max length 20 chars")]

        public string? Position { get; init; } //usa 'init'!
    }
    //è un DTO di input: cioe con i dati che ti invia l'utente tramite un POST/PUT
    //per questo tipo di DTO aggiungere sempre validazioni ect

}
