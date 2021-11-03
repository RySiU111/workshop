using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Workshop.API.Entities;
using Workshop.API.Interfaces;

namespace Workshop.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientAppController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ClientAppController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("config")]
        public async Task<ActionResult<ComponentConfig>> GetComponentConfigs([FromQuery]string componentName)
        {
            if(string.IsNullOrEmpty(componentName))
                return BadRequest();

            var componentConfig = await _unitOfWork.ClientAppRepository.GetComponentConfig(componentName);

            return Ok(componentConfig);
        }

        [HttpPost]
        [Route("config")]
        public async Task<ActionResult<ComponentConfig>> AddComponentConfig([FromBody]ComponentConfig componentConfig)
        {
            if(componentConfig == null)
                return BadRequest();

            //TODO: Validation
            _unitOfWork.ClientAppRepository.AddComponentConfig(componentConfig);

            var result = await _unitOfWork.SaveAsync();

            if(result)
                return Ok(componentConfig);

            return StatusCode(500);
        }

        [HttpDelete]
        [Route("config")]
        public async Task<ActionResult<ComponentConfig>> RemoveComponentConfig([FromBody]ComponentConfig componentConfig)
        {
            if(componentConfig == null)
                return BadRequest();

            _unitOfWork.ClientAppRepository.RemoveComponentConfig(componentConfig);

            var result = await _unitOfWork.SaveAsync();

            if(result)
                return Ok(componentConfig);

            return StatusCode(500);
        }

        [HttpPut]
        [Route("config")]
        public async Task<ActionResult<ComponentConfig>> EditComponentConfig([FromBody]ComponentConfig componentConfig)
        {
            if(componentConfig == null)
                return BadRequest();

            //TODO: Validation
            _unitOfWork.ClientAppRepository.EditComponentConfig(componentConfig);

            var result = await _unitOfWork.SaveAsync();

            if(result)
                return Ok(componentConfig);

            return StatusCode(500);
        }
    }
}