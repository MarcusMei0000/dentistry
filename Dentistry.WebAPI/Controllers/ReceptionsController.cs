using AutoMapper;
using Dentistry.Services.Abstract;
using Dentistry.Services.Models;
using Dentistry.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Dentistry.WebAPI.Controllers
{
    /// <summary>
    /// Doctors endpoints
    /// </summary>
    [ProducesResponseType(200)]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ReceptionsController : ControllerBase
    {
        private readonly IReceptionService receptionService;
        private readonly IMapper mapper;

        /// <summary>
        /// Doctors controller
        /// </summary>
        public ReceptionsController(IReceptionService receptionService, IMapper mapper)
        {
            this.receptionService = receptionService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Get receptions by pages
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetReceptions([FromQuery] int limit = 20, [FromQuery] int offset = 0)
        {
            var pageModel = receptionService.GetReceptions(limit, offset);

            return Ok(mapper.Map<PageResponse<ReceptionResponse>>(pageModel));
        }

        /// <summary>
        /// Update reception
        /// </summary>
        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateReception([FromRoute] Guid id, [FromBody] UpdateReceptionRequest model)
        {
            var validationResult = model.Validate();
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            try
            {
                var resultModel = receptionService.UpdateReception(id, mapper.Map<UpdateReceptionModel>(model));

                return Ok(mapper.Map<ReceptionResponse>(resultModel));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        /// <summary>
        /// Delete reception
        /// </summary>
        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteReception([FromRoute] Guid id)
        {
            try
            {
                receptionService.DeleteReception(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        /// <summary>
        /// Get reception
        /// </summary>
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetReception([FromRoute] Guid id)
        {
            try
            {
                var receptionModel = receptionService.GetReception(id);
                return Ok(mapper.Map<ReceptionResponse>(receptionModel));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPost]
        public IActionResult CreateReception([FromBody] CreateReceptionRequest createReceptionRequest, [FromQuery] Guid ScheduleId, [FromQuery] Guid PatientId)
        {
            var validationResult = createReceptionRequest.Validate();
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            try
            {
                var resultModel = receptionService.CreateReception(mapper.Map<CreateReceptionModel>(createReceptionRequest), ScheduleId, PatientId);
                return Ok(resultModel);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
