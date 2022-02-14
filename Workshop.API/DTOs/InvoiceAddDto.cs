using System.ComponentModel.DataAnnotations;

namespace Workshop.API.DTOs
{
    public class InvoiceAddDto
    {
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public int KanbanTaskId { get; set; }
        [Required]
        public int UserId { get; set; }
    }
}