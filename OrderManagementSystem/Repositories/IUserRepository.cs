using OrderManagementSystem.Models;
using System.Threading.Tasks;

namespace OrderManagementSystem.Data.Repositories
{
    public interface IUserRepository
    {
        Task<User> CreateAsync(User user);
        Task<User> GetByUsernameAsync(string username);
    }
}