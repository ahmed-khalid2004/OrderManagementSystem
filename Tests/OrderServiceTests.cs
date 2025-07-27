using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Data;
using OrderManagementSystem.Data.Repositories;
using OrderManagementSystem.Models;
using OrderManagementSystem.Services;
using OrderManagementSystem.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace OrderManagementSystem.Tests
{
	public class OrderServiceTests
	{
		private readonly OrderManagementDbContext _context;
		private readonly OrderService _orderService;
		private readonly IOrderRepository _orderRepository;
		private readonly IProductRepository _productRepository;
		private readonly IEmailService _emailService;

		public OrderServiceTests()
		{
			var options = new DbContextOptionsBuilder<OrderManagementDbContext>()
				.UseInMemoryDatabase(databaseName: "TestDb")
				.Options;

			_context = new OrderManagementDbContext(options);
			_orderRepository = new OrderRepository(_context);
			_productRepository = new ProductRepository(_context);
			_emailService = new EmailService();
			_orderService = new OrderService(_orderRepository, _productRepository, _emailService, _context);
		}

		[Fact]
		public async Task CreateOrderAsync_ValidOrder_ReturnsOrder()
		{
			// Arrange
			var product = new Product { ProductId = 1, Name = "Test Product", Price = 100, Stock = 10 };
			await _productRepository.CreateAsync(product);

			var orderDto = new OrderCreateDto
			{
				CustomerId = 1,
				PaymentMethod = "CreditCard",
				OrderItems = new List<OrderItemDto>
				{
					new OrderItemDto { ProductId = 1, Quantity = 2 }
				}
			};

			// Act
			var order = await _orderService.CreateOrderAsync(orderDto);

			// Assert
			Assert.NotNull(order);
			Assert.Equal(2, order.OrderItems.Count);
			Assert.Equal(190, order.TotalAmount); // 200 - 10% discount
		}

		[Fact]
		public async Task CreateOrderAsync_InsufficientStock_ThrowsException()
		{
			// Arrange
			var product = new Product { ProductId = 1, Name = "Test Product", Price = 100, Stock = 1 };
			await _productRepository.CreateAsync(product);

			var orderDto = new OrderCreateDto
			{
				CustomerId = 1,
				PaymentMethod = "CreditCard",
				OrderItems = new List<OrderItemDto>
				{
					new OrderItemDto { ProductId = 1, Quantity = 2 }
				}
			};

			// Act & Assert
			await Assert.ThrowsAsync<Exception>(() => _orderService.CreateOrderAsync(orderDto));
		}
	}
}