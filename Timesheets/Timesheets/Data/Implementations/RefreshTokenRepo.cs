using System.Threading.Tasks;
using Timesheets.Data.Ef;
using Timesheets.Data.Interfaces;
using Timesheets.Models;
using Timesheets.Models.Dto.Authentication;

namespace Timesheets.Data.Implementations
{
    public class RefreshTokenRepo : IRefreshTokenRepo
    {
        private readonly PostgreSqlDbContext _context;

        public RefreshTokenRepo(PostgreSqlDbContext context)
        {
            _context = context;
        }

        public async Task Add(JwtRefreshToken refreshToken)
        {
            await _context.RefreshTokens.AddAsync(refreshToken);
            await _context.SaveChangesAsync();
        }
    }
}