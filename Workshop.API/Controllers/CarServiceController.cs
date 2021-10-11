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
    public class CarServiceController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CarServiceController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("serviceRequests")]
        public async Task<ActionResult<IEnumerable<ServiceRequest>>> GetServiceRequests()
        {
            var serviceRequests = await _unitOfWork.CarServiceRepository.GetServiceRequests();

            var response = _mapper.Map<ServiceRequestDto[]>(serviceRequests);

            return Ok(response);
        }

        [HttpGet]
        [Route("serviceRequest")]
        public async Task<ActionResult<ServiceRequest>> GetServiceRequest([FromQuery]int id)
        {
            if(id == 0)
                return BadRequest();

            var serviceRequest = await _unitOfWork.CarServiceRepository.GetServiceRequest(id);

            var response = _mapper.Map<ServiceRequestDto>(serviceRequest);

            return Ok(response);
        }

        [HttpPost]
        [Route("serviceRequest")]
        public async Task<ActionResult<ServiceRequest>> AddServiceRequest([FromBody]ServiceRequest serviceRequest)
        {
            if(serviceRequest == null)
                return BadRequest();

            var customer = await _unitOfWork.CarServiceRepository.FindCustomer(serviceRequest.Customer);

            if(customer != null)
            {
                serviceRequest.CustomerId = customer.Id;
                serviceRequest.Customer = null;
            }

            _unitOfWork.CarServiceRepository.AddServiceRequest(serviceRequest);

            var result = await _unitOfWork.SaveAsync();

            if(result)
                return Ok();

            return BadRequest();
        }

        [HttpPut]
        [Route("serviceRequest")]
        public async Task<ActionResult<ServiceRequest>> EditServiceRequest([FromBody]ServiceRequest serviceRequest)
        {
            if(serviceRequest == null)
                return BadRequest();

            _unitOfWork.CarServiceRepository.EditServiceRequest(serviceRequest);

            var result = await _unitOfWork.SaveAsync();

            if(result)
                return Ok();

            return BadRequest();
        }

        [HttpDelete]
        [Route("serviceRequest")]
        public async Task<IActionResult> DeleteServiceRequest([FromQuery]int serviceRequestId)
        {
            if(serviceRequestId <= 0)
                return BadRequest();

            var serviceRequest = await _unitOfWork.CarServiceRepository.GetServiceRequest(serviceRequestId);

            if(serviceRequest == null)
                return BadRequest();

            _unitOfWork.CarServiceRepository.DeleteServiceRequest(serviceRequest);
            
            var result = await _unitOfWork.SaveAsync();

            if(result)
                return Ok();

            return StatusCode(500);
        }
    }
}