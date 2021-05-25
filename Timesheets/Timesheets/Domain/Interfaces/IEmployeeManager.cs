using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Timesheets.Models;
using Timesheets.Models.Dto;

namespace Timesheets.Domain.Interfaces
{
    public interface IEmployeeManager
    {
        Task CreateEmployee(EmployeeDto user);
        Task<List<EmployeeDto>> Employees();
        Task Update(Employee user);
        Task Delete(Guid id);
    }
}