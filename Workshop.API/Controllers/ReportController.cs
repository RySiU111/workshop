using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Workshop.API.Interfaces;
using Workshop.API.Models;

namespace Workshop.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReportController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("kanbanTasks")]
        public async Task<ActionResult> GetKanbanTasksReport([FromQuery]ReportQuery query)
        {
            if(!query.Validate())
                return BadRequest();

            var result = await _unitOfWork.ReportsRepository.GetKanbanTasksReport(query);

            return Ok(result);
        }

        [HttpGet]
        [Route("employees")]
        public async Task<ActionResult> GetEmployeesReport([FromQuery]ReportQuery query)
        {
            if(!query.Validate())
                return BadRequest();

            var result = await _unitOfWork.ReportsRepository.GetEmployeesReport(query);

            return Ok(result);
        }

        [HttpGet]
        [Route("kanbanTaskYear")]
        public async Task<ActionResult> GetKanbanTaskYearReport([FromQuery]int year)
        {
            if(year <= 0 || year > DateTime.Now.Year)
                return BadRequest();

            var result = await _unitOfWork.ReportsRepository.GetKanbanTaskYearReport(year);

            return Ok(result);
        }
    }
}