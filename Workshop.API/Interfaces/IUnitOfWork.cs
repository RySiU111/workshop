using System.Threading.Tasks;
using Workshop.API.Data.Repositories;

namespace Workshop.API.Interfaces
{
    public interface IUnitOfWork
    {
        ClientAppRepository ClientAppRepository { get; }
        CarServiceRepository CarServiceRepository { get; }
        KanbanRepository KanbanRepository { get; }
        CalendarRepository CalendarRepository { get; }
        InvoiceRepository InvoiceRepository { get; }
        ReportsRepository ReportsRepository { get; }
        Task<bool> SaveAsync();
    }
}