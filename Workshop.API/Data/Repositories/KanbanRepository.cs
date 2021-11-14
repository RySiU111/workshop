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

        public void AddSubtask(Subtask subtask)
        {
            _context.Subtasks.Add(subtask);
        }

        public void DeleteKanbanTask(KanbanTask kanbanTask)
        {
            kanbanTask.IsActive = false;
            _context.KanbanTasks.Update(kanbanTask);
        }

        public void DeleteSubtask(Subtask subtask)
        {
            subtask.IsActive = false;
            _context.Subtasks.Update(subtask);
        }

        public void EditKanbanTask(KanbanTask kanbanTask)
        {
            _context.KanbanTasks.Update(kanbanTask);
        }

        public void EditSubtask(Subtask subtask)
        {
            _context.Subtasks.Update(subtask);
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
                .Include(k => k.Subtasks
                    .Where(s => s.IsActive == true))
                .FirstOrDefaultAsync(k => k.Id == id);

            return kanbanTask;
        }

        public async Task<Subtask> GetSubtask(int id)
        {
            var subtask = await _context.Subtasks.
                FirstOrDefaultAsync(s => s.Id == id);

            return subtask;
        }

        public async Task<List<Subtask>> GetSubtasks(int kanbanTaskId)
        {
            var subtasks = await _context.Subtasks
                .Where(s => s.KanbanTaskId == kanbanTaskId)
                .ToListAsync();

            return subtasks;
        }
    }
}