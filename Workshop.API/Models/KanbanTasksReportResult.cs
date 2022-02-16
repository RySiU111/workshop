using Workshop.API.Entities;

namespace Workshop.API.Models
{
    public class KanbanTasksReportResult
    {
        public KanbanTaskStatus Status { get; set; }
        public int Count { get; set; }
        public double HoursSum { get; set; }
        public double PlannedHoursSum { get; set; }
        public double HoursAvg { get; set; }
        public double PlannedHoursAvg { get; set; }
    }
}