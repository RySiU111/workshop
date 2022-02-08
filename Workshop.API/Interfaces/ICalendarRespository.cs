using System.Collections.Generic;
using System.Threading.Tasks;
using Workshop.API.Entities;
using Workshop.API.Models;

namespace Workshop.API.Interfaces
{
    public interface ICalendarRepository
    {
        Task<List<CalendarEntry>> GetCalendarEntries(CalendarEntryQuery queryModel);
        void AddCalendarEntry(CalendarEntry entry);
    }
}