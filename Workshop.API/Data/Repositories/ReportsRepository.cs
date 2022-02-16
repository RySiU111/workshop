using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Workshop.API.Entities;
using Workshop.API.Models;

namespace Workshop.API.Data.Repositories
{
    public class ReportsRepository
    {
        private readonly WorkshopContext _context;
        private readonly UserManager<User> _userManager;

        public ReportsRepository(WorkshopContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<List<KanbanTasksReportResult>> GetKanbanTasksReport(ReportQuery queryModel)
        {
            var query = _context.KanbanTasks
                .Where(k => k.IsActive);

            if (queryModel.DateFrom.HasValue)
                query = query.Where(x =>
                    x.DateOfCarDelivery.Date >= queryModel.DateFrom.Value.Date);

            if (queryModel.DateTo.HasValue)
                query = query.Where(x =>
                    x.DateOfCarDelivery.Date <= queryModel.DateTo.Value.Date);

            query = query
                .Include(k => k.Subtasks)
                    .ThenInclude(s => s.CalendarEntries);

            var result = await query.ToListAsync();

            return result.GroupBy(k => k.Status).Select(k => new KanbanTasksReportResult
            {
                Status = k.Key,
                Count = k.Count(),
                HoursSum = k.Sum(x => x.Subtasks
                    .Where(x => x.IsActive)
                    .Sum(s => s.CalendarEntries
                        .Where(c => !c.IsPlanned && c.IsActive)
                        .Sum(c => c.Hours))),
                PlannedHoursSum = k.Sum(x => x.Subtasks
                    .Where(x => x.IsActive)
                    .Sum(s => s.CalendarEntries
                        .Where(c => c.IsPlanned && c.IsActive)
                        .Sum(c => c.Hours))),
                HoursAvg = k.Average(x => x.Subtasks
                    .Where(x => x.IsActive)
                    .Sum(s => s.CalendarEntries
                        .Where(c => !c.IsPlanned && c.IsActive)
                        .Sum(c => c.Hours))),
                PlannedHoursAvg = k.Average(x => x.Subtasks
                    .Where(x => x.IsActive)
                    .Sum(s => s.CalendarEntries
                        .Where(c => c.IsPlanned && c.IsActive)
                        .Sum(c => c.Hours)))
            }).ToList();
        }

        public async Task<List<EmployeesReportResult>> GetEmployeesReport(ReportQuery queryModel)
        {
            var query = _userManager.Users.AsQueryable();

            query = query
                .Include(u => u.KanbanTasks)
                .Include(u => u.Subtasks)
                .Include(u => u.CalendarEntries
                    .Where(c => queryModel.DateFrom.HasValue ? 
                        c.Date.Date >= queryModel.DateFrom.Value.Date :
                        true &&
                        queryModel.DateTo.HasValue ?
                        c.Date.Date <= queryModel.DateTo.Value.Date :
                        true));

            var result = await query.ToListAsync();

            return result.Select(u => new EmployeesReportResult
            {
                UserId = u.Id,
                Name = u.Name,
                Surname = u.Surname,
                Hours = u.CalendarEntries.Where(c => !c.IsPlanned && c.IsActive).Sum(c => c.Hours),
                PlannedHours = u.CalendarEntries.Where(c => c.IsPlanned && c.IsActive).Sum(c => c.Hours),
                KanbanTaskResults = u.KanbanTasks
                    .GroupBy(k => k.Status)
                    .Select(g => new EmployeeKanbanTaskResult
                        {
                            Status = g.Key,
                            Count = g.Count()
                        })
                    .ToList(),
                EmployeeSubtaskResults = u.Subtasks
                    .GroupBy(k => k.Status)
                    .Select(g => new EmployeeSubtaskResult
                        {
                            Status = g.Key,
                            Count = g.Count()
                        })
                    .ToList()
                
            }).ToList();
        }
    }
}