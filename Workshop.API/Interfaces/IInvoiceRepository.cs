using System.Collections.Generic;
using System.Threading.Tasks;
using Workshop.API.Entities;
using Workshop.API.Models;

namespace Workshop.API.Interfaces
{
    public interface IInvoiceRepository
    {
        void AddInvoice(Invoice invoice);
        void RemoveInvoice(Invoice invoice);
        Task<List<Invoice>> GetInvoices(InvoiceQuery queryModel);
        Task<Invoice> GetInvoice(int id);
        Task<int> GetInvoiceNextNumber(int kanbanTaskId);
    }
}