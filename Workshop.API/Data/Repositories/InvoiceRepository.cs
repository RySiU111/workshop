using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Workshop.API.Entities;
using Workshop.API.Interfaces;
using Workshop.API.Models;

namespace Workshop.API.Data.Repositories
{
    public class InvoiceRepository: IInvoiceRepository
    {
        private readonly WorkshopContext _context;

        public InvoiceRepository(WorkshopContext context)
        {
            _context = context;
        }

        public void AddInvoice(Invoice invoice)
        {
            _context.Invoices.Add(invoice);
        }

        public void RemoveInvoice(Invoice invoice)
        {
            invoice.IsActive = false;
            _context.Invoices.Update(invoice);
        }

        public async Task<List<Invoice>> GetInvoices(InvoiceQuery queryModel)
        {
            var query = _context.Invoices
                .Where(i => i.IsActive);
                
            if(queryModel.InvoiceId.HasValue)
                query = query.Where(c => c.Id == queryModel.InvoiceId);

            if(!string.IsNullOrEmpty(queryModel.InvoiceCode))
                query = query.Where(c => c.InvoiceCode.Contains(queryModel.InvoiceCode));

            if(queryModel.KanbanTaskId.HasValue)
                query = query.Where(c => c.KanbanTaskId == queryModel.KanbanTaskId);

            if(queryModel.UserId.HasValue)
                query = query.Where(c => c.UserId == queryModel.UserId);

            if(queryModel.DateFrom.HasValue)
                query = query.Where(c => c.DateOfCeation.Date >= queryModel.DateFrom.Value.Date);

            if(queryModel.DateTo.HasValue)
                query = query.Where(c => c.DateOfCeation.Date <= queryModel.DateTo.Value.Date);
            
            query = query
                .Include(c => c.KanbanTask)
                .Include(c => c.User)
                .Include(c => c.Customer)
                .OrderByDescending(c => c.Number);

            return await query.ToListAsync();
        }

        public async Task<Invoice> GetInvoice(int id)
        {
            var invoice = await _context.Invoices
                .Include(i => i.Customer)
                .Include(i => i.User)
                .Include(i => i.KanbanTask)
                    .ThenInclude(k => k.Subtasks)
                .Include(i => i.KanbanTask)
                    .ThenInclude(k => k.BasketItems)
                .FirstOrDefaultAsync(i => i.Id == id);

            return invoice;
        }

        public async Task<int> GetInvoiceNextNumber(int kanbanTaskId)
        {
            var invoice = await _context.Invoices
                .Where(i => i.KanbanTaskId == kanbanTaskId)
                .OrderByDescending(i => i.DateOfCeation)
                .FirstOrDefaultAsync();
            
            if(invoice == null)
                return 1;

            return invoice.Number + 1;
        }
    }
}