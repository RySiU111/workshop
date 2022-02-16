using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Workshop.API.Data.Repositories;
using Workshop.API.Entities;
using Workshop.API.Interfaces;

namespace Workshop.API.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WorkshopContext _context;
        private readonly UserManager<User> _userManager;

        public UnitOfWork(WorkshopContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public ClientAppRepository ClientAppRepository => new ClientAppRepository(_context);
        public CarServiceRepository CarServiceRepository => new CarServiceRepository(_context);
        public KanbanRepository KanbanRepository => new KanbanRepository(_context);
        public CalendarRepository CalendarRepository => new CalendarRepository(_context);
        public InvoiceRepository InvoiceRepository => new InvoiceRepository(_context);
        public ReportsRepository ReportsRepository => new ReportsRepository(_context, _userManager);

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}