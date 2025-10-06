using System.ComponentModel.DataAnnotations;

namespace Company_Management_Panel_CSharp.Models
{
    public class Company
    {
        
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Company name is required")]
        [StringLength(50, ErrorMessage = "Company name can't be longer than 50 characters")]
        public string Name { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string? Email { get; set; }
        
        public string? Logo { get; set; }

        [Url(ErrorMessage = "Invalid URL format")]
        public string? Website { get; set; }
        
        public List<Employee> Employees { get; set; } = new();
    }
}
