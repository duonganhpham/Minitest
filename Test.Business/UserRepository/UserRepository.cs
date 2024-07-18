using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Test.Business.Model;
using Test.Data;
using Test.Entities;

namespace Test.Business.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByUsername(string username)
        {
            return  await _context.Users.SingleOrDefaultAsync(u => u.Username == username);
        }

        public async Task AddUser(UserRequestModel user)
        {
            var newUser = new User
            {
                Id = new Guid(),
                Username = user.Username,
                Password = user.Password,

            };
            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();
        }
    }
}
