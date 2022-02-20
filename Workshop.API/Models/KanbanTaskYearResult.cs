namespace Workshop.API.Models
{
    public class KanbanTaskYearResult
    {
        public int Month { get; set; }
        public int CountDone { get; set; }
        public int CountNewAndInProgress { get; set; }
    }
}