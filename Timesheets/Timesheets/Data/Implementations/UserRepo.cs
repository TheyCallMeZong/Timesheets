using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Timesheets.Data.Ef;
using Timesheets.Data.Interfaces;
using Timesheets.Models;
using Timesheets.Models.Dto;
using Timesheets.Models.Dto.Authentication;

namespace Timesheets.Data.Implementations
{
    public class UserRepo : IUserRepo
    {
        private readonly PostgreSqlDbContext _context;

        public UserRepo(PostgreSqlDbContext context)
        {
            _context = context;
        }

        public async Task Create(User item)
        {
            await _context.Users.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetByLoginAndPasswordHash(string login, byte[] passwordHash)
        {
            return await _context.Users
                    .Where(x => x.UserName == login && x.PasswordHash == passwordHash)
                    .FirstOrDefaultAsync();
        }

        public async Task<List<UserDto>> All()
        {
            return _context.Users.Select(user => 
                new UserDto()
            {
                Id = user.Id,
                UserName = user.UserName, 
                Role = user.Role
            }).ToList();
        }                                                
                                                         
        public async Task Update(User item)
        {
            var user = await _context.Users.FindAsync(item.Id);
            if (user != null)                            
            {         
                user.UserName = item.UserName;           
                user.Role = item.Role;
                user.PasswordHash = item.PasswordHash;
            }
            _context.Users.Update(user);
            await _context.SaveChangesAsync();           
        }

        public async Task Delete(Guid id)
        {
            foreach (var user in _context.Users)
            {
                if (user.Id == id)
                {
                    _context.Users.Remove(user);
                }
            }
            await _context.SaveChangesAsync();
        }

        public User GetUserById(Guid id)
        {
            return _context.Users.FirstOrDefault(x => x.Id == id);
        }
    }                                                    
}                                                        