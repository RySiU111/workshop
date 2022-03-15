using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Workshop.API.DTOs;
using Workshop.API.Entities;
using Workshop.API.Interfaces;
using Workshop.API.Models;

namespace Workshop.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class KanbanController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoSevice;

        public KanbanController(IUnitOfWork unitOfWork, IMapper mapper, IPhotoService photoSevice)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _photoSevice = photoSevice;
        }

        //[Authorize]
        [HttpGet]
        [Route("kanbanTasks")]
        public async Task<ActionResult<IEnumerable<KanbanTaskDto>>> GetKanbanTasks([FromQuery]KanbanTaskQuery query)
        {
            if(!query.Validate())
                return BadRequest();

            if(string.IsNullOrEmpty(query.VIN))
            {
                var kanbanTasks = await _unitOfWork.KanbanRepository.GetKanabanTasks(query);
                var result = _mapper.Map<KanbanTaskDto[]>(kanbanTasks);
                return Ok(result);
            }
            else
            {
                var kanbanTasks = await _unitOfWork.KanbanRepository.GetCarHistory(query.VIN);
                var result = _mapper.Map<List<KanbanTaskHistoryDto>>(kanbanTasks);
                result.ForEach(r => { r.TotalWorkHoursCosts *= 100; r.PlannedWorkHoursCosts *= 100;});
                return Ok(result);
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("kanbanTask")]
        public async Task<ActionResult<KanbanTaskDetailsDto>> GetPublicKanbanTask(
            [FromQuery]int? id, [FromQuery]ProtocolQuery protocolQuery)
        {
            if((!id.HasValue && !protocolQuery.Validate()) || 
                (id.HasValue && id <= 0) || 
                (!id.HasValue && !protocolQuery.Validate()))
                return BadRequest();

            bool? isInnerComment = null;

            if(!User.Identity.IsAuthenticated)
                isInnerComment = false;

            var kanbanTask = await _unitOfWork.KanbanRepository.GetKanbanTask(id, isInnerComment, protocolQuery);

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
                return Ok(kanbanTaskToSave.Id);

            return StatusCode(500);
        }

        [HttpPost]
        [Route("kanbanTask/{kanbanTaskId}/photo")]
        public async Task<ActionResult> AddPhotoToKanbanTask(int kanbanTaskId, IFormFile file)
        {
            if(kanbanTaskId <= 0)
                return BadRequest();

            var photoResult = await _photoSevice.AddPhotoAsync(file);

            if(photoResult.Error != null)
                return BadRequest(photoResult.Error.Message);

            var photo = new Photo
            {
                KanbanTaskId = kanbanTaskId,
                Url = photoResult.SecureUrl.AbsoluteUri,
                PublicId = photoResult.PublicId,
                Name = file.FileName,
            };

            _unitOfWork.KanbanRepository.AddPhoto(photo);

            var result = await _unitOfWork.SaveAsync();

            if(result)
                return Ok();

            return BadRequest();
        }

        [HttpDelete]
        [Route("kanbanTask/photo")]
        public async Task<ActionResult> DeletePhoto([FromQuery]int photoId)
        {
            if(photoId <= 0)
                return BadRequest();

            var photo = await _unitOfWork.KanbanRepository.GetPhoto(photoId);

            if(photo == null)
                return NotFound();

            var photoResult = await _photoSevice.DeletePhotoAsync(photo.PublicId);

            if(photoResult.Error != null)
                return BadRequest(photoResult.Error.Message);

            _unitOfWork.KanbanRepository.RemovePhoto(photo);

            var result = await _unitOfWork.SaveAsync();

            if(result)
                return Ok();

            return BadRequest();
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

        [HttpPut]
        [Route("kanbanTask/protocol")]
        public async Task<ActionResult> GenerateProtocolNumber([FromQuery]int kanbanTaskId)
        {
            if(kanbanTaskId <= 0)
                return BadRequest();

            var kanbanTask = await _unitOfWork.KanbanRepository.GetKanbanTask(kanbanTaskId);
            
            if(!string.IsNullOrEmpty(kanbanTask.ProtocolNumber))
                return BadRequest("Protokół już został wygenerowany.");
            
            var protocolNumber = Guid.NewGuid().ToString().Substring(0, 8);
            kanbanTask.ProtocolNumber = protocolNumber;

            _unitOfWork.KanbanRepository.EditKanbanTask(kanbanTask);

            var result = await _unitOfWork.SaveAsync();

            if(result)
                return Ok(protocolNumber);

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
        public async Task<ActionResult<IEnumerable<KanbanCommentDto>>> GetKanabanComments(
            [FromQuery]int kanbanTaskId, 
            [FromQuery]bool? isInnerComment)
        {
            if(kanbanTaskId <= 0)
                return BadRequest();

            var kanabanComments = await _unitOfWork.KanbanRepository.GetKanbanComments(kanbanTaskId, isInnerComment);

            var result = _mapper.Map<KanbanCommentDto[]>(kanabanComments);

            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("comments")]
        public async Task<ActionResult> AddKanabanComment([FromBody]KanbanCommentDto kanbanComment)
        {
            if(kanbanComment == null)
                return BadRequest();

            if(!User.Identity.IsAuthenticated)
                kanbanComment.IsInnerComment = false;

            var kanbanCommentToSave = _mapper.Map<KanbanComment>(kanbanComment);

            _unitOfWork.KanbanRepository.AddKanbanComment(kanbanCommentToSave);

            var result = await _unitOfWork.SaveAsync();

            if(result)
                return StatusCode(201);

            return StatusCode(500);
        }

        [AllowAnonymous]
        [HttpPut]
        [Route("comments")]
        public async Task<ActionResult> EditKanabanComment([FromBody]KanbanCommentDto kanbanComment)
        {
            if(kanbanComment == null)
                return BadRequest();

            if(!User.Identity.IsAuthenticated)
                kanbanComment.IsInnerComment = false;

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

        [HttpPost]
        [Route("basketItem")]
        public async Task<ActionResult> AddBasketItem([FromBody]BasketItemDto basketItem)
        {
            if(basketItem == null)
                return BadRequest();

            var basketItemToEdit = _mapper.Map<BasketItem>(basketItem);

            _unitOfWork.KanbanRepository.AddBasketItem(basketItemToEdit);

            var result = await _unitOfWork.SaveAsync();

            if(result)
                return StatusCode(201);

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

            var basketItemsToReturn = _mapper.Map<BasketItemKanbanTaskDto[]>(basketItems);

            return Ok(basketItemsToReturn);
        }
    }
}