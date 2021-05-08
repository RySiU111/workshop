using Microsoft.EntityFrameworkCore;

namespace Workshop.Infra
{
    public class WorkshopContext : DbContext
    {
        public WorkshopContext(DbContextOptions options) : base(options)
        {
            
        }
    }
}