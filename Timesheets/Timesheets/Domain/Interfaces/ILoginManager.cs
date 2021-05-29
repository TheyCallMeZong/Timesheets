using System.Threading.Tasks;
using Timesheets.Models;
using Timesheets.Models.Dto.Authentication;

namespace Timesheets.Domain.Interfaces
{
    public interface ILoginManager
    {
        LoginResponse Authenticate(User user);
    }
}