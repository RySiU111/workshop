using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Workshop.API.DTOs;
using Workshop.API.Entities;
using Workshop.API.Interfaces;

namespace Workshop.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;

        public EmployeeController(IMapper mapper, IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("employees")]
        public async Task<ActionResult> GetEmployees()
        {
            var users = await _userManager.Users
                .Where(u => u.IsActive)
                .ToListAsync();
                
            var employees = _mapper.Map<EmployeeDto[]>(users);

            return Ok(employees);
        }
        
        [HttpGet]
        [Route("employee")]
        public async Task<ActionResult> GetEmployee([FromQuery]int id)
        {
            if(id <= 0)
                return BadRequest();

            var user = await _userManager.Users
                .Include(u => u.KanbanTasks
                    .Where(x => x.IsActive == true))
                .Include(u => u.Subtasks
                    .Where(x => x.IsActive == true))
                .Include(u => u.UserKanbanComments
                    .Where(x => x.IsActive == true))
                .Include(u => u.CalendarEntries
                    .Where(x => x.IsActive == true))
                .FirstOrDefaultAsync(u => u.Id == id);

            user.Subtasks.ForEach(s => s.User = null);

            var employee = _mapper.Map<EmployeeDetailsDto>(user);

            return Ok(employee);
        }
        
        [HttpPut]
        [Route("employee")]
        public async Task<ActionResult> EditEmployee([FromBody]EmployeeEditDto employee)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == employee.Id);

            if(user == null)
                return NotFound(employee);

            user.JobTitle = employee.JobTitle;
            user.Name = employee.Name;
            user.Surname = employee.Surname;
            user.UserName = employee.UserName;
            user.HourlyWage = employee.HourlyWage;
            user.DateOfEmployment = employee.DateOfEmployment;
            user.DateOfTerminationOfEmployment = employee.DateOfTerminationOfEmployment;

            var result = await _userManager.UpdateAsync(user);

            if(result.Succeeded)
                return Ok();

            return StatusCode(500, result.Errors);
        }
    }
}