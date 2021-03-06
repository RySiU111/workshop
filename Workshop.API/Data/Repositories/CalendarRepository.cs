using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Workshop.API.Entities;
using Workshop.API.Interfaces;
using Workshop.API.Models;

namespace Workshop.API.Data.Repositories
{
    public class CalendarRepository : ICalendarRepository
    {
        private readonly WorkshopContext _context;

        public CalendarRepository(WorkshopContext context)
        {
            _context = context;
        }

        public void AddCalendarEntry(CalendarEntry entry)
        {
            _context.CalendarEntries.Add(entry);
        }

        public Task<List<CalendarEntry>> GetCalendarEntries(CalendarEntryQuery queryModel)
        {
            var query = _context.CalendarEntries
                .Where(c => c.IsActive == true);

            if(queryModel.Id.HasValue)
                query = query.Where(c => c.Id == queryModel.Id);

            if(queryModel.SubtaskId.HasValue)
                query = query.Where(c => c.SubtaskId == queryModel.SubtaskId);

            if(queryModel.UserId.HasValue)
                query = query.Where(c => c.UserId.HasValue && c.UserId.Value == queryModel.UserId);

            if(queryModel.DateFrom.HasValue)
                query = query.Where(c => c.Date.Date >= queryModel.DateFrom.Value.Date);

            if(queryModel.DateTo.HasValue)
                query = query.Where(c => c.Date.Date <= queryModel.DateTo.Value.Date);

            if(queryModel.IsPlanned.HasValue)
                query = query.Where(c => c.IsPlanned == queryModel.IsPlanned);

            query = query
                .Include(c => c.User)
                .Include(c => c.Subtask);

            return query.ToListAsync();
        }

        public async Task<List<User>> GetAvailableUsers(DateTime date)
        {
            var users = await _context.Users
                .Where(u => u.CalendarEntries.Any(c => c.Date.Date != date.Date && c.IsActive) || 
                    u.CalendarEntries.Where(c => c.Date.Date == date.Date && c.IsActive).Sum(c => c.Hours) < 8)
                .Include(u => u.CalendarEntries.Where(c => c.Date.Date == date.Date && c.IsActive))
                .ToListAsync();

            return users;
        }

        public void RemoveCalendarEntry(CalendarEntry entry)
        {
            entry.IsActive = false;
            _context.CalendarEntries.Update(entry);
        }

        public void EditCalendarEntry(CalendarEntry entry)
        {
            _context.CalendarEntries.Update(entry);
        }
    }
}