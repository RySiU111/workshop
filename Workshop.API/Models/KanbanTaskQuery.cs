using System;

namespace Workshop.API.Models
{
    public class KanbanTaskQuery
    {
        public string VIN { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public DateTime? DateExactly { get; set; }

        public bool Validate()
        {
            var result = true;

            if(DateFrom.HasValue && DateTo.HasValue)
                result &= DateFrom.Value <= DateTo.Value;

            if(DateExactly.HasValue)
            {
                result &= !DateFrom.HasValue;
                result &= !DateTo.HasValue;
            }

            return result;
        }
    }
}