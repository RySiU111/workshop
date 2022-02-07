using System;
using Workshop.API.Models;

namespace Workshop.API.DTOs
{
    public class CalendarEntryDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public EmployeeDto User { get; set; }
        public int SubtaskId { get; set; }
        public SubtaskDto Subtask { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public double Hours { get; set; }
        public string Description { get; set; }

        public ValidationResult Validate()
        { 
            var result = new ValidationResult();

            if(DateFrom > DateTo)
            {
                result.IsSuccess &= false;
                result.Errors.Add("Niepoprawna data");
            }

            if(Hours > (DateTo - DateFrom).TotalHours)
            {
                result.IsSuccess &= false;
                result.Errors.Add("Niepoprawna ilość godzin");
            }

            return result;
        }
    }
}