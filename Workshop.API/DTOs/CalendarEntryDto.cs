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
        public DateTime Date { get; set; }
        public double Hours { get; set; }
        public string Description { get; set; }
        public bool IsPlanned { get; set; }

        public ValidationResult Validate()
        { 
            var result = new ValidationResult();

            if(Hours > 24)
            {
                result.IsSuccess = false;
                result.Errors.Add("Niepoprawna ilość godzin");
            }

            return result;
        }
    }
}