using System;

namespace Workshop.API.DTOs
{
    public class PhotoDto
    {
        public int Id { get; set; }
        public int KanbanTaskId { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public DateTime DateOfCreation { get; set; } = DateTime.Now;
    }
}