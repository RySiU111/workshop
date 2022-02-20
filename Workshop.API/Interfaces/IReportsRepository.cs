using System.Collections.Generic;
using System.Threading.Tasks;
using Workshop.API.Models;

namespace Workshop.API.Interfaces
{
    public interface IReportsRepository
    {
        Task<List<KanbanTasksReportResult>> GetKanbanTasksReport(ReportQuery queryModel);
        Task<List<EmployeesReportResult>> GetEmployeesReport(ReportQuery queryModel);
        Task<List<KanbanTaskYearResult>> GetKanbanTaskYearReport(int year);
    }
}