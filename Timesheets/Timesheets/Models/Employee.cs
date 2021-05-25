using System;

namespace Timesheets.Models
{
    public class Employee
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public bool IsDeleted { get; set; }
        
        public User User { get; set; }
    }
}