using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Timesheets.Models.Dto.Authentication
{
    public class JwtOptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string SigningKey { get; set; }
        public int LifeTime { get; set; }

        public TokenValidationParameters GetTokenValidationParameters()
        {
            return new TokenValidationParameters()
            {
                ValidateIssuer = false,
                ValidIssuer = Issuer,
                ValidateAudience = false,
                ValidAudience = Audience,
                ValidateLifetime = true,
                IssuerSigningKey = GetSummetricSecurityKey(),
                ValidateIssuerSigningKey = true,
                RoleClaimType = ClaimsIdentity.DefaultRoleClaimType
            };
        }

        private SymmetricSecurityKey GetSummetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SigningKey));
        }

        public JwtSecurityToken GenerateSecurityToken(IEnumerable<Claim> claims)
        {
            var timeNow = DateTime.Now;

            var jwt = new JwtSecurityToken(
                issuer: Issuer,
                audience: Audience,
                notBefore: timeNow,
                claims: claims,
                expires: timeNow.Add(TimeSpan.FromMinutes(LifeTime)),
                signingCredentials: new SigningCredentials(GetSummetricSecurityKey(),
                    SecurityAlgorithms.HmacSha256)
            );

            return jwt;
        }
    }
}