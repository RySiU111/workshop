using System;

namespace Workshop.API.Entities
{
    public class Photo
    {
        public int Id { get; set; }
        public int KanbanTaskId { get; set; }
        public KanbanTask KanbanTask { get; set; }
        public string PublicId { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public DateTime DateOfCreation { get; set; } = DateTime.Now;
    }
}