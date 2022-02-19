using System;

namespace Workshop.API.Models
{
    public class InvoiceQuery
    {
        public int? InvoiceId { get; set; }
        public int? UserId { get; set; }
        public int? KanbanTaskId { get; set; }
        public string InvoiceCode { get; set; }
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