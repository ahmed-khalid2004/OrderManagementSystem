using OrderManagementSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderManagementSystem.Data.Repositories
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(int productId);
        Task<Product> CreateAsync(Product product);
        Task<Product> UpdateAsync(Product product);
    }
}