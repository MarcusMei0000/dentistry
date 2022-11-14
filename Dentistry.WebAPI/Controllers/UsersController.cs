using System.Globalization;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using Dentistry.Entities.Models;
using Dentistry.Repository;
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
    public class UsersController : ControllerBase
    { 
        private IRepository<User> _repository;

        /// <summary>
        /// Users controller
        /// </summary>
        public UsersController(IRepository<User> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Get users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetUsers()
        {
            var user = new User()
            {
                PasswordHash = "123",
                FirstName = "POlina",
                LastName = "Poliakova",
                Patronymic = "Gennadievna"
            };
            _repository.Save(user);
            user.FirstName = "Lubov";
            _repository.Save(user);
            var users = _repository.GetAll();
            return Ok(users);
        }
        /// <summary>
        /// Get users
        /// </summary>
        /// <param name="user"></param>
        [HttpGet]
        public IActionResult GetUsers(Guid id)
        {
            var user = _repository.GetById(id);
            return Ok(user);
        }
        /// <summary>
        /// Delete users
        /// </summary>
        /// <param name="user"></param>
        [HttpDelete]
        public IActionResult DeleteUsers(User user)
        {
            _repository.Delete(user);
            return Ok();
        }
        /// <summary>
        /// Post users
        /// </summary>
        /// <param name="user"></param>
        [HttpPost]
        public IActionResult PostUsers(User user)
        {
            var result = _repository.Save(user);
            return Ok(result);
        }

        /// <summary>
        /// Update users
        /// </summary>
        /// <param name="student"></param>
        [HttpPut]
        public IActionResult Updatesers(User user)
        {
            return PostUsers(user);
        }

    }
}
