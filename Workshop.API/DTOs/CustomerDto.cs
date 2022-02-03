using System.ComponentModel.DataAnnotations;
using Workshop.API.Extensions;

namespace Workshop.API.DTOs
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        [Required]
        [BooleanTrueRequired]
        public bool ConsentToTheProcessingOfPersonalData { get; set; }
    }
}