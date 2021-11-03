using System;
using Workshop.API.Entities;

namespace Workshop.API.DTOs
{
    public class KanbanTaskDetailsDto
    {
        public int Id { get; set; }
        public int ServiceRequestId { get; set; }
        public ServiceRequestDto ServiceRequest { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateOfCreation { get; set; }
        public DateTime DateOfPlannedRealization { get; set; }
        public DateTime DateOfActualRealizatoin { get; set; }
        public KanbanTaskStatus Status { get; set; } = 0;
        public bool IsActive { get; set; } = true;

        #region CarSpec
        public string VIN { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int ProductionYear { get; set; }
        public string EngineDescription { get; set; }
        public int Power { get; set; }
        #endregion 
    }
}