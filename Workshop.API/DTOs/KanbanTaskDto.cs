using System;
using Workshop.API.Entities;

namespace Workshop.API.DTOs
{
    public class KanbanTaskDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfPlannedRealization { get; set; }
        public DateTime DateOfActualRealizatoin { get; set; }
        public DateTime DateOfCarDelivery { get; set; }
        public KanbanTaskStatus Status { get; set; } = 0;
        public string Make { get; set; }
        public string Model { get; set; }
    }
}