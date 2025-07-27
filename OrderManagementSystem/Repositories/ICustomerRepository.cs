using OrderManagementSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderManagementSystem.Data.Repositories
{
    public interface ICustomerRepository
    {
        Task<Customer> CreateAsync(Customer customer);
        Task<List<Order>> GetOrdersByCustomerIdAsync(int customerId);
    }
}