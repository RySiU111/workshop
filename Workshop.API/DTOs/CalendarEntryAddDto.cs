using System;
using System.ComponentModel.DataAnnotations;
using Workshop.API.Models;

namespace Workshop.API.DTOs
{
    public class CalendarEntryAddDto
    {
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int SubtaskId { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public double Hours { get; set; }
        public string Description { get; set; }

        public Models.ValidationResult Validate()
        { 
            var result = new Models.ValidationResult();

            if(Hours > 24)
            {
                result.IsSuccess = false;
                result.Errors.Add("Niepoprawna ilość godzin");
            }

            return result;
        }
    }
}