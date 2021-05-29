using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Timesheets.Data.Interfaces;
using Timesheets.Domain.Interfaces;
using Timesheets.Infrastucture.Extensions;
using Timesheets.Models;
using Timesheets.Models.Dto.Authentication;

namespace Timesheets.Domain.Implementations
{
    public class LoginManager : ILoginManager
    {
        private readonly AccessToken _accessToken;
        private readonly RefreshToken _refreshToken;
        private readonly IRefreshTokenManager _refreshTokenManager;
        public LoginManager(IOptions<AccessToken> accessToken, 
            IOptions<RefreshToken> refresh,
            IRefreshTokenManager refreshTokenManager)
        {
            _refreshTokenManager = refreshTokenManager;
            _refreshToken = refresh.Value;
            _accessToken = accessToken.Value;
        }

        public LoginResponse Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role)
            };

            var accessTokenRaw = _accessToken.GenerateSecurityToken(claims);
            var securityHandler = new JwtSecurityTokenHandler();
            var accessToken = securityHandler.WriteToken(accessTokenRaw);

            var refreshTokenRaw = _refreshToken.GenerateSecurityToken(claims);
            var securityHandlerRefresh = new JwtSecurityTokenHandler();
            var refreshToken = securityHandlerRefresh.WriteToken(refreshTokenRaw);
            _refreshTokenManager.CreateToken(new JwtRefreshToken()
            {
                UserId = user.Id,
                Token = refreshToken,
                Date = DateTime.UtcNow
            });

            var loginResponse = new LoginResponse()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                Time = accessTokenRaw.ValidTo.ToEpochTime()
            };

            return loginResponse;
        }
    }
}