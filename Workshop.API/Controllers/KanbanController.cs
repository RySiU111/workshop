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
        public async Task<ActionResult> CreateKanbanTask([FromBody]KanbanTaskDetailsDto kanbanTask)
        {
            if(kanbanTask == null)
                return BadRequest();

            if(kanbanTask.ServiceRequestId.HasValue)
                _unitOfWork.CarServiceRepository.AcceptServiceRequest(kanbanTask.ServiceRequestId.Value);

            var kanbanTaskToSave = _mapper.Map<KanbanTask>(kanbanTask);

            _unitOfWork.KanbanRepository.AddKanbanTask(kanbanTaskToSave);

            var result = await _unitOfWork.SaveAsync();

            if(result)
                return StatusCode(201);

            return StatusCode(500);
        }

        [HttpPut]
        [Route("kanbanTask")]
        public async Task<ActionResult> EditKanbanTask([FromBody]KanbanTaskDetailsDto kanbanTask)
        {
            if(kanbanTask == null)
                return BadRequest();

            var kanbanTaskToEdit = _mapper.Map<KanbanTask>(kanbanTask);

            _unitOfWork.KanbanRepository.EditKanbanTask(kanbanTaskToEdit);

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

            if(kanbanTaskToDelete.ServiceRequestId.HasValue)
            {
                var serviceRequest = await _unitOfWork.CarServiceRepository
                    .GetServiceRequest(kanbanTaskToDelete.ServiceRequestId.Value);

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

        [HttpGet]
        [Route("subtasks")]
        public async Task<ActionResult<IEnumerable<SubtaskDto>>> GetSubtasks([FromQuery]int kanbanTaskId)
        {
            if(kanbanTaskId <= 0)
                return BadRequest();

            var subtasks = await _unitOfWork.KanbanRepository.GetSubtasks(kanbanTaskId);

            var result = _mapper.Map<SubtaskDto[]>(subtasks);

            return Ok(result);
        }

        [HttpPut]
        [Route("subtask")]
        public async Task<ActionResult> EditSubtask([FromBody]SubtaskDto subtask)
        {
            if(subtask == null)
                return BadRequest();

            var subtaskToEdit = _mapper.Map<Subtask>(subtask);

            _unitOfWork.KanbanRepository.EditSubtask(subtaskToEdit);

            var result = await _unitOfWork.SaveAsync();

            if(result)
                return StatusCode(204);

            return StatusCode(500);
        }

        [HttpPost]
        [Route("subtask")]
        public async Task<ActionResult> GetSubtasks([FromBody]SubtaskDto subtask)
        {
            if(subtask == null)
                return BadRequest();

            var subtaskToSave = _mapper.Map<Subtask>(subtask);

            _unitOfWork.KanbanRepository.AddSubtask(subtaskToSave);

            var result = await _unitOfWork.SaveAsync();

            if(result)
                return Ok(subtaskToSave.Id);

            return StatusCode(500);
        }

        [HttpDelete]
        [Route("subtask")]
        public async Task<ActionResult> DeleteSubtask([FromQuery]int subtaskId)
        {
            if(subtaskId <= 0)
                return BadRequest();

            var subtaskToDelete = await _unitOfWork.KanbanRepository.GetSubtask(subtaskId);

            if(subtaskToDelete == null)
                return NotFound();

            _unitOfWork.KanbanRepository.DeleteSubtask(subtaskToDelete);
            
            var result = await _unitOfWork.SaveAsync();

            if(result)
                return StatusCode(204);

            return StatusCode(500);
        }

        [HttpGet]
        [Route("comments")]
        public async Task<ActionResult<IEnumerable<KanbanCommentDto>>> GetKanabanComments([FromQuery]int kanbanTaskId)
        {
            if(kanbanTaskId <= 0)
                return BadRequest();

            var kanabanComments = await _unitOfWork.KanbanRepository.GetKanbanComments(kanbanTaskId);

            var result = _mapper.Map<KanbanCommentDto[]>(kanabanComments);

            return Ok(result);
        }

        [HttpPost]
        [Route("comments")]
        public async Task<ActionResult> AddKanabanComment([FromBody]KanbanCommentDto kanbanComment)
        {
            if(kanbanComment == null)
                return BadRequest();

            var kanbanCommentToSave = _mapper.Map<KanbanComment>(kanbanComment);

            _unitOfWork.KanbanRepository.AddKanbanComment(kanbanCommentToSave);

            var result = await _unitOfWork.SaveAsync();

            if(result)
                return StatusCode(201);

            return StatusCode(500);
        }

        [HttpPut]
        [Route("comments")]
        public async Task<ActionResult> EditKanabanComment([FromBody]KanbanCommentDto kanbanComment)
        {
            if(kanbanComment == null)
                return BadRequest();

            var kanbanCommentToEdit = _mapper.Map<KanbanComment>(kanbanComment);

            _unitOfWork.KanbanRepository.EditKanbanComment(kanbanCommentToEdit);

            var result = await _unitOfWork.SaveAsync();

            if(result)
                return StatusCode(204);

            return StatusCode(500);
        }

        [HttpDelete]
        [Route("comments")]
        public async Task<ActionResult> DeleteKanabanComment([FromQuery]int kanbanCommentId)
        {
            if(kanbanCommentId <= 0)
                return BadRequest();

            var kanbanCommentToDelete = await _unitOfWork.KanbanRepository.GetKanbanComment(kanbanCommentId);

            if(kanbanCommentToDelete == null)
                return NotFound();

            _unitOfWork.KanbanRepository.DeleteKanbanComment(kanbanCommentToDelete);
            
            var result = await _unitOfWork.SaveAsync();

            if(result)
                return StatusCode(204);

            return StatusCode(500);
        }

        [HttpPut]
        [Route("basketItem")]
        public async Task<ActionResult> EditBasketItem([FromBody]BasketItemDto basketItem)
        {
            if(basketItem == null)
                return BadRequest();

            var basketItemToEdit = _mapper.Map<BasketItem>(basketItem);

            _unitOfWork.KanbanRepository.EditBasketItem(basketItemToEdit);

            var result = await _unitOfWork.SaveAsync();

            if(result)
                return StatusCode(204);

            return StatusCode(500);
        }

        [HttpDelete]
        [Route("basketItem")]
        public async Task<ActionResult> DeleteBasketItem([FromQuery]int basketItemId)
        {
            if(basketItemId <= 0)
                return BadRequest();

            var basketItemToDelete = await _unitOfWork.KanbanRepository.GetBasketItem(basketItemId);

            if(basketItemToDelete == null)
                return NotFound();

            _unitOfWork.KanbanRepository.DeleteBasketItem(basketItemToDelete);

            var result = await _unitOfWork.SaveAsync();

            if(result)
                return StatusCode(204);

            return StatusCode(500);
        }

        [HttpGet]
        [Route("basketItems/uncompleted")]
        public async Task<ActionResult<IEnumerable<BasketItem>>> GetUncompletedBasketItems()
        {
            var basketItems = await _unitOfWork.KanbanRepository.GetBasketItemsByState(
                new List<BasketItemState>() { BasketItemState.New, BasketItemState.InRealisation });

            var basketItemsToReturn = _mapper.Map<BasketItemDto[]>(basketItems);

            return Ok(basketItemsToReturn);
        }
    }
}