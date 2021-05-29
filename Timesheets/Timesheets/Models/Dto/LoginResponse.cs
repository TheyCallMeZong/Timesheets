namespace Timesheets.Models.Dto.Authentication
{
    public class LoginResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public long Time { get; set; }
    }
}