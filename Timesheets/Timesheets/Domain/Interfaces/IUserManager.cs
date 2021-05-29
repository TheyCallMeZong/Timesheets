using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Timesheets.Models;
using Timesheets.Models.Dto;
using Timesheets.Models.Dto.Authentication;

namespace Timesheets.Domain.Interfaces
{
    public interface IUserManager
    {
        User GetUserById(Guid id);
        Task<User> GetUser(LoginRequest request);
        Task CreateUser(CreateUserRequest user);
        Task<List<UserDto>> Users();
        Task Update(Guid id, CreateUserRequest user);
        Task Delete(Guid id);
    }
}