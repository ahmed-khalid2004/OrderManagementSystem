using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Models;
using System.Threading.Tasks;

namespace OrderManagementSystem.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly OrderManagementDbContext _context;

        public UserRepository(OrderManagementDbContext context)
        {
            _context = context;
        }

        public async Task<User> CreateAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }
    }
}