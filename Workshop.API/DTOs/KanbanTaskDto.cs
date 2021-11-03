using System;
using Workshop.API.Entities;

namespace Workshop.API.DTOs
{
    public class KanbanTaskDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfCreation { get; set; }
        public DateTime DateOfPlannedRealization { get; set; }
        public DateTime DateOfActualRealizatoin { get; set; }
        public KanbanTaskStatus Status { get; set; } = 0;
    }
}