using System.Collections.Generic;
using Workshop.API.Entities;

namespace Workshop.API.DTOs
{
    public class UserDetailsDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public ICollection<UserRoleDto> UserRoles { get; set; }
    }
}