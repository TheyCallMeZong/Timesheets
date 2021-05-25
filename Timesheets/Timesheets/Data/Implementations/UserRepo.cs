using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timesheets.Data.Ef;
using Timesheets.Data.Interfaces;
using Timesheets.Models;
using Timesheets.Models.Dto;

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

        public async Task<List<UserDto>> All()
        {
            return _context.Users.Select(user => 
                new UserDto()
            {
                UserName = user.UserName, 
                Role = user.Role
            }).ToList();
        }                                                
                                                         
        public async Task Update(User item)
        {
            var user = await _context.Users.FindAsync(item.Id);
            if (user != null)                            
            {                                            
                user.Id = Guid.NewGuid();                
                user.UserName = item.UserName;           
                user.Role = item.Role;
                user.PasswordHash = item.PasswordHash;
            }                                            
                                                         
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
    }                                                    
}                                                        