using System;
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
        private readonly IRefreshTokenManager _refreshToken;

        public LoginController(ILoginManager loginManager,
            IUserManager userManager,
            IRefreshTokenManager refreshToken)
        {
            _loginManager = loginManager;
            _userManager = userManager;
            _refreshToken = refreshToken;
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
        public async Task<IActionResult> Refresh([FromBody] JwtTokenRefreshRequest jwtTokenRefreshRequest)
        {
            var result = await _refreshToken.GetToken(jwtTokenRefreshRequest.Token);

            var user = _userManager.GetUserById(result.UserId);
            
            if (user != null)
            {
                var newTokens = _loginManager.Authenticate(user);
                return Ok(newTokens);
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}