using AutoMapper;
using Dentistry.Services.Abstract;
using Dentistry.Services.Models;
using Dentistry.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Dentistry.Controllers
{
    /// <summary>
    /// Roles endpoints
    /// </summary>
    [ProducesResponseType(200)]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService roleService;
        private readonly IMapper mapper;

        /// <summary>
        /// Roles controller
        /// </summary>
        public RolesController(IRoleService roleService, IMapper mapper)
        {
            this.roleService = roleService;
            this.mapper = mapper;
        }
        /// <summary>
        /// Get Roles by pages
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetRoles([FromQuery] int limit = 20, [FromQuery] int offset = 0)
        {
            var pageModel = roleService.GetRoles(limit, offset);
            return Ok(mapper.Map<PageResponse<RolePreviewResponse>>(pageModel));
        }

        /// <summary>
        /// Add Role
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddRole([FromBody] RoleModel role)
        {
            var response = roleService.AddRole(role);
            return Ok(response);
        }


        /// <summary>
        /// Update Role
        /// </summary>
        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateRole([FromRoute] Guid id, [FromBody] UpdateRoleRequest model)
        {
            var validationResult = model.Validate();
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            try
            {
                var resultModel = roleService.UpdateRole(id, mapper.Map<UpdateRoleModel>(model));

                return Ok(mapper.Map<RoleResponse>(resultModel));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        /// <summary>
        /// Delete Role
        /// </summary>
        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteRole([FromRoute] Guid id)
        {
            try
            {
                roleService.DeleteRole(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        /// <summary>
        /// Get Role
        /// </summary>
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetRole([FromRoute] Guid id)
        {
            try
            {
                var roleModel = roleService.GetRole(id);
                return Ok(mapper.Map<RoleResponse>(roleModel));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
    }
}