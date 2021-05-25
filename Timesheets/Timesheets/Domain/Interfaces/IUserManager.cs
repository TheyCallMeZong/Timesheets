using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Timesheets.Models;
using Timesheets.Models.Dto;

namespace Timesheets.Domain.Interfaces
{
    public interface IUserManager
    {
        Task CreateUser(CreateUserRequest user);
        Task<List<UserDto>> Users();
        Task Update(Guid id, CreateUserRequest user);
        Task Delete(Guid id);
    }
}