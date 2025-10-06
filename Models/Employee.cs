﻿using System.ComponentModel.DataAnnotations;

namespace Company_Management_Panel_CSharp.Models
{
    public class Employee
    {
        [Key] 
        public int Id { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [StringLength(50, ErrorMessage = "First name can't be longer than 50 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(50, ErrorMessage = "Last name can't be longer than 50 characters")]
        public string LastName { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string? Email { get; set; }

        [Phone(ErrorMessage = "Invalid phone number format")]
        public string? PhoneNumber { get; set; }
        
        [Required] 
        public int CompanyId { get; set; }
        
        public Company? Company { get; set; }
    }
}
