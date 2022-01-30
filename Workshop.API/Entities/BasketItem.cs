using System;

namespace Workshop.API.Entities
{
    public enum BasketItemState
    {
        New = 0,
        InRealisation = 1,
        Completed = 2,
        Removed = 3
    }

    public enum UnitOfMeasure
    {
        Pieces = 0,
        Mass = 1,
        Volume
    }

    public class BasketItem
    {
        public int Id { get; set; }
        public bool IsActive { get; set; } = true;
        public string ItemName { get; set; }
        public double Price { get; set; }
        public double Amount { get; set; }
        public UnitOfMeasure UnitOfMeasure { get; set; }
        public BasketItemState BasketItemState { get; set; } = 0;
        public DateTime DateOfAddedToCart { get; set; }
        public DateTime DateOfPurchase { get; set; }
        public DateTime DateOfDelivery { get; set; }
        public int KanbanTaskId { get; set; }
        public KanbanTask KanbanTask { get; set; }
    }
}