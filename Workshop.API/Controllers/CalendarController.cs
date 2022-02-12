using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Workshop.API.DTOs;
using Workshop.API.Entities;
using Workshop.API.Interfaces;
using Workshop.API.Models;

namespace Workshop.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CalendarController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CalendarController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("entries")]
        public async Task<ActionResult> GetCalendarEntries([FromQuery]CalendarEntryQuery query)
        {
            if(!query.Validate())
                return BadRequest();

            var entries = await _unitOfWork.CalendarRepository.GetCalendarEntries(query);
            var entriesToReturn = _mapper.Map<CalendarEntryDto[]>(entries);

            return Ok(entriesToReturn);
        }

        [HttpPost]
        [Route("entry")]
        public async Task<ActionResult> AddCalendarEntry([FromBody]CalendarEntryAddDto entry)
        {
            var validationResult = entry.Validate();

            if(!validationResult.IsSuccess)
                return BadRequest(validationResult.Errors);

            var entryToAdd = _mapper.Map<CalendarEntry>(entry);

            entryToAdd.IsPlanned = DateTime.Now.Date < entryToAdd.Date;

            _unitOfWork.CalendarRepository.AddCalendarEntry(entryToAdd);

            var result = await _unitOfWork.SaveAsync();

            if(result)
                return StatusCode(204);

            return StatusCode(500);
        }

        [HttpGet]
        [Route("availableUsers")]
        public async Task<ActionResult> GetAvailableUsers([FromQuery]DateTime date)
        {
            var users = await _unitOfWork.CalendarRepository.GetAvailableUsers(date);
            var employees = _mapper.Map<List<CalendarEntryUserDto>>(users);

            foreach (var employee in employees)
            {
                employee.AvailableHours = 8;
                var busyHours = users.First(u => u.Id == employee.Id)
                    .CalendarEntries.Sum(c => c.Hours);
                employee.AvailableHours -= busyHours;
            }

            return Ok(employees);
        }
    }
}