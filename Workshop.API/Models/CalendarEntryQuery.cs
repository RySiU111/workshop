using System;

namespace Workshop.API.Models
{
    public class CalendarEntryQuery
    {
        public int? Id { get; set; }
        public int? UserId { get; set; }
        public int? SubtaskId { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public bool? IsPlanned { get; set; }

        public bool Validate()
        {
            var result = true;

            if(DateFrom.HasValue && DateTo.HasValue)
                result &= DateFrom.Value <= DateTo.Value;

            return result;
        }
    }
}