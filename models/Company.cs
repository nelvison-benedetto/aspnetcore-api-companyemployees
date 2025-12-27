using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyEmployees.models
{
    public class Company
    {
        [Column("CompanyId")]
        public Guid Id { get; set; }  //added guid

        [Required(ErrorMessage="company name is required")]
        [MaxLength(60, ErrorMessage ="company name max length 60 chars")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "company address name is required")]
        [MaxLength(60, ErrorMessage = "company address max length 60 chars")]
        public string? Address { get; set; }


        public string? Country { get; set; }

        public ICollection<Employee>? Employees { get; set; }

    }
}
