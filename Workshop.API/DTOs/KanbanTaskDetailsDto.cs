using System;
using System.Collections.Generic;
using Workshop.API.Entities;

namespace Workshop.API.DTOs
{
    public class KanbanTaskDetailsDto
    {
        public int Id { get; set; }
        public int? ServiceRequestId { get; set; }
        public int CustomerId { get; set; }
        public CustomerDto Customer { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ProtocolNumber { get; set; }
        public DateTime DateOfCreation { get; set; } = DateTime.Now;
        public DateTime DateOfPlannedRealization { get; set; }
        public DateTime DateOfActualRealizatoin { get; set; }
        public DateTime DateOfCarDelivery { get; set; }
        public KanbanTaskStatus Status { get; set; } = 0;
        public bool IsActive { get; set; } = true;
        public List<SubtaskDto> Subtasks { get; set; }
        public List<KanbanCommentDto> Comments { get; set; }
        public List<BasketItemDto> BasketItems { get; set; }
        public List<PhotoDto> Photos { get; set; }

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