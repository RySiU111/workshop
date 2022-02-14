using System;

namespace Workshop.API.DTOs
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string JobTitle { get; set; }
        public double HourlyWage { get; set; }
        public DateTime DateOfEmployment { get; set; }
        public DateTime? DateOfTerminationOfEmployment { get; set; }
    }
}