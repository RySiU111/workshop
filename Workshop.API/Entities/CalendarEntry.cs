using System;

namespace Workshop.API.Entities
{
    public class CalendarEntry
    {
        public int Id { get; set; }
        public bool IsActive { get; set; } = true;
        public int UserId { get; set; }
        public User User { get; set; }
        public int SubtaskId { get; set; }
        public Subtask Subtask { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public double Hours { get; set; }
        public string Description { get; set; }
    }
}