using Workshop.API.Entities;

namespace Workshop.API.DTOs
{
    public class SubtaskDto
    {
        public int Id { get; set; }
        public int KanbanTaskId { get; set; }
        public int? UserId { get; set; } = null;
        public string Name { get; set; }
        public double ManHour { get; set; }
        public SubtaskStatus Status { get; set; }
        public EmployeeDto User { get; set; }
    }
}