using System.ComponentModel.DataAnnotations;

namespace CompanyEmployees.models
{
    public record UserForAuthenticationDTO
    {
        [Required(ErrorMessage="username is required")]
        public string? UserName { get; set; }  //here 'set' al posto di 'init' xk è usato x auth/serializzazione/libs esterne ect. anche se 'init' sarebbe piu clean.

        [Required(ErrorMessage="password is required")]
        public string? Password { get; set; }

    }
    //è un DTO di input: cioe con i dati che ti invia l'utente tramite un POST/PUT
    //per questo tipo di DTO mettere sempre validations ect!!

}
