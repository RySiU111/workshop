using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Workshop.API.Entities;
using Workshop.API.Interfaces;

namespace Workshop.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarServiceController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CarServiceController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("serviceRequests")]
        public async Task<ActionResult<IEnumerable<ServiceRequest>>> GetServiceRequests()
        {
            var serviceRequests = await _unitOfWork.CarServiceRepository.GetServiceRequests();

            return Ok(serviceRequests);
        }

        [HttpGet]
        [Route("serviceRequest")]
        public async Task<ActionResult<ServiceRequest>> GetServiceRequest([FromQuery]int id)
        {
            if(id == 0)
                return BadRequest();

            var serviceRequest = await _unitOfWork.CarServiceRepository.GetServiceRequest(id);

            return Ok(serviceRequest);
        }

        [HttpPost]
        [Route("serviceRequest")]
        public async Task<ActionResult<ServiceRequest>> AddServiceRequest([FromBody]ServiceRequest serviceRequest)
        {
            if(serviceRequest == null)
                return BadRequest();

            var customer = await _unitOfWork.CarServiceRepository.FindCustomer(serviceRequest.Custormer);

            if(customer != null)
            {
                serviceRequest.CustomerId = customer.Id;
                serviceRequest.Custormer = null;
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
    }
}