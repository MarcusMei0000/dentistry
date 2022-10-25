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
        /// <summary>
        /// Doctors controller
        /// </summary>
        public DoctorsController()
        {

        }

        /// <summary>
        /// Get doctors
        /// </summary>
        /// <returns></returns>
        [ApiVersion("1.0")]
        [ApiVersion("2.0")]
        [HttpGet]
        public IActionResult GetDoctors()
        {
            return Ok();
        }
    }
}
