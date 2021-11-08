using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Workshop.API.DTOs;
using Workshop.API.Entities;
using Workshop.API.Interfaces;

namespace Workshop.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KanbanController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public KanbanController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("kanbanTasks")]
        public async Task<ActionResult<IEnumerable<KanbanTaskDto>>> GetKanbanTasks()
        {
            var kanbanTasks = await _unitOfWork.KanbanRepository.GetKanabanTasks();

            var result = _mapper.Map<KanbanTaskDto[]>(kanbanTasks);

            return Ok(result);
        }

        [HttpGet]
        [Route("kanbanTask")]
        public async Task<ActionResult<KanbanTaskDetailsDto>> GetKanbanTask([FromQuery]int id)
        {
            if(id <= 0)
                return BadRequest();

            var kanbanTask = await _unitOfWork.KanbanRepository.GetKanbanTask(id);

            var result = _mapper.Map<KanbanTaskDetailsDto>(kanbanTask);

            return Ok(result);
        }

        [HttpPost]
        [Route("kanbanTask")]
        public async Task<ActionResult> CreateKanbanTask([FromBody]KanbanTask kanbanTask)
        {
            if(kanbanTask == null)
                return BadRequest();

            if(kanbanTask.ServiceRequestId > 0)
                _unitOfWork.CarServiceRepository.AcceptServiceRequest(kanbanTask.ServiceRequestId);

            _unitOfWork.KanbanRepository.AddKanbanTask(kanbanTask);

            var result = await _unitOfWork.SaveAsync();

            if(result)
                return StatusCode(201);

            return StatusCode(500);
        }

        [HttpPut]
        [Route("kanbanTask")]
        public async Task<ActionResult> EditKanbanTask([FromBody]KanbanTask kanbanTask)
        {
            if(kanbanTask == null)
                return BadRequest();

            _unitOfWork.KanbanRepository.EditKanbanTask(kanbanTask);

            var result = await _unitOfWork.SaveAsync();

            if(result)
                return StatusCode(204);

            return StatusCode(500);
        }

        [HttpDelete]
        [Route("kanbanTask")]
        public async Task<ActionResult> DeleteKanbanTask([FromQuery]int id)
        {
            if(id <= 0)
                return BadRequest();

            var kanbanTaskToDelete = await _unitOfWork.KanbanRepository.GetKanbanTask(id);

            if(kanbanTaskToDelete == null)
                return StatusCode(404);

            if(kanbanTaskToDelete.ServiceRequestId > 0)
            {
                var serviceRequest = await _unitOfWork.CarServiceRepository
                    .GetServiceRequest(kanbanTaskToDelete.ServiceRequestId);

                if(serviceRequest != null)
                {
                    serviceRequest.State = ServiceRequestState.ClientRejection;
                    _unitOfWork.CarServiceRepository.EditServiceRequest(serviceRequest);
                }
            }

            _unitOfWork.KanbanRepository.DeleteKanbanTask(kanbanTaskToDelete);

            var result = await _unitOfWork.SaveAsync();

            if(result)
                return StatusCode(204);

            return StatusCode(500);
        }
    }
}