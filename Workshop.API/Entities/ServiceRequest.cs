using System.ComponentModel.DataAnnotations;

namespace Workshop.API.Entities
{
    public enum ServiceRequestState
    {
        New = 0,
        Accepted = 1,
        Rejected = 2
    }

    public class ServiceRequest
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public bool IsActive { get; set; } = true;
        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }
        [Required]
        [MinLength(17)]
        [MaxLength(17)]
        [RegularExpression("[^\\W]+")]
        public string VIN { get; set; }
        [Required]
        public string Make { get; set; }
        public string Model { get; set; }
        [Range(1900, int.MaxValue)]
        public int ProductionYear { get; set; }
        [MaxLength(300)]
        public string EngineDescription { get; set; }
        [Range(0, int.MaxValue)]
        public int Power { get; set; }
        public ServiceRequestState State { get; set; } = 0;

        public Customer Customer { get; set; }
    }
}