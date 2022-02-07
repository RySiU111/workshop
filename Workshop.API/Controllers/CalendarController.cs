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
            _unitOfWork.CalendarRepository.AddCalendarEntry(entryToAdd);

            var result = await _unitOfWork.SaveAsync();

            if(result)
                return StatusCode(204);

            return StatusCode(500);
        }
    }
}