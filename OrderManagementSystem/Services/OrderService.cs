using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Data;
using OrderManagementSystem.Data.Repositories;
using OrderManagementSystem.DTOs;
using OrderManagementSystem.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManagementSystem.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IEmailService _emailService;
        private readonly OrderManagementDbContext _context;

        public OrderService(
            IOrderRepository orderRepository,
            IProductRepository productRepository,
            IEmailService emailService,
            OrderManagementDbContext context)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _emailService = emailService;
            _context = context;
        }

        public async Task<Order> CreateOrderAsync(OrderCreateDto orderDto)
        {
            var order = new Order
            {
                CustomerId = orderDto.CustomerId,
                OrderDate = DateTime.UtcNow,
                PaymentMethod = orderDto.PaymentMethod,
                Status = OrderStatus.Pending
            };

            decimal totalAmount = 0;
            foreach (var item in orderDto.OrderItems)
            {
                var product = await _productRepository.GetByIdAsync(item.ProductId);
                if (product == null)
                    throw new Exception($"Product with ID {item.ProductId} not found");

                if (product.Stock < item.Quantity)
                    throw new Exception($"Insufficient stock for product {product.Name}");

                var orderItem = new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = product.Price,
                    Discount = CalculateDiscount(item.Quantity * product.Price)
                };

                product.Stock -= item.Quantity;
                await _productRepository.UpdateAsync(product);

                totalAmount += (item.Quantity * product.Price) - orderItem.Discount;
                order.OrderItems.Add(orderItem);
            }

            order.TotalAmount = totalAmount;
            await _orderRepository.CreateAsync(order);
            await GenerateInvoiceAsync(order.OrderId);
            await SendOrderStatusEmailAsync(order.OrderId, order.Status);

            return order;
        }

        private decimal CalculateDiscount(decimal amount)
        {
            if (amount > 200)
                return amount * 0.10m;
            if (amount > 100)
                return amount * 0.05m;
            return 0;
        }

        public async Task<Invoice> GenerateInvoiceAsync(int orderId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order == null)
                throw new Exception("Order not found");

            var invoice = new Invoice
            {
                OrderId = orderId,
                InvoiceDate = DateTime.UtcNow,
                TotalAmount = order.TotalAmount,
                Order = order
            };

            _context.Invoices.Add(invoice);
            await _context.SaveChangesAsync();
            return invoice;
        }

        public async Task SendOrderStatusEmailAsync(int orderId, OrderStatus status)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order == null)
                throw new Exception("Order not found");

            var customer = await _context.Customers.FindAsync(order.CustomerId);
            if (customer == null)
                throw new Exception("Customer not found");

            await _emailService.SendEmailAsync(
                customer.Email,
                $"Order {orderId} Status Update",
                $"Your order status has been updated to {status}");
        }
    }
}