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
                query = query.Where(c => c.UserId == queryModel.UserId);

            if(queryModel.DateFrom.HasValue)
                query = query.Where(c => c.DateFrom.Date <= queryModel.DateFrom.Value.Date);

            if(queryModel.DateTo.HasValue)
                query = query.Where(c => c.DateTo.Date <= queryModel.DateTo.Value.Date);

            query = query
                .Include(c => c.User)
                .Include(c => c.Subtask);

            return query.ToListAsync();
        }
    }
}