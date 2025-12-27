using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [ForeignKey(nameof(Company))]
        public Guid CompanyId { get; set; }
        public Company? Company { get; set; }
    }
}
