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
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllProducts()
        {
            var products = await _productRepository.GetAllAsync();
            return Ok(products);
        }

        [HttpGet("{productId}")]
        public async Task<ActionResult<Product>> GetProduct(int productId)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            if (product == null)
                return NotFound();
            return Ok(product);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            var createdProduct = await _productRepository.CreateAsync(product);
            return CreatedAtAction(nameof(GetProduct), new { productId = createdProduct.ProductId }, createdProduct);
        }

        [HttpPut("{productId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Product>> UpdateProduct(int productId, Product product)
        {
            if (productId != product.ProductId)
                return BadRequest();

            var updatedProduct = await _productRepository.UpdateAsync(product);
            return Ok(updatedProduct);
        }
    }
}