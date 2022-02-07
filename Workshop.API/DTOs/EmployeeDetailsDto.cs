using System;
using System.Collections.Generic;

namespace Workshop.API.DTOs
{
    public class EmployeeDetailsDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string JobTitle { get; set; }
        public double HourlyWage { get; set; }
        public DateTime DateOfEmployment { get; set; }
        public DateTime? DateOfTerminationOfEmployment { get; set; }
        public List<KanbanCommentDto> UserKanbanComments { get; set; }
        public List<KanbanTaskDto> KanbanTasks { get; set; }
        public List<SubtaskDto> Subtasks { get; set; }
        public List<CalendarEntryDto> CalendarEntries { get; set; }
    }
}