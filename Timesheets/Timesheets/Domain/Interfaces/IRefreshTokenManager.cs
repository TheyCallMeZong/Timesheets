using System.Threading.Tasks;
using Timesheets.Models;

namespace Timesheets.Domain.Interfaces
{
    public interface IRefreshTokenManager
    { 
        Task CreateToken(JwtRefreshToken jwtRefreshToken);
    }
}