using System;

namespace Workshop.API.Entities
{
    public class Invoice
    {
        public int Id { get; set; }
        public bool IsActive { get; set; } = true;
        public int Number { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int KanbanTaskId { get; set; }
        public KanbanTask KanbanTask { get; set; } 
        public int UserId { get; set; }
        public User User { get; set; }  
        public DateTime DateOfCeation { get; set; } = DateTime.Now;
        public double PriceBrutto { get; set; }
        public double PriceNetto { get; set; }
        public double VAT { get; set; }
        public string InvoiceCode { get; set; } 
    }
}