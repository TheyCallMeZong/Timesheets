using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Timesheets.Data.Interfaces;
using Timesheets.Domain.Interfaces;
using Timesheets.Models;
using Timesheets.Models.Dto;
using Timesheets.Models.Dto.Authentication;

namespace Timesheets.Domain.Implementations
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepo _userRepo;

        public UserManager(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<User> GetUser(LoginRequest request)
        {
            var passwordHash = GetPasswordHash(request.Password);
            var user = await _userRepo.GetByLoginAndPasswordHash(request.Login, passwordHash);

            return user;
        }

        public async Task<Guid> CreateUser(CreateUserRequest user)
        {
            var passwordHash = GetPasswordHash(user.Password);
            Guid id = Guid.NewGuid();
            await _userRepo.Create(new User()
            {
                UserName = user.UserName,
                PasswordHash = passwordHash,
                Role = user.Role,
                Id = id
            });
            return id;
        }

        public async Task<List<UserDto>> Users()
        {
            var userDos = await _userRepo.All();
            if (userDos != null)
            {
                return userDos;
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        public async Task Update(Guid id, CreateUserRequest user)
        {
            var passwordHash = GetPasswordHash(user.Password);
            var newUser = new User()
            {
                UserName = user.UserName,
                Role = user.Role,
                PasswordHash = passwordHash,
                Id = id
            };
            await _userRepo.Update(newUser);
        }

        public async Task Delete(Guid id)
        {
            await _userRepo.Delete(id);
        }

        public User GetUserById(Guid id)
        {
            var result = _userRepo.GetUserById(id);
            return result;
        }
        
        private static byte[] GetPasswordHash(string password)
        {
            using var sha1 = new SHA1CryptoServiceProvider();
            return sha1.ComputeHash(Encoding.Unicode.GetBytes(password));
        }
    }
}