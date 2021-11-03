using System.Collections.Generic;
using System.Threading.Tasks;
using Workshop.API.Entities;

namespace Workshop.API.Interfaces
{
    public interface IKanbanRepository
    {
        Task<KanbanTask> GetKanbanTask(int id);
        Task<List<KanbanTask>> GetKanabanTasks();
        void AddKanbanTask(KanbanTask kanbanTask);
        void EditKanbanTask(KanbanTask kanbanTask);
        void DeleteKanbanTask(KanbanTask kanbanTask);
    }
}