using System.Collections.Generic;
using Workshop.API.Entities;

namespace Workshop.API.Models
{
    public class EmployeesReportResult
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public double Hours { get; set; }
        public double PlannedHours { get; set; }
        public List<EmployeeKanbanTaskResult> KanbanTaskResults { get; set; }
        public List<EmployeeSubtaskResult> EmployeeSubtaskResults { get; set; }
    }

    public class EmployeeKanbanTaskResult
    {
        public KanbanTaskStatus Status { get; set; }
        public int Count { get; set; }
    }

    public class EmployeeSubtaskResult
    {
        public SubtaskStatus Status { get; set; }
        public int Count { get; set; }
    }
}