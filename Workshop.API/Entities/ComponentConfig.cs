using System.ComponentModel.DataAnnotations;

namespace Workshop.API.Entities
{
    public class ComponentConfig
    {
        [Key]
        [RegularExpression("[^!@#$%^&*()-+={}[\\]:\";'<>,\\.\\/\\?\\\\|\n]+")]
        public string ComponentName { get; set; }
        [Required]
        public string Data { get; set; }
        public bool IsActive { get; set; } = true;
    }
}