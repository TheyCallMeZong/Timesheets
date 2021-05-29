using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Timesheets.Models;
using Timesheets.Models.Dto;

namespace Timesheets.Data.Interfaces
{
    public interface IUserRepo : IRepoBase<User>
    {
        User GetUserById(Guid id);
        Task<User> GetByLoginAndPasswordHash(string login, byte[] passwordHash);
        Task<List<UserDto>> All();
    }
}