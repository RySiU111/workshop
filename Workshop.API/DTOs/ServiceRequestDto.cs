using Workshop.API.Entities;

namespace Workshop.API.DTOs
{
    public class ServiceRequestDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string Description { get; set; }
        public string VIN { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int ProductionYear { get; set; }
        public string EngineDescription { get; set; }
        public int Power { get; set; }
        public ServiceRequestState State { get; set; } = 0;
        public CustomerDto Custormer { get; set; }
    }
}