using Workshop.API.Entities;

namespace Workshop.API.DTOs
{
    public class SubtaskDto
    {
        public int Id { get; set; }
        public int KanbanTaskId { get; set; }
        public string Name { get; set; }
        public double ManHour { get; set; }
        public SubtaskStatus Status { get; set; }
    }
}