using System.Collections.Generic;
using System.Threading.Tasks;
using Workshop.API.Entities;

namespace Workshop.API.Interfaces
{
    public interface IKanbanRepository
    {
        Task<KanbanTask> GetKanbanTask(int? id, bool? isInnerComment = null, string vin = null);
        Task<List<KanbanTask>> GetKanabanTasks();
        void AddKanbanTask(KanbanTask kanbanTask);
        void EditKanbanTask(KanbanTask kanbanTask);
        void DeleteKanbanTask(KanbanTask kanbanTask);
        Task<List<KanbanTask>> GetCarHistory(string vin);

        Task<List<Subtask>> GetSubtasks(int kanbanTaskId);
        void AddSubtask(Subtask subtask);
        void EditSubtask(Subtask subtask);
        void DeleteSubtask(Subtask subtask);
        Task<Subtask> GetSubtask(int id);

        void AddKanbanComment(KanbanComment kanbanComment);
        void EditKanbanComment(KanbanComment kanbanComment);
        void DeleteKanbanComment(KanbanComment kanbanComment);
        Task<KanbanComment> GetKanbanComment(int id);
        Task<List<KanbanComment>> GetKanbanComments(int kanbanTaskId, bool? isInnerComment = null);

        void EditBasketItem(BasketItem basketItem);
        void DeleteBasketItem(BasketItem basketItem);
        Task<BasketItem> GetBasketItem(int basketItemId);
        Task<List<BasketItem>> GetBasketItemsByState(List<BasketItemState> states);
        void AddBasketItem(BasketItem basketItem);
    }
}