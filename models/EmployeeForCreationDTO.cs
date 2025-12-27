using System.ComponentModel.DataAnnotations;

namespace CompanyEmployees.models
{
    //public record EmployeeForCreationDTO(string Name, int Age, string Position)
    //{
    //}
    public record EmployeeForCreationDTO
    {
        [Required(ErrorMessage = "employee name is required")]
        [MaxLength(30, ErrorMessage = "employee name max length 30 chars")]

        public string? Name { get; init; }

        [Required(ErrorMessage = "employee age is required")]
        public int Age;

        [Required(ErrorMessage = "employee position is required")]
        [MaxLength(20, ErrorMessage = "employee name max length 20 chars")]

        public string? Position { get; init; }
    }
}
