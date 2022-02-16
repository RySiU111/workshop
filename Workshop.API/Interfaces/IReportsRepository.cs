using System.Collections.Generic;
using System.Threading.Tasks;
using Workshop.API.Models;

namespace Workshop.API.Interfaces
{
    public interface IReportsRepository
    {
        Task<List<KanbanTasksReportResult>> GetKanbanTasksReport(ReportQuery queryModel);
    }
}