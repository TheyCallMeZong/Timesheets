using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Timesheets.Data.Interfaces;
using Timesheets.Domain.Interfaces;
using Timesheets.Models;
using Timesheets.Models.Dto;

namespace Timesheets.Domain.Implementations
{
    public class EmployeeManager : IEmployeeManager
    {
        private readonly IEmployeeRepo _employeeRepo;

        public EmployeeManager(IEmployeeRepo employeeRepo)
        {
            _employeeRepo = employeeRepo;
        }

        public async Task CreateEmployee(EmployeeDto employee)
        {
            await _employeeRepo.Create(new Employee()
            {
                Id = Guid.NewGuid(),
                IsDeleted = employee.IsDeleted,
                UserId = employee.UserId
            });
        }

        public async Task<List<EmployeeDto>> Employees()
        {
            var result = await _employeeRepo.All();
            if (result != null)
            {
                return result;
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        public async Task Update(Employee employee)
        {
            await _employeeRepo.Update(employee);
        }

        public async Task Delete(Guid id)
        {
            await _employeeRepo.Delete(id);
        }
    }
}