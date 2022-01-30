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

        Task<List<Subtask>> GetSubtasks(int kanbanTaskId);
        void AddSubtask(Subtask subtask);
        void EditSubtask(Subtask subtask);
        void DeleteSubtask(Subtask subtask);
        Task<Subtask> GetSubtask(int id);

        void AddKanbanComment(KanbanComment kanbanComment);
        void EditKanbanComment(KanbanComment kanbanComment);
        void DeleteKanbanComment(KanbanComment kanbanComment);
        Task<KanbanComment> GetKanbanComment(int id);
        Task<List<KanbanComment>> GetKanbanComments(int kanbanTaskId);

        void EditBasketItem(BasketItem basketItem);
        void DeleteBasketItem(BasketItem basketItem);
        Task<BasketItem> GetBasketItem(int basketItemId);
        Task<List<BasketItem>> GetBasketItemsByState(List<BasketItemState> states);
        void AddBasketItem(BasketItem basketItem);
    }
}