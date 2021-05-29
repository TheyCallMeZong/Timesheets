using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Timesheets.Domain.Interfaces;
using Timesheets.Models;
using Timesheets.Models.Dto;
using Timesheets.Models.Dto.Authentication;

namespace Timesheets.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : Controller
    {
        private readonly ILoginManager _loginManager;
        private readonly IUserManager _userManager;

        public LoginController(ILoginManager loginManager, IUserManager userManager)
        {
            _loginManager = loginManager;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _userManager.GetUser(request);

            if (user == null) 
                return Unauthorized();

            var loginResponse = _loginManager.Authenticate(user);

            return Ok(loginResponse);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromRoute] JwtTokenRefreshRequest jwtTokenRefreshRequest)
        {
            return Ok();
        }
    }
}