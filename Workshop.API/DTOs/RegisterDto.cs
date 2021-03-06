using System;
using System.ComponentModel.DataAnnotations;

namespace Workshop.API.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string JobTitle { get; set; }
        public double HourlyWage { get; set; }
        public DateTime DateOfEmployment { get; set; }
        public DateTime? DateOfTerminationOfEmployment { get; set; }
    }
}