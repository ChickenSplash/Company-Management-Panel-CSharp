using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Company_Management_Panel_CSharp.Models
{
    public class Company
    {
        
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Company name is required")]
        [StringLength(50, ErrorMessage = "Company name can't be longer than 50 characters")]
        public string Name { get; set; } = string.Empty;

        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string? Email { get; set; }

        [NotMapped]
        public IFormFile? Logo { get; set; }

        public string? LogoPath { get; set; }

        [Url(ErrorMessage = "Invalid URL format")]
        public string? Website { get; set; }
        
        public List<Employee> Employees { get; set; } = new();
    }
}
