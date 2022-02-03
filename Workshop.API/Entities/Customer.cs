using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Workshop.API.Extensions;

namespace Workshop.API.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        [Required]
        [MaxLength(30)]
        public string Surname { get; set; }
        [Required]
        [MaxLength(15)]
        [RegularExpression("\\d+")]
        public string PhoneNumber { get; set; }
        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [BooleanTrueRequired]
        public bool ConsentToTheProcessingOfPersonalData { get; set; }

        public List<ServiceRequest> ServiceRequests { get; set; }
        public List<KanbanTask> KanbanTasks { get; set; }
    }
}