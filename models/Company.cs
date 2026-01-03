using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//x usare le annotazioni  '[]'

namespace CompanyEmployees.models
{
    public class Company
    {
        [Column("CompanyId")]  //set name col in db
        public Guid Id { get; set; }  //idendificatore univoco e.g.3f2504e0-4f89-11d3-9a0c-0305e82c3301
            //EF capisce auto che è la pk perche è chiamata Id.

        [Required(ErrorMessage="company name is required")]
        [MaxLength(60, ErrorMessage ="company name max length 60 chars")]
        public string? Name { get; set; }  //? indica che puo essere null a compile-time (se lo togli puoi avere errors compile-time, ma cmnq EF works ok), cmnq [Required] impedisce che sia null a runtime + DB level
        //sui fields '?' il compilatore accetta che sia null (senza avresti warning continui). '?' sulle navigation properties serve x EF & db.

        [Required(ErrorMessage = "company address name is required")]
        [MaxLength(60, ErrorMessage = "company address max length 60 chars")]
        public string? Address { get; set; }

        public string? Country { get; set; }

        
        //navigation properties
        public ICollection<Employee>? Employees { get; set; }  //? xk a runtime puo essere null (EF carica solo tab company), poi cmnq quando loads a richiesta allora gli employees non saranno piu null
        //relation one-to-many con Employee, una company ha molti employees. ora puoi fare company.Employees

    }
}
