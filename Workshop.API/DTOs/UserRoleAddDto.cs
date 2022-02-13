using System.ComponentModel.DataAnnotations;

namespace Workshop.API.DTOs
{
    public class UserRoleAddDto
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public string RoleName { get; set; }
    }
}