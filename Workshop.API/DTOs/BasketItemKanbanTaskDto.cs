using System;
using Workshop.API.Entities;

namespace Workshop.API.DTOs
{
    public class BasketItemKanbanTaskDto
    {
        public int Id { get; set; }
        public bool IsActive { get; set; } = true;
        public string ItemName { get; set; }
        public double Price { get; set; }
        public BasketItemState BasketItemState { get; set; }
        public double Amount { get; set; }
        public UnitOfMeasure UnitOfMeasure { get; set; }
        public DateTime DateOfAddedToCart { get; set; } = DateTime.Now;
        public DateTime DateOfPurchase { get; set; }
        public DateTime DateOfDelivery { get; set; }
        public int KanbanTaskId { get; set; }
        public KanbanTaskBasketDto KanbanTask { get; set; }
    }
}