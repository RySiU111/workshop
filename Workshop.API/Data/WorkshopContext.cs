using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Workshop.API.Entities;

namespace Workshop.API.Data
{
    public class WorkshopContext : IdentityDbContext<User, Role, int, 
        IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>, 
        IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public WorkshopContext(DbContextOptions options) : base(options) { }

        public DbSet<ComponentConfig> ComponentConfigs { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<ServiceRequest> ServiceRequests { get; set; }
        public DbSet<KanbanTask> KanbanTasks { get; set; }
        public DbSet<Subtask> Subtasks { get; set; }
        public DbSet<KanbanComment> KanbanComments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>()
                .HasMany(u => u.UserRoles)
                .WithOne(ur =>ur.User)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();

            builder.Entity<Role>()
                .HasMany(r => r.UserRoles)
                .WithOne(ur =>ur.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();
        }
    }
}