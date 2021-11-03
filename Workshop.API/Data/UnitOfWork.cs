using System.Threading.Tasks;
using Workshop.API.Data.Repositories;
using Workshop.API.Interfaces;

namespace Workshop.API.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WorkshopContext _context;

        public UnitOfWork(WorkshopContext context)
        {
            _context = context;
        }

        public ClientAppRepository ClientAppRepository => new ClientAppRepository(_context);
        public CarServiceRepository CarServiceRepository => new CarServiceRepository(_context);
        public KanbanRepository KanbanRepository => new KanbanRepository(_context);

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}