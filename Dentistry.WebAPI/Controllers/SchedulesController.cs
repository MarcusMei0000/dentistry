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
    public class SchedulesController : ControllerBase
    {
        private readonly IScheduleService scheduleService;
        private readonly IMapper mapper;

        /// <summary>
        /// Doctors controller
        /// </summary>
        public SchedulesController(IScheduleService scheduleService, IMapper mapper)
        {
            this.scheduleService = scheduleService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Get schedules by pages
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetSchedules([FromQuery] int limit = 20, [FromQuery] int offset = 0)
        {
            var pageModel = scheduleService.GetSchedules(limit, offset);

            return Ok(mapper.Map<PageResponse<ScheduleResponse>>(pageModel));
        }

        /// <summary>
        /// Update schedule
        /// </summary>
        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateSchedule([FromRoute] Guid id, [FromBody] UpdateScheduleRequest model)
        {
            var validationResult = model.Validate();
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            try
            {
                var resultModel = scheduleService.UpdateSchedule(id, mapper.Map<UpdateScheduleModel>(model));

                return Ok(mapper.Map<ScheduleResponse>(resultModel));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        /// <summary>
        /// Delete schedule
        /// </summary>
        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteSchedule([FromRoute] Guid id)
        {
            try
            {
                scheduleService.DeleteSchedule(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        /// <summary>
        /// Get schedule
        /// </summary>
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetSchedule([FromRoute] Guid id)
        {
            try
            {
                var scheduleModel = scheduleService.GetSchedule(id);
                return Ok(mapper.Map<ScheduleResponse>(scheduleModel));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPost]
        public IActionResult CreateSchedule([FromBody] CreateScheduleRequest createScheduleRequest, [FromQuery] Guid DoctorId, [FromQuery] Guid ReceptionId)
        {
            var validationResult = createScheduleRequest.Validate();
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            try
            {
                var resultModel = scheduleService.CreateSchedule(mapper.Map<CreateScheduleModel>(createScheduleRequest), DoctorId, ReceptionId);
                return Ok(resultModel);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
