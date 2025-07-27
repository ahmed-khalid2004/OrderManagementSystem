using OrderManagementSystem.DTOs;
using OrderManagementSystem.Models;
using System.Threading.Tasks;

namespace OrderManagementSystem.Services
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(OrderCreateDto orderDto);
        Task<Invoice> GenerateInvoiceAsync(int orderId);
        Task SendOrderStatusEmailAsync(int orderId, OrderStatus status);
    }
}