using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Workshop.API.Entities;
using Workshop.API.Interfaces;
using Workshop.API.Models;

namespace Workshop.API.Data.Repositories
{
    public class KanbanRepository : IKanbanRepository
    {
        private readonly WorkshopContext _context;
        public KanbanRepository(WorkshopContext context)
        {
            _context = context;
        }

        public void AddKanbanComment(KanbanComment kanbanComment)
        {
            _context.KanbanComments.Add(kanbanComment);
        }

        public void AddKanbanTask(KanbanTask kanbanTask)
        {
            _context.KanbanTasks.Add(kanbanTask);
        }

        public void AddSubtask(Subtask subtask)
        {
            _context.Subtasks.Add(subtask);
        }

        public void DeleteKanbanComment(KanbanComment kanbanComment)
        {
            kanbanComment.IsActive = false;
            _context.KanbanComments.Update(kanbanComment);
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

        public void EditKanbanComment(KanbanComment kanbanComment)
        {
            _context.KanbanComments.Update(kanbanComment);
        }

        public void EditKanbanTask(KanbanTask kanbanTask)
        {
            _context.KanbanTasks.Update(kanbanTask);
        }

        public void EditSubtask(Subtask subtask)
        {
            _context.Subtasks.Update(subtask);
        }

        public async Task<List<KanbanTask>> GetKanabanTasks(KanbanTaskQuery queryModel)
        {
            var query = _context.KanbanTasks
                .Where(k => k.IsActive && k.Status != KanbanTaskStatus.Done);

            if(queryModel.DateExactly.HasValue)
                query = query.Where(x => 
                    x.DateOfCarDelivery.Date == queryModel.DateExactly.Value.Date);

            if(queryModel.DateFrom.HasValue)
                query = query.Where(x => 
                    x.DateOfCarDelivery.Date >= queryModel.DateFrom.Value.Date);

            if(queryModel.DateTo.HasValue)
                query = query.Where(x => 
                    x.DateOfCarDelivery.Date <= queryModel.DateTo.Value.Date);

            var kanbanTasks = await query.ToListAsync();

            return kanbanTasks;
        }

        public async Task<KanbanComment> GetKanbanComment(int id)
        {
            var kanbanComment = await _context.KanbanComments
                .FirstOrDefaultAsync(k => k.Id == id);

            return kanbanComment;
        }

        public async Task<List<KanbanComment>> GetKanbanComments(int kanbanTaskId, bool? isInnerComment = null)
        {
            var kanbanComments = await _context.KanbanComments
                .Where(k => 
                    k.KanbanTaskId == kanbanTaskId && 
                    k.IsActive == true && 
                    (isInnerComment.HasValue ?
                        k.IsInnerComment == isInnerComment.Value : true))
                .Include(k => k.User)
                .ToListAsync();

            return kanbanComments;
        }

        public async Task<KanbanTask> GetKanbanTask(int? id, bool? isInnerComment = null, ProtocolQuery protocolQuery = null)
        {
            var query = _context.KanbanTasks
                .Where(k => k.IsActive == true &&
                    k.Status != KanbanTaskStatus.Done)
                .Include(k => k.ServiceRequest)
                    .ThenInclude(sr => sr.Customer)
                .Include(k => k.Subtasks
                    .Where(s => s.IsActive == true))
                    .ThenInclude(st => st.CalendarEntries
                        .Where(c => c.IsActive == true))
                .Include(k => k.BasketItems
                    .Where(bi => bi.IsActive == true))
                .Include(k => k.Comments
                        .Where(c => c.IsActive == true && 
                            (isInnerComment.HasValue ?
                                c.IsInnerComment == isInnerComment.Value : true)))
                        .ThenInclude(c => c.User)
                .Include(k => k.Customer)
                .Include(k => k.User)
                .Include(k => k.Photos)
                .OrderByDescending(k => k.DateOfCreation);

                if(id.HasValue)
                    return await query.FirstOrDefaultAsync(k => k.Id == id.Value);
                else
                    return await query.FirstOrDefaultAsync(k => 
                        k.VIN.ToUpper() == protocolQuery.VIN.ToUpper() &&
                        k.ProtocolNumber.ToUpper() == protocolQuery.ProtocolNumber.ToUpper());
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
                .Where(s => s.KanbanTaskId == kanbanTaskId && s.IsActive)
                .ToListAsync();

            return subtasks;
        }

        public void EditBasketItem(BasketItem basketItem)
        {
            _context.BasketItems.Update(basketItem);
        }

        public void DeleteBasketItem(BasketItem basketItem)
        {
            basketItem.IsActive = false;
            _context.BasketItems.Update(basketItem);
        }

        public async Task<List<BasketItem>> GetBasketItemsByState(List<BasketItemState> states)
        {
            var basketItems = await _context.BasketItems
                .Where(bi => 
                    states.Contains(bi.BasketItemState) &&
                    bi.IsActive == true)
                .Include(bi => bi.KanbanTask)
                .ToListAsync();

            return basketItems;
        }

        public async Task<BasketItem> GetBasketItem(int basketItemId)
        {
            var basketItem = await _context.BasketItems
                .FirstOrDefaultAsync(bi => bi.Id == basketItemId);

            return basketItem;
        }

        public void AddBasketItem(BasketItem basketItem)
        {
            _context.BasketItems.Add(basketItem);
        }

        public async Task<List<KanbanTask>> GetCarHistory(string vin)
        {
            var allowedKanbanStatuses = new KanbanTaskStatus[] 
                {KanbanTaskStatus.Done, KanbanTaskStatus.WaitingForCustomer};

            var kanbanTasks = await _context.KanbanTasks
                .Where(k => k.VIN == vin && 
                    k.IsActive == true &&
                    allowedKanbanStatuses.Contains(k.Status))
                .Include(k => k.Subtasks
                    .Where(s => s.IsActive == true && 
                        s.Status == SubtaskStatus.Done))
                    .ThenInclude(s => s.CalendarEntries
                        .Where(c => c.IsActive))
                .Include(k => k.BasketItems
                    .Where(b => b.IsActive == true))
                .ToListAsync(); 

            return kanbanTasks;
        }

        public void AddPhoto(Photo photo)
        {
            _context.Photos.Add(photo);
        }

        public async Task<Photo> GetPhoto(int id)
        {
            var photo = await _context.Photos
                .FirstOrDefaultAsync(p => p.Id == id);

            return photo;
        }

        public void RemovePhoto(Photo photo)
        {
            _context.Photos.Remove(photo);
        }
    }
}