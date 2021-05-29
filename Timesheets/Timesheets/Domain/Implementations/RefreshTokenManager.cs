using System.Threading.Tasks;
using Timesheets.Data.Interfaces;
using Timesheets.Domain.Interfaces;
using Timesheets.Models;

namespace Timesheets.Domain.Implementations
{
    public class RefreshTokenManager : IRefreshTokenManager
    {
        private readonly IRefreshTokenRepo _refreshTokenRepo;

        public RefreshTokenManager(IRefreshTokenRepo refreshTokenRepo)
        {
            _refreshTokenRepo = refreshTokenRepo;
        }

        public async Task CreateToken(JwtRefreshToken jwtRefreshToken)
        {
            await _refreshTokenRepo.Add(jwtRefreshToken);
        }
    }
}