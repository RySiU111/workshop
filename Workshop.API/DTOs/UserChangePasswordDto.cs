using System.ComponentModel.DataAnnotations;

namespace Workshop.API.DTOs
{
    public class UserChangePasswordDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string CurrentPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }
    }
}