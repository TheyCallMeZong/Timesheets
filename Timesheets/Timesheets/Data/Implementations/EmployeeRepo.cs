using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timesheets.Data.Ef;
using Timesheets.Data.Interfaces;
using Timesheets.Models;
using Timesheets.Models.Dto;

namespace Timesheets.Data.Implementations
{
    public class EmployeeRepo : IEmployeeRepo
    {
        private readonly PostgreSqlDbContext _context;

        public EmployeeRepo(PostgreSqlDbContext context)
        {
            _context = context;
        }

        public async Task Create(Employee item)
        {
            await _context.Employees.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            foreach (var employee in _context.Employees)
            {
                if (employee.Id == id)
                {
                    _context.Employees.Remove(employee);
                }
            }
            await _context.SaveChangesAsync();
        }

        public async Task Update(Employee item)
        {
            var employeeToUpdate = await _context.Employees.FindAsync(item.Id);
            if (employeeToUpdate != null)
            {
                employeeToUpdate.Id = item.Id;
                employeeToUpdate.UserId = item.UserId;
                employeeToUpdate.IsDeleted = item.IsDeleted;
            }

            await _context.SaveChangesAsync();
        }

        public async Task<List<EmployeeDto>> All()
        {
            return _context.Employees.Select(employee => 
                new EmployeeDto() {UserId = employee.UserId, IsDeleted = employee.IsDeleted}).ToList();
        }
    }
}