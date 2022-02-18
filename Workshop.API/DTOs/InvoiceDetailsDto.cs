using System;

namespace Workshop.API.DTOs
{
    public class InvoiceDetailsDto
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int CustomerId { get; set; }
        public CustomerDto Customer { get; set; }
        public int KanbanTaskId { get; set; }
        public KanbanTaskHistoryDto KanbanTask { get; set; } 
        public int UserId { get; set; }
        public EmployeeDto User { get; set; }  
        public DateTime DateOfCeation { get; set; } 
        public double PriceBrutto { get; set; }
        public double WorkHoursPriceBrutto { get; set; }
        public double PriceNetto { get; set; }
        public double WorkHoursPriceNetto { get; set; }
        public double VAT { get; set; }
        public string InvoiceCode { get; set; } 
    }
}