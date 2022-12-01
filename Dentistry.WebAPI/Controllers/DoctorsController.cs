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
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorService doctorService;
        private readonly IMapper mapper;

        /// <summary>
        /// Doctors controller
        /// </summary>
        public DoctorsController(IDoctorService doctorService, IMapper mapper)
        {
            this.doctorService = doctorService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Get doctors by pages
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetDoctors([FromQuery] int limit = 20, [FromQuery] int offset = 0)
        {
            var pageModel = doctorService.GetDoctors(limit, offset);

            return Ok(mapper.Map<PageResponse<DoctorResponse>>(pageModel));
        }

        /// <summary>
        /// Update doctor
        /// </summary>
        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateDoctor([FromRoute] Guid id, [FromBody] UpdateDoctorRequest model)
        {
            var validationResult = model.Validate();
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            try
            {
                var resultModel = doctorService.UpdateDoctor(id, mapper.Map<UpdateDoctorModel>(model));

                return Ok(mapper.Map<DoctorResponse>(resultModel));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        /// <summary>
        /// Delete doctor
        /// </summary>
        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteDoctor([FromRoute] Guid id)
        {
            try
            {
                doctorService.DeleteDoctor(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        /// <summary>
        /// Get doctor
        /// </summary>
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetDoctor([FromRoute] Guid id)
        {
            try
            {
                var doctorModel = doctorService.GetDoctor(id);
                return Ok(mapper.Map<DoctorResponse>(doctorModel));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

         [HttpPost]
        public IActionResult CreateDoctor([FromBody] CreateDoctorRequest createDoctorRequest)
        {
            var validationResult = createDoctorRequest.Validate();
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            try
            {
                var resultModel = doctorService.CreateDoctor(mapper.Map<CreateDoctorModel>(createDoctorRequest));
                return Ok(resultModel);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
