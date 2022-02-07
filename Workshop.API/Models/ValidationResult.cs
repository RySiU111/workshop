using System.Collections.Generic;

namespace Workshop.API.Models
{
    public class ValidationResult
    {
        public bool IsSuccess { get; set; } = true;
        public List<string> Errors { get; set; } = new List<string>();
    }
}