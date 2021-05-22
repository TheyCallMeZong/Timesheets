using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Timesheets.Domain.Interfaces;
using Timesheets.Models;
using Timesheets.Models.Dto;

namespace Timesheets.Controllers
{
    [ApiController]
    [Route("employees")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeManager _employeeManager;

        public EmployeeController(IEmployeeManager employeeManager)
        {
            _employeeManager = employeeManager;
        }

        [HttpPost("employee/create")]
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeDto employeeDto)
        {
            await _employeeManager.CreateEmployee(employeeDto);
            return Ok();
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAllEmployees()
        {
            var result = await _employeeManager.Employees();
            return Ok(result);
        }

        [HttpDelete("employee/delete/id/{id}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] Guid id)
        {
            await _employeeManager.Delete(id);
            return Ok();
        }

        [HttpPut("employee/update/{id}")]
        public async Task<IActionResult> UpdateEmployee([FromBody] Employee employee)
        {
            await _employeeManager.Update(employee);
            return Ok();
        }
    }
}