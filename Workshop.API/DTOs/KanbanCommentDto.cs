using System;
using System.ComponentModel.DataAnnotations;

namespace Workshop.API.DTOs
{
    public class KanbanCommentDto
    {
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int KanbanTaskId { get; set; }
        [Required]
        public string Content { get; set; }
        public string UserName { get; set; }
        public DateTime DateOfCreation { get; set; } = DateTime.Now;
        [Required]
        public bool IsInnerComment { get; set; }
    }
}