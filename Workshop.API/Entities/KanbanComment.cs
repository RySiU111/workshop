using System;

namespace Workshop.API.Entities
{
    public class KanbanComment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int KanbanTaskId { get; set; }
        public KanbanTask KanbanTask { get; set; }
        public string Content { get; set; }
        public DateTime DateOfCreation { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;
        public bool IsInnerComment { get; set; }
    }
}