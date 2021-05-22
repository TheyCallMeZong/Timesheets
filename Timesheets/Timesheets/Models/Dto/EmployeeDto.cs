using System;

namespace Timesheets.Models.Dto
{
    public class EmployeeDto
    {
        public Guid UserId { get; set; }
        public bool IsDeleted { get; set; }
    }
}