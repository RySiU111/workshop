using System;

namespace Workshop.API.DTOs
{
    public class InvoiceDto
    {
        public int Id { get; set; }
        public bool IsActive { get; set; } = true;
        public int Number { get; set; }
        public int CustomerId { get; set; }
        public string CustomerNameAndSurname { get; set; }
        public int KanbanTaskId { get; set; }
        public string KanbanTaskName { get; set; }
        public int UserId { get; set; }
        public string UserNameAndSurname { get; set; }
        public DateTime DateOfCeation { get; set; }
        public double PriceBrutto { get; set; }
        public double WorkHoursPriceBrutto { get; set; }
        public double PriceNetto { get; set; }
        public double WorkHoursPriceNetto { get; set; }
        public double VAT { get; set; }
        public string InvoiceCode { get; set; } 
    }
}