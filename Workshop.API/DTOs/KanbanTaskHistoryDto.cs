using System;
using System.Collections.Generic;
using System.Linq;
using Workshop.API.Entities;

namespace Workshop.API.DTOs
{
    public class KanbanTaskHistoryDto
    {
        public int Id { get; set; }
        public int? ServiceRequestId { get; set; }
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateOfActualRealizatoin { get; set; }
        public List<SubtaskDto> Subtasks { get; set; }
        public List<BasketItemDto> BasketItems { get; set; }
        public double TotalBasketPrice { get => BasketItems.Where(bi => bi.IsActive).Sum(bi => bi.Price * bi.Amount); }
        public double TotalWorkHoursCosts { get; set; }
        public double PlannedWorkHoursCosts { get; set; }
    }
}