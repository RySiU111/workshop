using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Workshop.API.Entities
{
    public class User : IdentityUser<int>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string JobTitle { get; set; }
        public double HourlyWage { get; set; }
        public DateTime DateOfEmployment { get; set; }
        public DateTime? DateOfTerminationOfEmployment { get; set; }
        public bool IsActive { get; set; } = true;
        public ICollection<UserRole> UserRoles { get; set; }
        public List<KanbanComment> UserKanbanComments { get; set; }
        public List<KanbanTask> KanbanTasks { get; set; }
        public List<Subtask> Subtasks { get; set; }
        public List<CalendarEntry> CalendarEntries { get; set; }

    }
}