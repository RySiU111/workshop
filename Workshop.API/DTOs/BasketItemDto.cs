using System;
using Workshop.API.Entities;

namespace Workshop.API.DTOs
{
    public class BasketItemDto
    {
        public int Id { get; set; }
        public bool IsActive { get; set; } = true;
        public string ItemName { get; set; }
        public double Price { get; set; }
        public BasketItemState BasketItemState { get; set; }
        public DateTime DateOfAddedToCart { get; set; }
        public DateTime DateOfPurchase { get; set; }
        public DateTime DateOfDelivery { get; set; }
        public int KanbanTaskId { get; set; }
    }
}