using System;
using System.Threading.Tasks;
using Timesheets.Models;

namespace Timesheets.Domain.Interfaces
{
    public interface IRefreshTokenManager
    {
        Task<JwtRefreshToken> GetToken(string token);
        void CreateToken(JwtRefreshToken jwtRefreshToken);
    }
}