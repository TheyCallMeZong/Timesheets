using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Timesheets.Domain.Interfaces;
using Timesheets.Models.Dto;

namespace Timesheets.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController : ControllerBase
    {
        private readonly IUserManager _userManager;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserManager userManager, ILogger<UserController> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        /// <summary>
        /// Добавление нового пользователя в БД
        /// </summary>
        /// <param name="userRequest">имя, пароль, статус</param>
        /// <returns></returns>
        [HttpPost("user/create")]
        public async Task<IActionResult> Create([FromBody] CreateUserRequest userRequest)
        {
            _logger.LogInformation("Create new user");
            await _userManager.CreateUser(userRequest);
            return Ok();
        } 
        
        /// <summary>
        /// Вывод всех юзеров 
        /// </summary>
        /// <returns></returns>
        [HttpGet("getall")]
        public async Task<IActionResult> GetAllUsers()
        {
            _logger.LogInformation("Get all users");
            var result = await _userManager.Users();
            return Ok(result);
        }

        /// <summary>
        /// Обновление пользователя
        /// </summary>
        /// <param name="userRequest">новые данные</param>
        /// <returns></returns>
        [HttpPut("user/update/id/{id}")]
        public async Task<IActionResult> UpdateUser([FromBody] CreateUserRequest userRequest, [FromRoute] Guid id)
        {
            await _userManager.Update(id, userRequest);
            return Ok();
        }

        /// <summary>
        /// Удаление пользователя
        /// </summary>
        /// <param name="id">id для поиска пользователя</param>
        /// <returns></returns>
        [HttpDelete("user/delete/id/{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid id)
        {
            await _userManager.Delete(id);
            return Ok();
        }
        
        [HttpGet("user/{id}")]
        public IActionResult GetUserById([FromRoute] Guid id)
        {
            var user = _userManager.GetUserById(id);
            return Ok(user);
        }
    }
}