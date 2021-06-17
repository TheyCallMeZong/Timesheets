using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

        public async Task<JwtRefreshToken> GetTokenFromDB(string token)
        {
            var result = await _context.RefreshTokens.
                FirstOrDefaultAsync(x => x.Token == token);
            return result;
        }

        public void Add(JwtRefreshToken refreshToken)
        {
            _context.RefreshTokens.Add(refreshToken);
            _context.SaveChanges();
        }

        public void DeleteTOken(JwtRefreshToken refreshToken)
        {
            foreach (var token in _context.RefreshTokens)
            {
                if (token.Token == refreshToken.Token)
                {
                    _context.RefreshTokens.Remove(token);
                }
            }

            _context.SaveChanges();
        }
    }
}