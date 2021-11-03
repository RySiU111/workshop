using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Workshop.API.Entities;
using Workshop.API.Interfaces;

namespace Workshop.API.Data.Repositories
{
    public class KanbanRepository : IKanbanRepository
    {
        private readonly WorkshopContext _context;
        public KanbanRepository(WorkshopContext context)
        {
            _context = context;
        }

        public void AddKanbanTask(KanbanTask kanbanTask)
        {
            _context.KanbanTasks.Add(kanbanTask);
        }

        public void DeleteKanbanTask(KanbanTask kanbanTask)
        {
            kanbanTask.IsActive = false;
            _context.KanbanTasks.Update(kanbanTask);
        }

        public void EditKanbanTask(KanbanTask kanbanTask)
        {
            _context.KanbanTasks.Update(kanbanTask);
        }

        public async Task<List<KanbanTask>> GetKanabanTasks()
        {
            var kanbanTasks = await _context.KanbanTasks
                .Where(k => k.IsActive)
                .ToListAsync();

            return kanbanTasks;
        }

        public async Task<KanbanTask> GetKanbanTask(int id)
        {
            var kanbanTask = await _context.KanbanTasks
                .Include(k => k.ServiceRequest)
                    .ThenInclude(sr => sr.Customer)
                .FirstOrDefaultAsync(k => k.Id == id);

            return kanbanTask;
        }
    }
}