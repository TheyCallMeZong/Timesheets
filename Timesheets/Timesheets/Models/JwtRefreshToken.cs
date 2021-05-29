using System;

namespace Timesheets.Models
{
    public class JwtRefreshToken
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Token { get; set; }
        public Guid UserId { get; set; }
        
        public User User { get; set; }
    }
}