using System;
using System.ComponentModel.DataAnnotations;

namespace Workshop.API.Entities
{
    public enum KanbanTaskStatus
    {
        New = 0,
        ToDo = 1,
        InDiagnostics = 2,
        InRealization = 3,
        Frozen = 4,
        Done = 5
    }

    public class KanbanTask
    {
        public int Id { get; set; }
        public int ServiceRequestId { get; set; }
        public ServiceRequest ServiceRequest { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateOfCreation { get; set; }
        public DateTime DateOfPlannedRealization { get; set; }
        public DateTime DateOfActualRealizatoin { get; set; }
        public KanbanTaskStatus Status { get; set; } = 0;
        public bool IsActive { get; set; } = true;

        #region CarSpec
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
        #endregion 
    }
}