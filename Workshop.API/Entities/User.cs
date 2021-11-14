using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Workshop.API.Entities
{
    public class User : IdentityUser<int>
    {
        public ICollection<UserRole> UserRoles { get; set; }
        public List<KanbanComment> UserKanbanComments { get; set; }
    }
}