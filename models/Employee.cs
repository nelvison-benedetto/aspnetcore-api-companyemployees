using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//x usare le annotazioni  '[]'

namespace CompanyEmployees.models
{
    public class Employee
    {
        [Column("EmployeeId")]
        public Guid Id { get; set; }  //added guid

        [Required(ErrorMessage = "employee name is required")]
        [MaxLength(30, ErrorMessage = "employee name max length 30 chars")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "employee age is required")]
        public int Age { get; set; }

        [Required(ErrorMessage = "employee position is required")]
        [MaxLength(20, ErrorMessage = "employee position max length 20 chars")]
        public string? Position { get; set; }

        [ForeignKey(nameof(Company))]  //fk x link Employee-Company
        public Guid CompanyId { get; set; }  //Guid xk anche Company.Id è Guid
        

        //navigation properties
        public Company? Company { get; set; }  //navigation property, serve x fare e.g. employee.Company.Name  !! molto utile!!
          //OBBLIGATORIA xk qui c'è la fk CompanyId


    }
}
