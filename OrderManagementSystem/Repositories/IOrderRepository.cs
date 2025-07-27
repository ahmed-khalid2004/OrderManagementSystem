using OrderManagementSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderManagementSystem.Data.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> CreateAsync(Order order);
        Task<Order> GetByIdAsync(int orderId);
        Task<List<Order>> GetAllAsync();
        Task UpdateStatusAsync(int orderId, OrderStatus status);
    }
}