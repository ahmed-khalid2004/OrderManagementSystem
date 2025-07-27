using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.Data.Repositories;
using OrderManagementSystem.Models;
using System.Threading.Tasks;

namespace OrderManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpPost]
        public async Task<ActionResult<Customer>> CreateCustomer(Customer customer)
        {
            var createdCustomer = await _customerRepository.CreateAsync(customer);
            return CreatedAtAction(nameof(GetCustomerOrders), new { customerId = createdCustomer.CustomerId }, createdCustomer);
        }

        [HttpGet("{customerId}/orders")]
        public async Task<ActionResult> GetCustomerOrders(int customerId)
        {
            var orders = await _customerRepository.GetOrdersByCustomerIdAsync(customerId);
            return Ok(orders);
        }
    }
}