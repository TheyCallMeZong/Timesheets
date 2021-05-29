using System;

namespace Timesheets.Models.Dto
{
    public class JwtTokenRefreshRequest
    {
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public bool IsExpired => DateTime.UtcNow >= Expires;
    }
}