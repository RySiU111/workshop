using System;

namespace Workshop.API.Models
{
    public class ReportQuery
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }

        public bool Validate()
        {
            var result = true;

            if(DateFrom.HasValue && DateTo.HasValue)
                result &= DateFrom.Value <= DateTo.Value;

            return result;
        }
    }
}