namespace Workshop.API.Entities
{
    public enum SubtaskStatus
    {
        ToDo = 0,
        InProgress = 1,
        Done = 2
    }

    public class Subtask
    {
        public int Id { get; set; }
        public int KanbanTaskId { get; set; }
        public string Name { get; set; }
        public double ManHour { get; set; }
        public bool IsActive { get; set; } = true;
        public SubtaskStatus Status { get; set; }
        public KanbanTask KanbanTask { get; set; }
    }
}